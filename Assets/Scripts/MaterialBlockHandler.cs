using UnityEngine;
using UnityEngine.Experimental.Rendering.HDPipeline;

[ExecuteInEditMode]
public class MaterialBlockHandler : MonoBehaviour
{
    public int materialID = 0; //which material to use? (typically, set to 0)
    [ColorUsage(false,true)]
    public Color color = Color.white;
    public string colorName = "_EmissiveColor";
    [Range(0,1)]
    public float emissiveDimmer = 1.0f;
    public bool driveEmissiveWithLight = true;
    public HDAdditionalLightData additionalLight;
    private Renderer rend;
    private MaterialPropertyBlock mpb;

    private Light drivingLight;
    private Color _oldColor;
    private Color _originalColor;

    private void OnEnable()
    {
        if (rend == null)
            rend = GetComponent<Renderer>();
        _originalColor = rend.sharedMaterial.GetColor(colorName).gamma;
        if (driveEmissiveWithLight && additionalLight != null)
        {
            drivingLight = additionalLight.GetComponent<Light>();
        }
        if (driveEmissiveWithLight && additionalLight == null)
        {
            Debug.LogWarning(gameObject.name + "'s emissive tries to read a light's intensity but the reference is null.");
        }
        _oldColor = _originalColor;
    }

    // Update is called once per frame
    void Update()
    {
        if (additionalLight != null && driveEmissiveWithLight)
        {
            if (!additionalLight.gameObject.activeInHierarchy || !drivingLight.enabled)
            {
                emissiveDimmer = 0.0f;
            }
            else
            {
                emissiveDimmer = additionalLight.lightDimmer;
            }
            if (drivingLight != null)
                color = drivingLight.color;
        }

        //Change to HDR color, intensity is in the color now
        var finalColor = _originalColor * color * emissiveDimmer;

        //update color if the value has changed
        if (_oldColor != finalColor)
        {
            _oldColor = finalColor;
            SetPropertyBlock(finalColor);
        }
    }

    void SetPropertyBlock(Color color)
    {
        //ensure a renderer is available
        if (rend == null)
            rend = GetComponent<Renderer>();

        if (rend != null)
        {
            //create a new material property block
            mpb = new MaterialPropertyBlock();

            //get material property block
            rend.GetPropertyBlock(mpb, materialID);

            mpb.SetColor(colorName, color );

            //set material property block
            rend.SetPropertyBlock(mpb, materialID);
        }

    }
}
