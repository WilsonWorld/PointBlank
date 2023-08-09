/* Created by Wilson World Games, August 2022 */
/* The Zombie Head Trigger class detects when a headshot has occurred and reduces health with a bonus. Plays additional FX upon zombie death. */

using System.Collections;
using UnityEngine;

public class ZombieHeadTrigger : MonoBehaviour
{
    [Header("Trigger Settings")]
    public HealthComponent ZombieHealthComp;

    [Header("Headshot FX")]
    public AudioSource HeadshotImpactSFX;
    public ParticleSystem HeadshotDeathPFX;

    // Reduces the attached zombie's health and checks if the zombie has died
    public void UpdateHealth(float damage)
    {
        ZombieHealthComp.ReduceHealth(damage * 2.0f);

        if (ZombieHealthComp.IsDead == true)
            StartCoroutine(DisableHead());
    }

    // Removes the zombie's head and plays headshot FX
    IEnumerator DisableHead()
    {
        GetComponent<SkinnedMeshRenderer>().enabled = false;

        yield return new WaitForSeconds(0.1f);
        HeadshotDeathPFX.Play();

        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
    }
}
