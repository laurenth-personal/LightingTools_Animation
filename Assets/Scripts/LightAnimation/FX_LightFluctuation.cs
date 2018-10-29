using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.HDPipeline;

/// <summary>
/// Controls the light fluctuations of the plasma core.
/// </summary>

public class FX_LightFluctuation : MonoBehaviour
{

    private Light m_light;
    private HDAdditionalLightData m_lightData;
    public Vector2 minMaxIntensity = new Vector2(100f, 500f);
    public Vector2 minMaxTemperature = new Vector2(2000f, 4000f);
    public Vector2 minMaxFrequency = new Vector2(0.1f, 0.7f);


    private float m_originalIntensity;
    private float m_originalTemperature;
    void Start()
    {
        m_light = GetComponent<Light>();
        m_lightData = GetComponent<HDAdditionalLightData>();

        m_originalIntensity = m_lightData.intensity;
        m_originalTemperature = m_light.colorTemperature;

        StartCoroutine(RandomFluctuations());
    }

    IEnumerator RandomFluctuations()
    {
        while (true)
        {
            float timer = 0.0f;

            // Grab random values within the pre-defined range
            float randomInensity = Random.Range(minMaxIntensity.x, minMaxIntensity.y);
            float randomTemperature = Random.Range(minMaxTemperature.x, minMaxTemperature.y);
            float randomFrequency = Random.Range(minMaxFrequency.x, minMaxFrequency.y);

            // Cache light settings
            float curLightIntensity = m_lightData.intensity;
            float curLightTemperature = m_light.colorTemperature;

            while (timer < randomFrequency)
            {
                // Progress time
                timer += Time.deltaTime;
                float progress = timer / randomFrequency;

                // Set new values
                m_lightData.intensity = Mathf.Lerp(curLightIntensity, randomInensity, progress);
                m_light.colorTemperature = Mathf.Lerp(curLightTemperature, randomTemperature, progress);
                yield return null;
            }

            yield return null;
        }
    }


    public void LightSurge(float intensity, float duration, float temperature = 4000f)
    {
        StopAllCoroutines();
        StartCoroutine(Surge(intensity, duration, temperature));
    }

    IEnumerator Surge(float intensity, float duration, float temperature)
    {
        float timer = 0.0f;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            float progress = timer / duration;

            // Set new values
            m_lightData.intensity = Mathf.Lerp(intensity, m_originalIntensity, progress);
            m_light.colorTemperature = Mathf.Lerp(temperature, m_originalTemperature, progress);
            yield return null;
        }

        StartCoroutine(RandomFluctuations());
    }

    public void StopFluctuaction()
    {
        m_lightData.intensity = 0f;
        StopAllCoroutines();
    }

}
