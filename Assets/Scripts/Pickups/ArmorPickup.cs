/* Created by Wilson World Games, September 2022 */
/* The Armor Pickup is an interactable pickup that adds a specific amount to the player's armor. */

using System.Collections;
using UnityEngine;

public class ArmorPickup : Interactable
{
    [Header("Armor Settings")]
    public GameObject PlayerRef;
    public MeshRenderer PickupRenderer;
    public MeshRenderer PickupRendererChild;
    public AudioSource PickupSFX;
    public int ArmorAmount;

    private void OnMouseOver()
    {
        ActivateDisplayUI();
        SetInteractionText("Pickup Armor");

        if (Input.GetButtonDown("Interact") && Distance <= DistanceMax)
        {
            DeactivateDisplayUI();
            IncreaseArmor();
        }
    }

    private void OnMouseExit()
    {
        DeactivateDisplayUI();
    }

    // Disable pickup collider, play pick up effects, and increase the player's armor before starting the despawn/destroy timer.
    void IncreaseArmor()
    {
        this.GetComponent<BoxCollider>().enabled = false;
        PickupSFX.Play();
        PlayerRef.GetComponent<HealthComponent>().RestoreArmor(ArmorAmount);
        StartCoroutine(DestroyPickup());
    }

    // Hides the mesh and destroys the pick up object after enough time has passed for the FX and logic to complete.
    IEnumerator DestroyPickup()
    {
        PickupRenderer.enabled = false;
        PickupRendererChild.enabled = false;
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }
}
