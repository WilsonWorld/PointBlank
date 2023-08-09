/* Created by Wilson World Games, August 2022 */
/* The Ammo Pickup is an interactable pickup that adds a specific ammunition amount to the player's storage for a specified weapon. */

using System.Collections;
using UnityEngine;

public class AmmoPickup : Interactable
{
    [Header("Ammo Settings")]
    public Firearm FirearmObject;
    public GameObject BulletMeshObj;
    public MeshRenderer PickupRenderer;
    public AudioSource PickupSFX;
    public int AmmoAmount;

    private void OnMouseOver()
    {
        ActivateDisplayUI();
        SetInteractionText("Pickup Ammo");

        if (Input.GetButtonDown("Interact") && Distance <= DistanceMax) {
            DeactivateDisplayUI();
            IncreaseAmmo();
        }
    }

    private void OnMouseExit()
    {
        DeactivateDisplayUI();
    }

    // Disable pickup collider, play pick up effects, and increase the storage ammo for the firearm before starting the despawn/destroy timer.
    void IncreaseAmmo()
    {
        this.GetComponent<BoxCollider>().enabled = false;
        PickupSFX.Play();
        FirearmObject.AddStorageAmmo(AmmoAmount);
        StartCoroutine(DestroyPickup());
    }

    // Hides the mesh and destroys the pick up object after enough time has passed for the FX and logic to complete.
    IEnumerator DestroyPickup()
    {
        if (PickupRenderer)
            PickupRenderer.enabled = false;

        if (BulletMeshObj)
            BulletMeshObj.SetActive(false);

        yield return new WaitForSeconds(0.1f);
        Destroy(transform.parent.gameObject);
    }
}
