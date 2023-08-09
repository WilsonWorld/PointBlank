/* Created by AaronWilson, Wilson World Games, July 17, 2023 */
/* The Enemy class is a base class for shared properties among all enemy entities. */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Settings")]
    public GameObject PlayerRef;
    public GameObject EnemyRef;
    public HealthComponent EnemyHealth;
    [SerializeField] protected float ActionRange = 16.0f;
    [SerializeField] protected float AttackDamage = 1.0f;
    [SerializeField] protected float AttackSpeed = 5.0f;
    [SerializeField] protected float DespawnTime = 10.0f;
    [SerializeField] protected float RotationSpeed = 1.0f; //The angle in radians

    protected RaycastHit m_RayHit;
    protected float TargetDistance;
    bool hasDetectedPlayer = false;
    bool hasSeenPlayer = false;
    bool isDead = false;
    bool canAttack = false;
    bool isAttacking = false;


    // Raycast forward for a player object, updating the TargetDistance if hit or resetting to 0 if not.
    protected virtual void DetectPlayer()
    {
        GameObject gObj = CastForObject();

        if (gObj == null)
            return;

        if (gObj.tag == "Player") {
            TargetDistance = m_RayHit.distance;
            hasSeenPlayer = true;
        }
        else {
            TargetDistance = 0.0f;
            hasSeenPlayer = false;
            return;
        }
    }

    protected virtual void CheckPlayerRangeForAttack()
    {
        if (TargetDistance < ActionRange && TargetDistance > 0)
            AttackTriggered = true;
        else
            AttackTriggered = false;
    }

    // When in attack range, activates an attack that raycasts for the player, reducing their health if hit
    protected virtual void AttackPlayer() { }

    // Set the enemy to a dead state, playing the associated effects and started the despawn/destroy timer.
    protected virtual void TriggerDeath()
    {
        DeathTriggered = true;
    }

    // Checks if the enenmy has already triggered death. Ensures death only occurs once.
    protected virtual void OnDeath() { }

    // Destroys the enemy object after a spefied time
    protected IEnumerator DestroyEnemy()
    {
        yield return new WaitForSeconds(DespawnTime);
        Destroy(gameObject);
    }

    // Calculates a rotation a turn closer to the target and applies the rotation to the swivel object
    protected void SmoothLookRotation()
    {
        Vector3 targetDirection = PlayerRef.transform.position - transform.position;  // Determine direction to rotate towards
        float turnAmount = RotationSpeed * Time.deltaTime;  // The turn size is equal to speed times frame rate
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDirection, turnAmount, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDir);
    }

    protected void SmoothLookRotation(GameObject RotatingObject)
    {
        Vector3 targetDirection = PlayerRef.transform.position - RotatingObject.transform.position;  // Determine direction to rotate towards
        float turnAmount = RotationSpeed * Time.deltaTime;  // The turn size is equal to speed times frame rate
        Vector3 newDir = Vector3.RotateTowards(RotatingObject.transform.forward, targetDirection, turnAmount, 0.0f);
        RotatingObject.transform.rotation = Quaternion.LookRotation(newDir);
    }

    // Raycast from the forward of the enemy, returning either a hit object or null
    protected GameObject CastForObject()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out m_RayHit, ActionRange))
            return m_RayHit.collider.gameObject;
        else
            return null;
    }

    protected GameObject CastForObject(GameObject castingObj)
    {
        if (Physics.Raycast(castingObj.transform.position, castingObj.transform.TransformDirection(Vector3.forward), out m_RayHit, ActionRange))
            return m_RayHit.collider.gameObject;
        else
            return null;
    }

    protected GameObject CastForObject(int layerMask)
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out m_RayHit, ActionRange, layerMask))
            return m_RayHit.collider.gameObject;
        else
            return null;
    }

    protected GameObject CastForObject(GameObject castingObj, int layerMask)
    {
        if (Physics.Raycast(castingObj.transform.position, castingObj.transform.TransformDirection(Vector3.forward), out m_RayHit, ActionRange, layerMask))
            return m_RayHit.collider.gameObject;
        else
            return null;
    }

    /* Public Getter and Setter for other classes to access necessary variables */
    public bool DetectedPlayer
    {
        get { return hasDetectedPlayer; }
        set { hasDetectedPlayer = value; }
    }

    public bool SeesPlayer
    {
        get { return hasSeenPlayer; }
        set { hasSeenPlayer = value; }
    }

    public bool DeathTriggered
    {
        get { return isDead; }
        set { isDead = value; }
    }

    public bool AttackTriggered
    {
        get { return canAttack; }
        set { canAttack = value; }
    }

    public bool Attacking
    {
        get { return isAttacking; }
        set { isAttacking = value; }
    }
}