/* Created by Wilson World Games, August 2022 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class Inventory : MonoBehaviour
{
    [Header("Weapon Inventory")]
    public List<Firearm> m_Weapons;
    public GameObject m_CurrentWeapon;
    public GameObject m_PreviousWeapon;
    [SerializeField] GameObject m_GrenadePrefab;
    [SerializeField] int m_GrenadeCount;
    const int m_GrenadeMax = 3;

    [Header("Item Inventory")]
    public List<Key> m_Keys;

    [Header("Bone IK Constraint")]
    public TwoBoneIKConstraint m_RightHandConstraint;
    public TwoBoneIKConstraint m_LeftHandConstraint;
    public RigBuilder m_RigBuilder;
    public Transform m_DefaultGripRight;
    public Transform m_DefaultGripLeft;
    public Transform m_PistolGripRight;
    public Transform m_PistolGripLeft;
    public Transform m_MagnumGripRight;
    public Transform m_MagnumGripLeft;
    public Transform m_ShotgunGripRight;
    public Transform m_ShotgunGripLeft;
    public Transform m_RifleGripRight;
    public Transform m_RifleGripLeft;
    public Transform m_SniperGripRight;
    public Transform m_SniperGripLeft;

    // Private class variables
    bool m_PistolUnlocked = false;
    bool m_MagnumUnlocked = false;
    bool m_RifleUnlocked = false;
    bool m_ShotgunUnlocked = false;
    bool m_SniperUnlocked = false;

    void Update()
    {
        if (Input.GetButtonDown("SwapWeapons"))
            SwapWeapons();

        if (Input.GetButtonUp("Grenade"))
            ThrowGrenade();

        if (Input.GetKeyDown(KeyCode.Alpha1))
            EquipPistol();

        if (Input.GetKeyDown(KeyCode.Alpha2))
            EquipMagnum();

        if (Input.GetKeyDown(KeyCode.Alpha3))
            EquipShotgun();

        if (Input.GetKeyDown(KeyCode.Alpha4))
            EquipRifle();

        if (Input.GetKeyDown(KeyCode.Alpha5))
            EquipSniper();
    }

    // Check if the player has grenades to throw. If so, reduce count by 1 and change the UI to match before spawning in a grenade
    void ThrowGrenade()
    {
        if (m_GrenadeCount <= 0)
            return;

        m_GrenadeCount--;

        if (m_GrenadeCount > 0)
            m_Weapons[0].AmmoUI.RemoveFromGrenadeDisplay(m_GrenadeCount);
        else {
            m_Weapons[0].AmmoUI.RemoveFromGrenadeDisplay(m_GrenadeCount);
            m_Weapons[0].AmmoUI.DisableGrenadeDisplay();
        }

        SpawnGrenade();
    }

    // Spawn in a grenade from a prefab and adds force in the direction the player is facing
    void SpawnGrenade()
    {
        Vector3 spawnPos = transform.position + (transform.forward * 2) + transform.up;
        GameObject grenade = Instantiate(m_GrenadePrefab, spawnPos, Quaternion.identity);
        Vector3 addedForce = gameObject.transform.forward * 1500.0f;
        grenade.GetComponent<Rigidbody>().AddForce(addedForce);
    }

    // Swaps the Current and Previous Weapons
    void SwapWeapons()
    {
        if (m_CurrentWeapon == null)
            return;
    }

    // Turn off the weapon game object and UI elements
    void RemoveAllWeapons()
    {
        SetWeaponGripTargets(null, null);

        for (int i =0; i < m_Weapons.Capacity; i++) {
            m_Weapons[i].AmmoUI.transform.GetChild(i + 2).gameObject.SetActive(false);
            m_Weapons[i].gameObject.SetActive(false);
        }
    }

    // Activate the Pistol game object and UI elements
    public void EquipPistol()
    {
        if (m_PistolUnlocked == false)
            return;

        RemoveAllWeapons();
        SetWeaponGripTargets(m_PistolGripRight, m_PistolGripLeft);
        m_CurrentWeapon = m_Weapons[0].gameObject;
        m_Weapons[0].gameObject.SetActive(true);
        m_Weapons[0].AmmoUI.SetWeaponDisplayIcon(m_Weapons[0].WeaponIconUI);
        m_Weapons[0].AmmoUI.transform.GetChild(2).gameObject.SetActive(true);
    }

    // Activate the Magnum game object and UI elements
    public void EquipMagnum()
    {
        if (m_MagnumUnlocked == false)
            return;

        RemoveAllWeapons();
        SetWeaponGripTargets(m_MagnumGripRight, m_MagnumGripLeft);
        m_CurrentWeapon = m_Weapons[1].gameObject;
        m_Weapons[1].gameObject.SetActive(true);
        m_Weapons[1].AmmoUI.SetWeaponDisplayIcon(m_Weapons[1].WeaponIconUI);
        m_Weapons[1].AmmoUI.transform.GetChild(3).gameObject.SetActive(true);
    }

    // Activate the Shotgun game object and UI elements
    public void EquipShotgun()
    {
        if (m_ShotgunUnlocked == false)
            return;

        RemoveAllWeapons();
        SetWeaponGripTargets(m_ShotgunGripRight, m_ShotgunGripLeft);
        m_CurrentWeapon = m_Weapons[2].gameObject;
        m_Weapons[2].gameObject.SetActive(true);
        m_Weapons[2].AmmoUI.SetWeaponDisplayIcon(m_Weapons[2].WeaponIconUI);
        m_Weapons[2].AmmoUI.transform.GetChild(4).gameObject.SetActive(true);
    }

    // Activate the Rifle game object and UI elements
    public void EquipRifle()
    {
        if (m_RifleUnlocked == false)
            return;

        RemoveAllWeapons();
        SetWeaponGripTargets(m_RifleGripRight, m_RifleGripLeft);
        m_CurrentWeapon = m_Weapons[3].gameObject;
        m_Weapons[3].gameObject.SetActive(true);
        m_Weapons[3].AmmoUI.SetWeaponDisplayIcon(m_Weapons[3].WeaponIconUI);
        m_Weapons[3].AmmoUI.transform.GetChild(5).gameObject.SetActive(true);
    }

    // Activate the Sniper game object and UI elements
    public void EquipSniper()
    {
        if (m_SniperUnlocked == false)
            return;

        RemoveAllWeapons();
        SetWeaponGripTargets(m_SniperGripRight, m_SniperGripLeft);
        m_CurrentWeapon = m_Weapons[4].gameObject;
        m_Weapons[4].gameObject.SetActive(true);
        m_Weapons[4].AmmoUI.SetWeaponDisplayIcon(m_Weapons[4].WeaponIconUI);
        m_Weapons[4].AmmoUI.transform.GetChild(6).gameObject.SetActive(true);
    }

    // Add a grenade to the counter if the player isn't already at the max
    public void AddGrenade() 
    {
        if (m_GrenadeCount == m_GrenadeMax)
            return;

        m_GrenadeCount++;

        if (m_Weapons[0].AmmoUI.GrenadeIconUI.activeSelf == true) {
            m_Weapons[0].AmmoUI.AddToGrenadeDisplay(m_GrenadeCount - 1);
        }
        else {
            m_Weapons[0].AmmoUI.EnableGrenadeDisplay();
            m_Weapons[0].AmmoUI.AddToGrenadeDisplay(m_GrenadeCount - 1);
        }
    }

    // Set the arm's target points to a different weapon's grip points and build the new rig
    void SetWeaponGripTargets(Transform rightGrip, Transform leftGrip)
    {
        if (rightGrip)
            m_RightHandConstraint.data.target = rightGrip;
        else
            m_RightHandConstraint.data.target = null;

        if (leftGrip)
            m_LeftHandConstraint.data.target = leftGrip;
        else
            m_LeftHandConstraint.data.target = null;

        m_RigBuilder.Build();
    }

    public void UnlockPistol() { m_PistolUnlocked = true; }
    public void UnlockMagnum() { m_MagnumUnlocked = true; }
    public void UnlockRifle() { m_RifleUnlocked = true; }
    public void UnlockShotgun() { m_ShotgunUnlocked = true; }
    public void UnlockSniper() { m_SniperUnlocked = true; }
}
