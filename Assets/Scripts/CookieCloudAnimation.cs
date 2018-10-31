using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Light))]
public class CookieCloudAnimation : MonoBehaviour
{
	private Material m_material;
    private CustomRenderTexture m_customRenderTexture;
    private Light m_Light;

	public int size;
    public Texture2D cloudLayer1;
	public Vector2 speedLayer1;
    public Texture2D cloudLayer2;
    public Vector2 speedLayer2;

	// Start is called before the first frame update
	private void OnEnable()
	{
		m_customRenderTexture = new CustomRenderTexture(size,size);
		var m_shader = Shader.Find("CookieAnimation/TextureScrolling_2layers");
		m_material = new Material(m_shader);
		m_customRenderTexture.material = m_material;
		m_customRenderTexture.updateMode = CustomRenderTextureUpdateMode.Realtime;
		m_customRenderTexture.useMipMap = true;
        m_Light = gameObject.GetComponent<Light>();
        m_Light.cookie = m_customRenderTexture;
        SetShaderProperties();
    }

    public void SetShaderProperties()
    {
		m_material.SetTexture("_Tex",cloudLayer1);
		m_material.SetTexture("_Tex2", cloudLayer2);
		m_material.SetVector("_Speed",new Vector4(speedLayer1.x,speedLayer1.y,speedLayer2.x,speedLayer2.y));      
    }

	private void OnDisable()
	{
        OnDestroy();
	}

	private void OnDestroy()
	{
        m_Light.cookie = null;
        m_customRenderTexture.Release();
        if(Application.isPlaying)
        {
			Destroy(m_customRenderTexture);
			Destroy(m_material);            
        }
        else
        {
            DestroyImmediate(m_customRenderTexture);
            DestroyImmediate(m_material);
        }
	}
}
