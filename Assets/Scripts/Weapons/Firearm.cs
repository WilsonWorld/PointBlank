/* Created by Wilson World Games, August 2022 */

using System.Collections;
using UnityEngine;

public enum EFireMode
{
    FM_SEMI = 0,
    FM_AUTO = 1,
    FM_BURST = 2,
    FM_MAX
}

public class Firearm : MonoBehaviour
{
    [Header("Main Properties")]
    public EFireMode CurrentFireMode;
    public float BaseDamage;
    public float BaseRange;
    public float FireDelay;
    public float ProjectileSpreadRadius;
    public float ProjectileAmount = 1;
    public int CurrentAmmo;
    public int MaxAmmo;
    public int StoredAmmoCount;
    public int StoredAmmoMax;
    public bool bDebug;

    [Header("Attachments")]
    public Transform MuzzlePoint;
    public GameObject MuzzleFlashObj;

    [Header("Effects")]
    public AudioSource CasingDropSFX;
    public AudioSource DryFireSFX;
    public AudioSource GunShotSFX;
    public AudioSource ReloadSFX;
    public AudioSource UnloadSFX;
    public ParticleSystem BloodPFX;
    public ParticleSystem CasingPFX;

    [Header("Prefabs")]
    public GameObject BulletHoleObj;
    public GameObject BulletHoleMetal;
    public GameObject BulletHoleWood;

    [Header("UI")]
    public AmmoDisplay AmmoUI;
    public GameObject CrosshairUI;
    public Texture WeaponIconUI;

    // Component / Object References
    protected Animator WeaponAnimator;
    protected GameObject WeaponObj;

    // Weapon States
    protected bool isAimingDownSights = false;
    protected bool isFiring = false;
    protected bool isReloading = false;

    protected virtual void Start()
    {
        WeaponObj = this.gameObject;
        WeaponAnimator = WeaponObj.GetComponent<Animator>();
        CurrentAmmo = MaxAmmo;
    }

    protected virtual void Update()
    {
        UpdateAmmoDisplay();

        // Check for Fire input
        switch (CurrentFireMode) {
            case EFireMode.FM_SEMI:
                if (Input.GetButtonDown("Fire1")) {
                    PlayDryFireFX();
                    FireWeapon();
                }
                break;

            case EFireMode.FM_AUTO:
                if (Input.GetButtonDown("Fire1"))
                    PlayDryFireFX();

                if (Input.GetButton("Fire1"))
                    FireWeapon();
                break;
        }

        // Check for Aim press
        if (Input.GetButtonDown("Fire2"))
            AimDownSights();

        // Check for Aim release
        if (Input.GetButtonUp("Fire2"))
            AimFromHip();

        // Check for Reload input
        if (Input.GetButtonDown("Reload"))
        {
            if (isReloading == true || StoredAmmoCount <= 0)
                return;

            if (CurrentAmmo != MaxAmmo)
                StartCoroutine(OnReload());
        }
    }

    protected virtual void FireWeapon()
    {
        if (isFiring == true || isReloading == true || CurrentAmmo <= 0)
            return;

        StartCoroutine(OnFire());
    }

    protected virtual void Reload()
    {
        int ammoNeeded = MaxAmmo - CurrentAmmo;

        if (StoredAmmoCount >= ammoNeeded)
            MoveAmmoFromStorage(ammoNeeded);
        else
            MoveAmmoFromStorage(StoredAmmoCount);
    }

    // Update ammo UI with current ammo counts
    public virtual void UpdateAmmoDisplay()
    {
        AmmoDisplay.AmmoCount = CurrentAmmo;
        AmmoDisplay.StoredAmmoCount = StoredAmmoCount;
    }

    // Increases the storage ammo amount by a desired input, up to the max amount
    public void AddStorageAmmo(int ammoCount)
    {
        StoredAmmoCount += ammoCount;

        if (StoredAmmoCount > StoredAmmoMax)
            StoredAmmoCount = StoredAmmoMax;
    }

    // Decreases the storage ammo amount by a desired input, down to 0 
    public void RemoveStorageAmmo(int ammoCount)
    {
        StoredAmmoCount -= ammoCount;

        if (StoredAmmoCount < 0)
            StoredAmmoCount = 0;
    }

    // Fill the weapon's ammo from the stored ammo
    protected void MoveAmmoFromStorage(int ammo)
    {
        CurrentAmmo += ammo;
        RemoveStorageAmmo(ammo);
    }

    // Set ADS state to true, turn off the crosshair and reduce the FOV
    protected void AimDownSights()
    {
        if (isReloading == true)
            return;

        isAimingDownSights = true;
        CrosshairUI.SetActive(false);
        Camera.main.fieldOfView = 45.0f;
        PlayAimDownSightsAnimation();
    }

    protected void AimFromHip()
    {
        if (isReloading == true)
            return;

        Camera.main.fieldOfView = 60.0f;
        CrosshairUI.SetActive(true);
        StartCoroutine(ReturnToHipAim());
    }

    // Raycasts and creates projectile effects when the firearm is fired
    protected IEnumerator OnFire()
    {
        isFiring = true;
        CurrentAmmo--;
        PlayFireAnimation();
        PlayGunshotFX();

        for (int i = 0; i < ProjectileAmount; i++)
            CrosshairCast();

        yield return new WaitForSeconds(0.05f);
        MuzzleFlashObj.SetActive(false);

        yield return new WaitForSeconds(FireDelay - 0.05f);
        FinishFiring();
    }

    // Return the camera to normal settings before reloading
    protected virtual IEnumerator OnReload()
    {
        if (isAimingDownSights == true) {
            Camera.main.fieldOfView = 60.0f;
            ReturnToHipAim();
            yield return new WaitForSeconds(0.2f);
        }

        isReloading = true;
        PlayReloadAnimation();
    }

    protected IEnumerator ReturnToHipAim()
    {
        PlayAimFromHipAnimation();

        yield return new WaitForSeconds(0.1f);
        isAimingDownSights = false;
        PlayIdleAnimation();
    }

    // Plays a casing dropping effect, resets the gun to the correct animation state and resets the firing state to allow firing again
    protected void FinishFiring()
    {
        CasingDropSFX.Play();
        PlayPostFireAnimation();
        isFiring = false;
    }

    /* ANIMATIONS */
    protected void PlayFireAnimation()
    {
        if (isAimingDownSights == false)
            WeaponAnimator.Play("FireWeapon");
        else
            WeaponAnimator.Play("FireWeaponDownSights");
    }

    protected void PlayPostFireAnimation()
    {
        if (WeaponAnimator == null)
            return;

        if (CurrentAmmo <= 0) {
            if (isAimingDownSights == false)
                WeaponAnimator.Play("IdleEmptyState");
            else
                WeaponAnimator.Play("AimEmptyState");
        }
        else {
            if (isAimingDownSights == false)
                WeaponAnimator.Play("IdleState");
            else
                WeaponAnimator.Play("AimState");
        }
    }

    protected void PlayAimDownSightsAnimation()
    {
        if (WeaponAnimator == null)
            return;

        if (CurrentAmmo > 0)
            WeaponAnimator.Play("AimDownSights");
        else
            WeaponAnimator.Play("AimDownSightsEmpty");
    }

    protected void PlayAimFromHipAnimation()
    {
        if (WeaponAnimator == null)
            return;

        if (CurrentAmmo > 0)
            WeaponAnimator.Play("AimFromHip");
        else
            WeaponAnimator.Play("AimFromHipEmpty");
    }

    protected void PlayIdleAnimation()
    {
        if (WeaponAnimator == null)
            return;

        if (CurrentAmmo > 0)
            WeaponAnimator.Play("IdleState");
        else
            WeaponAnimator.Play("IdleEmptyState");
    }

    protected void PlayReloadAnimation()
    {
        if (WeaponAnimator == null)
            return;

        if (CurrentAmmo > 0)
            WeaponAnimator.Play("Reload");
        else
            WeaponAnimator.Play("ReloadEmpty");
    }

    /* FX */
    protected void PlayDryFireFX()
    {
        if (DryFireSFX && CurrentAmmo <= 0)
            DryFireSFX.Play();
    }

    protected void PlayGunshotFX()
    {
        if (GunShotSFX)
            GunShotSFX.Play();

        if (CasingPFX)
            CasingPFX.Play();

        MuzzleFlashObj.SetActive(true);
    }

    protected void PlayReloadFX()
    {
        if (UnloadSFX)
            UnloadSFX.Play();

        if (ReloadSFX)
            ReloadSFX.Play();
    }

    protected void CrosshairCast()
    {
        // Raycast from the weapon muzzle towards the point returned from the first ray
        Vector3 aimPoint = CameraCastForPosition();
        Vector3 reticleRand = RandDirectionFromCircle();
        Vector3 attackDir = (aimPoint - WeaponObj.transform.position) + reticleRand;
        RaycastHit hit;
        Ray attackRay = new Ray(MuzzlePoint.position, attackDir);

        if (bDebug)
            Debug.DrawRay(attackRay.origin, attackRay.direction * BaseRange, Color.red, 3.0f);

        // React to the collided object's tag
        if (Physics.Raycast(attackRay, out hit, BaseRange)) {
            if (hit.collider.gameObject.tag == "Environment")
                Instantiate(BulletHoleObj, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));

            if (hit.collider.gameObject.tag == "Target")
                HitTargetWithShot(hit);

            if (hit.collider.gameObject.tag == "ZombieBody")
                HitZombieBody(hit);

            if (hit.collider.gameObject.tag == "ZombieHead")
                HitZombieHead(hit);

            if (hit.collider.gameObject.tag == "Turret")
                HitTurretWithShot(hit);
        }
    }

    // Raycast from player's view to find out what's under the cross hair, gets the point if something is within raycast distance.
    private Vector3 CameraCastForPosition()
    {
        Vector3 rayOrigin = new Vector3(0.5f, 0.5f, 0.0f);
        Ray ray = Camera.main.ViewportPointToRay(rayOrigin);
        RaycastHit hit;
        Vector3 aimPoint = ray.origin + ray.direction * BaseRange;

        if (bDebug)
            Debug.DrawRay(ray.origin, ray.direction * BaseRange, Color.red, 3.0f);

        if (Physics.Raycast(ray, out hit, BaseRange))
            aimPoint = hit.point;

        return aimPoint;
    }

    // Add some randomness to the shot's direction. Reduced when aiming.
    private Vector3 RandDirectionFromCircle()
    {
        float randRadius = ProjectileSpreadRadius;

        if (isAimingDownSights == true)
            randRadius *= 0.5f;

        Vector2 circleRand = Random.insideUnitCircle * randRadius;
        Vector3 reticleRand = new Vector3(0.0f, circleRand.y, circleRand.x);

        return reticleRand;
    }

    private void HitTargetWithShot(RaycastHit hit)
    {
        // Create an instance of a bullet hole as a child of the target, at the postion the shot hit
        Target targetObj = hit.collider.gameObject.GetComponent<Target>();
        GameObject bhObj = Instantiate(BulletHoleMetal, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
        bhObj.transform.SetParent(hit.transform.GetChild(0));
        bhObj.transform.position = new Vector3(hit.transform.GetChild(0).position.x - 0.001f, bhObj.transform.position.y, bhObj.transform.position.z);

        targetObj.GetComponent<Target>().UpdateDisplayScore((int)BaseDamage);

        // Move the actual target when shot, depending on which type of track the target is on and where the shot landed. If there is no track, just lower the target (stationary targets)
        if (targetObj.TargetTrack != null) {
            if (targetObj.TargetTrack.isMovingOnX)
                targetObj.LowerTarget();

            if (targetObj.TargetTrack.isMovingOnY && bhObj.transform.localPosition.x >= 0.0f)
                targetObj.RotateTargetLeft();

            if (targetObj.TargetTrack.isMovingOnY && bhObj.transform.localPosition.x < 0.0f)
                targetObj.RotateTargetRight();
        }
        else
            targetObj.LowerTarget();
    }

    private void HitZombieBody(RaycastHit hit)
    {
        hit.collider.GetComponent<HealthComponent>().ReduceHealth(BaseDamage);
        hit.collider.gameObject.GetComponent<Zombie>().BulletImpactSFX.Play();
        BloodPFX.transform.position = hit.point;
        BloodPFX.Play();
    }

    private void HitZombieHead(RaycastHit hit)
    {
        hit.collider.GetComponent<ZombieHeadTrigger>().UpdateHealth(BaseDamage * 3);
        hit.collider.GetComponent<ZombieHeadTrigger>().HeadshotImpactSFX.Play();
        BloodPFX.transform.position = hit.point;
        BloodPFX.Play();
    }

    private void HitTurretWithShot(RaycastHit hit)
    {
        hit.collider.GetComponent<HealthComponent>().TakeDamage(BaseDamage);
        hit.collider.gameObject.GetComponent<Turret>().BulletImpactSFX.Play();
    }
}
