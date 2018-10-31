using UnityEngine;
using UnityEngine.Experimental.Rendering.HDPipeline;

[ExecuteInEditMode]
public class MaterialEmissionDimmer : MonoBehaviour
{
    public int targetMaterialID = 0; //which material to use? (typically, set to 0)
    [ColorUsage(false,true)]
    public Color colorMultiplier = Color.white;
    [Range(0,1)]
    public float emissiveDimmer = 1.0f;
    public HDAdditionalLightData additionalLight;
    public bool inheritLightColor;

	private  string colorName = "_EmissiveColor";
    private Renderer rend;
    private MaterialPropertyBlock mpb;
    private Light drivingLight;
    private Color _cachedColor;
    private Color _originalColor;

    private void OnEnable()
    {
        if (rend == null)
            rend = GetComponent<Renderer>();

        if (additionalLight != null)
        {
            drivingLight = additionalLight.GetComponent<Light>();

            if(inheritLightColor)
            {
                _originalColor = drivingLight.color;
            }
        }

        if(!inheritLightColor)
        {
			_originalColor = rend.sharedMaterial.GetColor(colorName).gamma;
        }

        _cachedColor = _originalColor;
    }

    // Update is called once per frame
    void Update()
    {
        if (additionalLight != null)
        {
            if (!additionalLight.gameObject.activeInHierarchy || !drivingLight.enabled || !additionalLight.enabled)
            {
                emissiveDimmer = 0.0f;
            }
            else
            {
                emissiveDimmer = additionalLight.lightDimmer;
            }
        }

        //Change to HDR color, intensity is in the color now
        var finalColor = _originalColor * colorMultiplier * emissiveDimmer;

        //update color if the value has changed
        if (_cachedColor != finalColor)
        {
            SetPropertyBlock(finalColor);
			_cachedColor = finalColor;
        }
    }

    void SetPropertyBlock(Color color)
    {
        //ensure a renderer is available
        if (rend == null)
            rend = GetComponent<Renderer>();

        if (rend != null)
        {
			mpb = new MaterialPropertyBlock();            
			mpb.SetColor(colorName, color );

            //set material property block
            rend.SetPropertyBlock(mpb, targetMaterialID);
        }
    }

    void RemovePropertyBlock()
    {
        if (rend.HasPropertyBlock())
            rend.SetPropertyBlock(null,targetMaterialID);
    }

	private void OnDisable()
	{
        RemovePropertyBlock();
	}
}
