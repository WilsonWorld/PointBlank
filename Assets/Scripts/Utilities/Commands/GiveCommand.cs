using System;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "New Log Command", menuName = "Utilities/DeveloperConsole/Commands/Give Command")]

public class GiveCommand : ConsoleCommand
{
    public override bool Process(string[] args)
    {
        if (args.Length != 1) { return false; }

        string desiredItemType = args[0];

        if (desiredItemType.Equals("weapons", StringComparison.OrdinalIgnoreCase) ||
            desiredItemType.Equals("Weapons", StringComparison.OrdinalIgnoreCase)) {

            UnlockWeapons();
            return true;
        }
        else if (desiredItemType.Equals("ammo", StringComparison.OrdinalIgnoreCase) ||
                    desiredItemType.Equals("Ammo", StringComparison.OrdinalIgnoreCase)) {

            ReplenishAmmo();
            return true;
        }
        else if (desiredItemType.Equals("health", StringComparison.OrdinalIgnoreCase) ||
                    desiredItemType.Equals("Health", StringComparison.OrdinalIgnoreCase))
        {

            ReplenishHealth();
            return true;
        }
        else
            return false;
    }

    // Unlocks all weapons from the inventory and equips the pistol by default
    private void UnlockWeapons()
    {
        GameObject PlayerObject = GameObject.Find("FPSPlayerController");
        Inventory PlayerInventoryRef = PlayerObject.GetComponent<Inventory>();

        if (PlayerInventoryRef == null)
            return;

        PlayerInventoryRef.UnlockSniper();
        PlayerInventoryRef.UnlockRifle();
        PlayerInventoryRef.UnlockShotgun();
        PlayerInventoryRef.UnlockMagnum();
        PlayerInventoryRef.UnlockPistol();

        PlayerInventoryRef.ActivateAmmoUI();
        PlayerInventoryRef.EquipPistol();
        Debug.Log("Weapons Unlocked");
    }

    // Sets all storage ammo to the maximum amount
    private void ReplenishAmmo()
    {
        GameObject PlayerObject = GameObject.Find("FPSPlayerController");
        Inventory PlayerInventoryRef = PlayerObject.GetComponent<Inventory>();

        if (PlayerInventoryRef == null)
            return;

        foreach (var weapon in PlayerInventoryRef.m_Weapons){
            int maxAmmo = weapon.StoredAmmoMax - weapon.StoredAmmoCount;
            weapon.AddStorageAmmo(maxAmmo);
        }
        Debug.Log("Ammo added for all weapons");
    }

    // Sets health and armor values to their max
    private void ReplenishHealth()
    {
        GameObject PlayerObject = GameObject.Find("FPSPlayerController");
        HealthComponent PlayerHealthRef = PlayerObject.GetComponent<HealthComponent>();

        if (PlayerHealthRef == null)
            return;

        PlayerHealthRef.RestoreHealth(PlayerHealthRef.MaxHealth);
        PlayerHealthRef.RestoreArmor(PlayerHealthRef.MaxArmor);
        Debug.Log("Armor and health have been restored");
    }
}
