/* Created by Wilson World Games, August 2022 */
/* The Firearm Pickup is an interactable pickup that is inherited from to unlock the use of a specified weapon. */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirearmPickup : Interactable
{
    public enum WeaponsList
    {
        WPN_PISTOL = 0,
        WPN_MAGNUM = 1,
        WPN_SHOTGUN = 2,
        WPN_RIFLE = 3,
        WPN_SNIPER = 4,
        WPN_MAX = 5
    }

    [Header("Firearm Settings")]
    public WeaponsList PickupWeapon;
    public GameObject AmmoDisplayObj;
    public GameObject BulletDisplayUI;
    public List <MeshRenderer> PickupRenderers;
    public AudioSource PickupSFX;
    public string PickupName;

    [SerializeField] Inventory PlayerInventory;

    protected void OnMouseOver()
    {
        ActivateDisplayUI();
        SetInteractionText("Pickup " + PickupName);

        if (Input.GetButtonDown("Interact") && Distance <= DistanceMax) {
            this.GetComponent<BoxCollider>().enabled = false;
            DeactivateDisplayUI();
            EquipFirearm();
            StartCoroutine(DestroyPickup());
        }
    }

    protected void OnMouseExit()
    {
        DeactivateDisplayUI();
    }

    // Ensures the Ammo & Bullet Displays are active, unlocks the pistol and equips it for use.
    void EquipFirearm()
    {
        AmmoDisplayObj.SetActive(true);
        BulletDisplayUI.SetActive(true);
        EquipFromInventory();
        PlayPickupFX();
    }

    void EquipFromInventory()
    {
        switch(PickupWeapon) {
            case WeaponsList.WPN_PISTOL:
                PlayerInventory.UnlockPistol();
                PlayerInventory.EquipPistol();
                break;

            case WeaponsList.WPN_MAGNUM:
                PlayerInventory.UnlockMagnum();
                PlayerInventory.EquipMagnum();
                break;

            case WeaponsList.WPN_SHOTGUN:
                PlayerInventory.UnlockShotgun();
                PlayerInventory.EquipShotgun();
                break;

            case WeaponsList.WPN_RIFLE:
                PlayerInventory.UnlockRifle();
                PlayerInventory.EquipRifle();
                break;

            case WeaponsList.WPN_SNIPER:
                PlayerInventory.UnlockSniper();
                PlayerInventory.EquipSniper();
                break;

            case WeaponsList.WPN_MAX:
                break;
        }
    }

    void PlayPickupFX()
    {
        if (PickupSFX)
            PickupSFX.Play();
    }

    // Disables the Pistol's mesh renderers and destroys the gameobject after a brief amount of time.
    IEnumerator DestroyPickup()
    {
        foreach (var render in PickupRenderers) {
            //render.gameObject.SetActive(false);
            render.enabled = false;
        }

        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);
    }
}
