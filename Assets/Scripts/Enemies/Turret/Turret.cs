/* Created by AaronWilson, Wilson World Games, July 17, 2023 */
/* The Turret is a specific enemy type that can detect and attack the player. The Turret can also die. */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : Enemy
{
    [Header("Turret Settings")]
    public GameObject SwivelObj;
    public GameObject ExplosionFlash;
    public GameObject TurretLight;
    public Color RedLight;
    public Color GreenLight;

    [Header("Effects Settings")]
    [SerializeField] private CameraShake CameraShakeFX;
    [SerializeField] private AudioSource BulletImpact;
    [SerializeField] private AudioSource TurretDeath;
    [SerializeField] private AudioSource TurretAttack;

    protected void Update()
    {
        if (EnemyHealth.IsDead == true) {
            OnDeath();
            return;
        }

        if (DetectedPlayer == false)
        {
            if (SeesPlayer == false)
                return;

            TargetDistance = 0.0f;
            SeesPlayer = false;
            UpdateTurretLight(SeesPlayer);
            return;
        }
        else
        {
            SmoothLookRotation(SwivelObj);
            DetectPlayer();
        }

        if (SeesPlayer == false)
            return;
        else {
            CheckPlayerRangeForAttack();
            AttackPlayer();
        }
    }

    protected override void DetectPlayer()
    {
        GameObject gObj = CastForObject(SwivelObj);

        if (gObj == null)
            return;

        if (gObj.tag == "Player") {
            TargetDistance = m_RayHit.distance;
            SeesPlayer = true;
            UpdateTurretLight(SeesPlayer);
        }
    }

    // When in attack range, activates an attack that raycasts for the player, reducing their health if hit
    protected override void AttackPlayer()
    {
        if (AttackTriggered == true && Attacking == false) {
            Attacking = true;
            int layerMask = 1 << 8;
            GameObject gObj = CastForObject(SwivelObj, layerMask);

            if (gObj == null) {
                Attacking = false;
                return;
            }

            if (gObj.GetComponent<HealthComponent>())
                gObj.GetComponent<HealthComponent>().TakeDamage(AttackDamage);

            StartCoroutine(AttackAnimation());
        }
    }

    // Controls what animations are played at specific times and applies damage FX to the player controller
    IEnumerator AttackAnimation()
    {
        PlayAttackSFX();
        CameraShakeFX.ShakeCamera();

        float randResetTime = Random.Range(1.0f, 3.0f);
        yield return new WaitForSeconds(randResetTime / AttackSpeed);
        Attacking = false;
    }

    // Handles the main death functions once, when death has been triggered
    protected override void OnDeath()
    {
        base.OnDeath();
        if (DeathTriggered == true)
            return;

        TriggerDeath();
    }

    // Set the zombie to a dead state, playing the associated effects and started the despawn/destroy timer.
    protected override void TriggerDeath()
    {
        base.TriggerDeath();
        SwivelObj.SetActive(false);
        TurretDeath.Play();
        //StartCoroutine(DeathAnimation());
        StartCoroutine(DestroyEnemy());
    }

    void PlayAttackSFX()
    {
        //TurretAttack.time = 0.4f;
        TurretAttack.Play();
    }

    // Changes the door light from red to green, showing it's been opened
    void UpdateTurretLight(bool seePlayer)
    {
        if (seePlayer == true) {
            TurretLight.GetComponent<MeshRenderer>().material.color = GreenLight;
            TurretLight.GetComponent<Light>().color = GreenLight;
        }
        else {
            TurretLight.GetComponent<MeshRenderer>().material.color = RedLight;
            TurretLight.GetComponent<Light>().color = RedLight;
        }
    }

    IEnumerator DeathAnimation()
    {
        ExplosionFlash.SetActive(true);
        yield return new WaitForSeconds(0.15f);
        ExplosionFlash.SetActive(false);
    }

    public AudioSource BulletImpactSFX
    {
        get { return BulletImpact; }
    }

}