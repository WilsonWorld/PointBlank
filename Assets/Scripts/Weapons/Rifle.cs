/* Created by Wilson World Games, August 2022 */

using System.Collections;
using UnityEngine;

public class Rifle : Firearm
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void FireWeapon()
    {
        base.FireWeapon();
        AmmoUI.ChangeToRifleOutline(CurrentAmmo);
    }

    protected override void Reload()
    {
        base.Reload();

        if (AmmoUI)
            AmmoUI.ResetRifleIcons(CurrentAmmo);
    }

    protected override IEnumerator OnReload()
    {
        StartCoroutine(base.OnReload());
        PlayReloadFX();

        yield return new WaitForSeconds(2.8f);
        Reload();
        isReloading = false;

        yield return new WaitForSeconds(0.1f);
        //PlayIdleAnimation();
    }
}
