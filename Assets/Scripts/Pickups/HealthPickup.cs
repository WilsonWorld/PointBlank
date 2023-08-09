/* Created by Wilson World Games, September 2022 */
/* The Armor Pickup is an interactable pickup that adds a specific amount to the player's health. */

using System.Collections;
using UnityEngine;

public class HealthPickup : Interactable
{
    [Header("Armor Settings")]
    public GameObject PlayerRef;
    public MeshRenderer PickupRenderer;
    public AudioSource PickupSFX;
    public int HealthAmount;

    private void OnMouseOver()
    {
        ActivateDisplayUI();
        SetInteractionText("Pickup Health");

        if (Input.GetButtonDown("Interact") && Distance <= DistanceMax)
        {
            DeactivateDisplayUI();
            IncreaseHealth();
        }
    }

    private void OnMouseExit()
    {
        DeactivateDisplayUI();
    }

    // Disable pickup collider, play pick up effects, and increase the player's health before starting the despawn/destroy timer.
    void IncreaseHealth()
    {
        this.GetComponent<BoxCollider>().enabled = false;
        PickupSFX.Play();
        PlayerRef.GetComponent<HealthComponent>().RestoreHealth(HealthAmount);
        StartCoroutine(DestroyPickup());
    }

    // Hides the mesh and destroys the pick up object after enough time has passed for the FX and logic to complete.
    IEnumerator DestroyPickup()
    {
        PickupRenderer.enabled = false;
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }
}
