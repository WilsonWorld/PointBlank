/* Created by Wilson World Games, August 2022 */
/* The Blink Light class adds a constant shifting of light intensity to create a blinking effect on a light source. */

using UnityEngine;

public class BlinkLight : MonoBehaviour
{
    [Header("Light Settings")]
    public float IntensityMax = 1.0f;
    public float IntensityMin = 0.0f;
    public float BlinkRate = 1.0f;

    Light m_LightSource;
    bool m_isIncreasing = false;

    
    void Start()
    {
        m_LightSource = GetComponent<Light>();
    }

    void Update()
    {
        if (m_isIncreasing == false)
            DecreaseIntensity();
        else
            IncreaseIntensity();
    }

    // Increase the light intensity at a specified rate until the specified max has been reached.
    void IncreaseIntensity()
    {
        m_LightSource.intensity += (BlinkRate * 0.1f) * Time.deltaTime;

        if (m_LightSource.intensity >= IntensityMax) {
            m_LightSource.intensity = IntensityMax;
            m_isIncreasing = false;
        }
    }

    // Decrease the light intensity at a specified rate until the specified min has been reached.
    void DecreaseIntensity()
    {
        m_LightSource.intensity -= (BlinkRate * 0.1f) * Time.deltaTime;

        if (m_LightSource.intensity <= IntensityMin) {
            m_LightSource.intensity = IntensityMin;
            m_isIncreasing = true;
        }
    }
}
