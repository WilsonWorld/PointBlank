/* Created by Wilson World Games, August 2022 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    public GameObject LightObj;

    bool IsActive = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
            ToggleLight();
    }

    void ToggleLight()
    {
        if (IsActive == false) {
            IsActive = true;
            LightObj.SetActive(true);
        }
        else {
            IsActive = false;
            LightObj.SetActive(false);
        }
    }
}
