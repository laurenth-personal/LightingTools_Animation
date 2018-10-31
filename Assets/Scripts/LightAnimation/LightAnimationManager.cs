using UnityEngine;
using UnityEngine.Experimental.Rendering.HDPipeline;

public class LightAnimationManager : MonoBehaviour
{
    private HDAdditionalLightData additionalLightData;
    private MaterialEmissionDimmer matEmissionDimmer;
    private float _oldValue;

    private LightEventAnimation[] lightAnimations;

    private void OnEnable()
    {
        lightAnimations = GetComponents<LightEventAnimation>();
        if (gameObject.GetComponent<HDAdditionalLightData>())
            additionalLightData = gameObject.GetComponent<HDAdditionalLightData>();
        else if (gameObject.GetComponent<MaterialEmissionDimmer>())
            matEmissionDimmer = gameObject.GetComponent<MaterialEmissionDimmer>();
    }

    void Update()
    {
        if (additionalLightData == null && matEmissionDimmer == null)
            return;
        if (lightAnimations.Length <= 0)
            return;
        var currentValue = 1.0f;
        foreach (var lightAnimator in lightAnimations)
        {
            currentValue *= lightAnimator.getCurrentValue();
        }
        //if( _oldValue != currentValue )
        //{
            if (matEmissionDimmer != null)
                matEmissionDimmer.emissiveDimmer = currentValue;
            else if (additionalLightData != null)
            {
                additionalLightData.lightDimmer = currentValue;
                additionalLightData.volumetricDimmer = currentValue;
            }

        _oldValue = currentValue;
        //}
    }

    public void SetDimmer(float dimmer)
    {
        OnEnable();

        if (matEmissionDimmer != null)
            matEmissionDimmer.emissiveDimmer = dimmer;
        if (additionalLightData != null)
        {
            additionalLightData.lightDimmer = dimmer;
            additionalLightData.volumetricDimmer = dimmer;
        }
    }
}
