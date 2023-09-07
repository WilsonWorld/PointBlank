/* Created by Wilson World Games, September 7, 2023 */
/* The Key script holds an access code and is stored in the player inventory in order to unlock doors and gateways that match the access code.  */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : Interactable
{
    [Header("Key Settings")]
    public Inventory PlayerInventoryRef;
    public int KeyCode = -1;

    private void OnMouseOver()
    {
        ActivateDisplayUI();
        SetInteractionText("Pickup Key");

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

    // Disable the pick up key object in the world and adds it to the player's inventory.
    void AddToInventory()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetComponent<BoxCollider>().enabled = false;

        if (PlayerInventoryRef != null )
            PlayerInventoryRef.m_Keys.Add(this);
    }
}
