using UnityEngine;
using System.Collections;

namespace AlpacaSound
{
	[ExecuteInEditMode]
	[RequireComponent (typeof(Camera))]
	[AddComponentMenu("Image Effects/Custom/Retro Pixel")]
	public class RetroPixel : MonoBehaviour
	{
		public static readonly int MAX_NUM_COLORS = 16;

		public int horizontalResolution = 160;
		public int verticalResolution = 200;

        public Texture2D image;

		public int numColors = MAX_NUM_COLORS;
		int oldNumColors = 0;

		public Color color0 = Color.black;
		public Color color1 = Color.white;
		public Color color2 = new Color32(255, 75, 75, 255);
		public Color color3 = new Color32(255, 186, 19, 255);
		public Color color4 = new Color32(234, 233, 0, 255);
		public Color color5 = new Color32(132, 207, 69, 255);
		public Color color6 = new Color32(0, 165, 202, 255);
		public Color color7 = new Color32(192, 106, 194, 255);
		public Color color8 = Color.white;
		public Color color9 = Color.white;
		public Color color10 = Color.white;
		public Color color11 = Color.white;
		public Color color12 = Color.white;
		public Color color13 = Color.white;
		public Color color14 = Color.white;
		public Color color15 = Color.white;

		Shader newShader;

		Material m_material;
		Material material
		{
			get
			{
				if (m_material == null)
				{
					string shaderName = "AlpacaSound/RetroPixel16";
					Shader shader = Shader.Find(shaderName);
                    newShader = shader;
					
                    m_material = new Material (newShader);
					m_material.hideFlags = HideFlags.DontSave;
				}
				return m_material;
			} 
		}

		void Start ()
		{
			if (!SystemInfo.supportsImageEffects)
			{
				enabled = false;
				return;
			}
		}
		
		public void OnRenderImage (RenderTexture src, RenderTexture dest)
		{
			horizontalResolution = Mathf.Clamp(horizontalResolution, 1, 2048);
			verticalResolution = Mathf.Clamp(verticalResolution, 1, 2048);
			numColors = Mathf.Clamp(numColors, 2, 16);

			if (material)
			{
				if (oldNumColors != numColors)
				{
                    material.shader = newShader;
				}
				
				material.SetColor ("_Color0", color0);
				material.SetColor ("_Color1", color1);
				if (numColors > 2) material.SetColor ("_Color2", color2);
				if (numColors > 3) material.SetColor ("_Color3", color3);
				if (numColors > 4) material.SetColor ("_Color4", color4);
				if (numColors > 5) material.SetColor ("_Color5", color5);
				if (numColors > 6) material.SetColor ("_Color6", color6);
				if (numColors > 7) material.SetColor ("_Color7", color7);
				if (numColors > 8) material.SetColor("_Color8", color8);
				if (numColors > 9) material.SetColor("_Color9", color9);
				if (numColors > 10) material.SetColor("_Color10", color10);
				if (numColors > 11) material.SetColor("_Color11", color11);
				if (numColors > 12) material.SetColor("_Color12", color12);
				if (numColors > 13) material.SetColor("_Color13", color13);
				if (numColors > 14) material.SetColor("_Color14", color14);
				if (numColors > 15) material.SetColor("_Color15", color15);
				
				RenderTexture scaled = RenderTexture.GetTemporary (horizontalResolution, verticalResolution);
				scaled.filterMode = FilterMode.Point;
				Graphics.Blit (src, scaled, material);
				Graphics.Blit (scaled, dest);
				RenderTexture.ReleaseTemporary (scaled);
				
			}
			else
			{
				Graphics.Blit (src, dest);
			}
		}

		void OnDisable ()
		{
			if (m_material)
			{
				Material.DestroyImmediate (m_material);
			}
		}
	}
}



