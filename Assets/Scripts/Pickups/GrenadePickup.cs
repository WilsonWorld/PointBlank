/* Created by Wilson World Games, September 2022 */
/* The Grenade Pickup is an interactable pickup that increases the grenade counter in the player's inventory. */

using System.Collections;
using UnityEngine;

public class GrenadePickup : Interactable
{
    [Header("Grenade Settings")]
    public Inventory PlayerInventory;
    public MeshRenderer BaseMesh;
    public MeshRenderer PinMesh;
    public MeshRenderer LoopMesh;
    public AudioSource PickupSFX;

    private void OnMouseOver()
    {
        ActivateDisplayUI();
        SetInteractionText("Pickup Grenade");

        if (Input.GetButtonDown("Interact") && Distance <= DistanceMax)
        {
            DeactivateDisplayUI();
            AddToInventory();
        }
    }

    private void OnMouseExit()
    {
        DeactivateDisplayUI();
    }

    void AddToInventory()
    {
        this.GetComponent<BoxCollider>().enabled = false;
        PickupSFX.Play();
        PlayerInventory.AddGrenade();

        StartCoroutine(DestroyPickup());
    }

    IEnumerator DestroyPickup()
    {
        BaseMesh.enabled = false;
        PinMesh.enabled = false;
        LoopMesh.enabled = false;

        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }
}
