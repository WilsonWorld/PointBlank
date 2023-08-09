/* Created by Wilson World Games, September 2022 */

using System.Collections;
using UnityEngine;

public class Sniper : Firearm
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
        AmmoUI.ChangeToSniperOutline(CurrentAmmo);
    }

    protected override void Reload()
    {
        base.Reload();

        if (AmmoUI)
            AmmoUI.ResetSniperIcons(CurrentAmmo);
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
