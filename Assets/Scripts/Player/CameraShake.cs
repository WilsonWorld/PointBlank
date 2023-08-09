/* Created by Wilson World Games, August 2022 */
/* The Camera Shake class is a custom shake effect that can be applied to the camera. Intended to simulate impacts to the player. */

using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [Header("Shake Settings")]
    public Transform CameraTransform;
    public float ShakeDuration;
    public float ShakeAmount;

    Vector3 m_OriginalPosition;
    float m_OriginalDuration;
    bool m_ShakeCamera = false;

    void Awake()
    {
        if (CameraTransform == null)
            CameraTransform = GetComponent<Transform>();
    }

    // When set to shake, the camera will move around within a sphere around the camera's position until the duration has ended.
    void Update()
    {
        if (m_ShakeCamera == false)
            return;

        if (ShakeDuration > 0) {
            CameraTransform.localPosition = Vector3.Lerp(CameraTransform.localPosition, m_OriginalPosition + Random.insideUnitSphere * ShakeAmount, 3 * Time.deltaTime);
            ShakeDuration -= Time.deltaTime;
        }
        else {
            ShakeDuration = m_OriginalDuration;
            CameraTransform.localPosition = m_OriginalPosition;
            m_ShakeCamera = false;
        }
    }

    // Store the original camera position and shake duration before starting the shake FX
    void InitShake()
    {
        m_OriginalPosition = CameraTransform.localPosition;
        m_OriginalDuration = ShakeDuration;
    }

    // Set the camera to shake
    public void ShakeCamera()
    {
        InitShake();
        m_ShakeCamera = true;
    }
}
