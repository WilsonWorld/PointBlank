/* Created by Wilson World Games, September 2022 */

using System.Collections;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    [Header("Grenade Settings")]
    public MeshRenderer GrenadeMesh;
    public GameObject ExplosionPFX;
    public float FuseTime = 4.0f;
    public float ExplosionRadius = 8.0f;
    public float ExplosionDamage = 200.0f;

    [Header("Effects")]
    public AudioSource ExplosionSFX;
    public AudioSource EnvironmentHitSFX;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ExplosionTimer());
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.tag == "Environment")
            EnvironmentHitSFX.Play();
    }

    void OnExplosion()
    {
        // Check for objects with Health Components in the explosion radius
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, ExplosionRadius);

        foreach (Collider collider in hitColliders) {
            // Check for targets in the explosion radius
            if (collider.gameObject.tag == "Target")
                HitTarget(collider);

            // Check for zombies in the explosion radius
            if (collider.gameObject.tag == "ZombieBody") {
                // Apply Explosion Damage to each object with a health component, reducing their health or killing them
                collider.GetComponent<HealthComponent>().ReduceHealth(ExplosionDamage);
                collider.gameObject.GetComponent<Zombie>().BulletImpactSFX.Play();
            }
        }
    }

    private void HitTarget(Collider collider)
    {
        Target targetObj = collider.gameObject.GetComponent<Target>();

        targetObj.GetComponent<Target>().UpdateDisplayScore((int)ExplosionDamage);

        if (targetObj.TargetTrack != null) {
            if (targetObj.TargetTrack.isMovingOnX)
                targetObj.LowerTarget();

            if (targetObj.TargetTrack.isMovingOnY)
                targetObj.RotateTargetLeft();
        }
        else
            targetObj.LowerTarget();
    }

    IEnumerator ExplosionTimer()
    {
        yield return new WaitForSeconds(FuseTime);

        gameObject.GetComponent<CapsuleCollider>().enabled = false;
        GrenadeMesh.enabled = false;
        ExplosionPFX.SetActive(true);
        ExplosionSFX.Play();
        OnExplosion();

        yield return new WaitForSeconds(3.0f);

        Destroy(gameObject);
    }
}
