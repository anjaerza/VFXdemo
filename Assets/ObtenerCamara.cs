using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ObtenerCamara : MonoBehaviour
{
	[SerializeField] Shader shader;
	[Range (0,0.5f)]
	public float factor;
	private Material materialActual;

	Material material 
	{
		get
		{
			if (materialActual==null)
			{
				Debug.Log("Creando Material");
				materialActual=new Material(shader);
				materialActual.hideFlags = HideFlags.HideAndDontSave;
			}
			return materialActual;
		}
	}
    // Start is called before the first frame update
    void Start()
    {
		if(!SystemInfo.supportsImageEffects)
		{
			enabled=false;
			return;
		}

		if(!shader || !shader.isSupported)
		{
			enabled=false;
		}
			     
    }

	void OnRenderImage(RenderTexture entrada, RenderTexture salida) 
	{
		material.SetFloat("_Factor",factor);
		Graphics.Blit(entrada, salida, material);
	}

    // Update is called once per frame
    void Update() {}

	void onDisable()
	{
		if(materialActual) 
		{
			DestroyImmediate(materialActual);
		}
	}
}
