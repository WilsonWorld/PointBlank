/* Created by Wilson World Games, August 2022 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSway : MonoBehaviour
{
    [Header("Sway Settings")]
    [SerializeField] private float m_Smoothing;
    [SerializeField] private float m_SwayMultiplier;

    private void Update()
    {
        Vector2 mouseInput = GetMouseInput();
        Quaternion rotation = CalculateRotation(mouseInput.x, mouseInput.y);
        transform.localRotation = Quaternion.Slerp(transform.localRotation, rotation, m_Smoothing * Time.deltaTime);
    }

    private Vector2 GetMouseInput()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * m_SwayMultiplier;
        float mouseY = Input.GetAxisRaw("Mouse Y") * m_SwayMultiplier;
        return new Vector2(mouseX, mouseY);
    }

    private Quaternion CalculateRotation(float X, float Y)
    {
        Quaternion rotationX = Quaternion.AngleAxis(-Y, Vector3.right);
        Quaternion rotationY = Quaternion.AngleAxis(X, Vector3.up);
        Quaternion targetRotation = rotationX * rotationY;
        return targetRotation;
    }

}
