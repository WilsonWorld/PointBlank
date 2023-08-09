/* Created by Wilson World Games, August 2022 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCasting : MonoBehaviour
{
    public static float DistanceFromTarget = 1000.0f;
    public float ToTarget;

    static bool m_IsCasting = false;

    private void Update()
    {
        if (m_IsCasting == false)
            return;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit))
        {
            ToTarget = hit.distance;
            DistanceFromTarget = hit.distance;
        }
    }

    public static void ActivateCasting()
    {
        m_IsCasting = true;
    }

    public static void StopCasting()
    {
        m_IsCasting = false;
    }
}
