/* Created by Wilson World Games, August 2022 */

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoDisplay : MonoBehaviour
{
    [Header("UI Bullet Icons")]
    public List<RawImage> PistolBulletIconsUI;
    public List<RawImage> MagnumBulletIconsUI;
    public List<RawImage> RifleBulletIconsUI;
    public List<RawImage> ShotgunShellIconsUI;
    public List<RawImage> SniperBulletIconsUI;

    [Header("References")]
    public GameObject AmmoDisplayUI;
    public GameObject StoredDisplayUI;
    public GameObject WeaponIconUI;
    public GameObject GrenadeIconUI;

    [Header("Properties")]
    public Color IconFill;
    public Color IconOutline;
    public static int AmmoCount;
    public static int StoredAmmoCount;

    private void Update()
    {
        AmmoDisplayUI.GetComponent<Text>().text = AmmoCount.ToString();
        StoredDisplayUI.GetComponent<Text>().text = StoredAmmoCount.ToString();
    }

    public void ChangeToPistolOutline(int index)
    {
        PistolBulletIconsUI[index].color = IconOutline;
    }

    public void ResetPistolIcons(int amount)
    {
        for (int i = 0; i < amount; i++) {
            PistolBulletIconsUI[i].color = IconFill;
        }
    }

    public void ChangeToMagnumOutline(int index)
    {
        MagnumBulletIconsUI[index].color = IconOutline;
    }

    public void ResetMagnumIcons(int amount)
    {
        for (int i = 0; i < amount; i++) {
            MagnumBulletIconsUI[i].color = IconFill;
        }
    }

    public void ChangeToRifleOutline(int index)
    {
        RifleBulletIconsUI[index].color = IconOutline;
    }

    public void ResetRifleIcons(int amount)
    {
        for (int i = 0; i < amount; i++) {
            RifleBulletIconsUI[i].color = IconFill;
        }
    }

    public void ChangeToShotgunOutline(int index)
    {
        ShotgunShellIconsUI[index].color = IconOutline;
    }

    public void ResetShotgunIcons(int amount)
    {
        for (int i = 0; i < amount; i++) {
            ShotgunShellIconsUI[i].color = IconFill;
        }
    }

    public void ChangeToSniperOutline(int index)
    {
        SniperBulletIconsUI[index].color = IconOutline;
    }

    public void ResetSniperIcons(int amount)
    {
        for (int i = 0; i < amount; i++) {
            SniperBulletIconsUI[i].color = IconFill;
        }
    }

    public void SetWeaponDisplayIcon(Texture gunImage)
    {
        WeaponIconUI.transform.GetChild(0).GetComponent<RawImage>().texture = gunImage;
    }

    public void EnableGrenadeDisplay()
    {
        GrenadeIconUI.gameObject.SetActive(true);
    }

    public void DisableGrenadeDisplay()
    {
        GrenadeIconUI.gameObject.SetActive(false);
    }

    public void AddToGrenadeDisplay(int index)
    {
        GrenadeIconUI.transform.GetChild(index).gameObject.SetActive(true);
    }

    public void RemoveFromGrenadeDisplay(int index)
    {
        GrenadeIconUI.transform.GetChild(index).gameObject.SetActive(false);
    }
}
