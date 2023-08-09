/* Created by Wilson World Games, August 2022 */
/* The Zombie class is a specific enemy type that can detect, move to, and attack the player. The Zombie can also die. */

using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : Enemy
{
    [Header("Zombie Settings")]
    [SerializeField] private float MovementSpeed = 2.0f;
    [SerializeField] protected float AttackRange = 6.0f;

    [Header("Effects Settings")]
    [SerializeField] private CameraShake CameraShakeFX;
    [SerializeField] private AudioSource BulletImpact;
    [SerializeField] private AudioSource ZombieDeath;
    [SerializeField] private AudioSource ZombieAttack;

    NavMeshAgent m_ZombieNav;
    Animator m_EnemyAnimator;

    private void Awake()
    {
        m_ZombieNav = GetComponent<NavMeshAgent>();
        m_ZombieNav.speed = MovementSpeed;
        m_EnemyAnimator = EnemyRef.GetComponent<Animator>();
    }

    protected void Update()
    {
        if (EnemyHealth.IsDead == true) {
            OnDeath();
            return;
        }

        if (DetectedPlayer == false)
            return;
        else {
            SmoothLookRotation();
            DetectPlayer();
        }

        if (SeesPlayer == false)
            return;
        else {
            MoveToPlayer();
            CheckPlayerRangeForAttack();
            AttackPlayer();
        }
    }

    // While not in attack range, check if the player's position is within range of moving the zombie. If not, set the zombie to Idle
    void MoveToPlayer()
    {
        if (AttackTriggered == true)
            return;
    
        if (TargetDistance < ActionRange && TargetDistance > 0) {
            m_EnemyAnimator.Play("Z_Walk_InPlace");
            m_ZombieNav.speed = 2.0f;
            m_ZombieNav.destination = PlayerRef.transform.position;
        }
        else {
            m_ZombieNav.speed = 0;
            m_EnemyAnimator.Play("Z_Idle");
        }
    }

    protected override void CheckPlayerRangeForAttack()
    {
        if (TargetDistance < AttackRange && TargetDistance > 1)
            AttackTriggered = true;
        else
            AttackTriggered = false;
    }

    // When in attack range, activates an attack that raycasts for the player, reducing their health if hit
    protected override void AttackPlayer()
    {
        if (AttackTriggered == true && Attacking == false) {
            Attacking = true;
            int layerMask = 1 << 8;
            GameObject gObj = CastForObject(layerMask);

            if (gObj.GetComponent<HealthComponent>())
                gObj.GetComponent<HealthComponent>().TakeDamage(AttackDamage);

            m_ZombieNav.speed = 0;
            StartCoroutine(AttackAnimation());
        }
    }

    // Handles the main death functions once, when death has been triggered
    protected override void OnDeath()
    {
        base.OnDeath();
        if (DeathTriggered == true)
            return;

        TriggerDeath();
        DisableZombieColliders();
    }

    // Disables the zombie's colliders
    void DisableZombieColliders()
    {
        SphereCollider sComp = transform.GetChild(0).GetChild(2).GetComponent<SphereCollider>();
        sComp.enabled = false;

        CapsuleCollider ccComp = transform.GetComponent<CapsuleCollider>();
        ccComp.attachedRigidbody.isKinematic = true;
        ccComp.enabled = false;
    }

    // Set the zombie to a dead state, playing the associated effects and started the despawn/destroy timer.
    protected override void TriggerDeath()
    {
        base.TriggerDeath();
        m_ZombieNav.speed = 0;
        ZombieDeath.Play();
        EnemyRef.GetComponent<Animator>().Play("Z_FallingBack");
        StartCoroutine(DestroyEnemy());
    }

    // Controls what animations are played at specific times and applies damage FX to the player controller
    IEnumerator AttackAnimation()
    {
        m_EnemyAnimator.Play("PB-Z_IdleToAttack");

        yield return new WaitForSeconds(0.3f);
        PlayAttackSFX();

        yield return new WaitForSeconds(0.3f);
        m_EnemyAnimator.Play("Z_Attack");
        PlayAttackSFX();
        CameraShakeFX.ShakeCamera();

        yield return new WaitForSeconds(1.0f);
        m_EnemyAnimator.Play("PB-Z_AttackToIdle");

        yield return new WaitForSeconds(0.5f);
        m_EnemyAnimator.Play("Z_Idle");
        float randResetTime = Random.Range(1.0f, 3.0f);

        yield return new WaitForSeconds(randResetTime / AttackSpeed);
        EnemyRef.transform.localRotation = Quaternion.Euler(Vector3.zero);
        Attacking = false;
    }

    void PlayAttackSFX()
    {
        ZombieAttack.time = 0.4f;
        ZombieAttack.Play();
    }

    public AudioSource BulletImpactSFX
    {
        get { return BulletImpact; }
    }
}
