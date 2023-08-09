/* Created by Wilson World Games, September 2022 */

using System.Collections;
using UnityEngine;

public class Magnum : Firearm
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
        AmmoUI.ChangeToMagnumOutline(CurrentAmmo);
    }

    protected override void Reload()
    {
        base.Reload();

        if (AmmoUI)
            AmmoUI.ResetMagnumIcons(CurrentAmmo);
    }

    protected override IEnumerator OnReload()
    {
        StartCoroutine(base.OnReload());

        yield return new WaitForSeconds(0.5f);
        PlayReloadFX();

        yield return new WaitForSeconds(2.615f);
        Reload();
        isReloading = false;

        yield return new WaitForSeconds(0.5f);
        //PlayIdleAnimation();
    }
}
