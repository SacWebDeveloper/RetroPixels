using UnityEngine;
using System.Collections;

namespace RetroPixels
{
    [RequireComponent(typeof(Camera))]
    public class RetroPixel : MonoBehaviour
    {
        public static readonly int MAX_NUM_COLORS = 8;

        public int horizontalResolution = 160;
        public int verticalResolution = 200;
        public int bits = 8;
        public bool retroColors = false;
        public int numColors = MAX_NUM_COLORS;
        public bool useActualColors = false;
        public string shader2;
        public string shader3;
        public string shader4;
        public string shader5;
        public string shader6;
        public string shader7;
        public string shader8;
        public string shader16;

        public enum RPColorMode
        {
            Modern = 0,
            Retro = 1,
            SuperRetro = 2,
        }
        public RPColorMode colorMode = RPColorMode.Modern;
        public Color color0 = Color.black;
        public Color color1;
        public Color color2;
        public Color color3;
        public Color color4;
        public Color color5;
        public Color color6;
        public Color color7;
        public Color color8 = new Color32(125, 75, 0, 255);
        public Color color9 = new Color32(206, 143, 13, 255);
        public Color color10 = new Color32(64, 105, 211, 255);
        public Color color11 = new Color32(240, 219, 44, 255);
        public Color color12 = new Color32(213, 67, 67, 255);
        public Color color13 = new Color32(79, 98, 76, 255);
        public Color color14 = new Color32(34, 34, 34, 255);
        public Color color15 = new Color32(201, 201, 201, 255);


        public Color superRetroColor1 = new Color32(105, 105, 105, 255);
        public Color superRetroColor2 = new Color32(255, 75, 75, 255);
        public Color superRetroColor3 = new Color32(255, 186, 19, 255);
        public Color superRetroColor4 = new Color32(234, 233, 0, 255);
        public Color superRetroColor5 = new Color32(132, 207, 69, 255);
        public Color superRetroColor6 = new Color32(0, 165, 202, 255);
        public Color superRetroColor7 = new Color32(192, 106, 194, 255);


        public Color retroColor1 = new Color32(163, 163, 163, 255);
        public Color retroColor2 = new Color32(125, 35, 35, 255);
        public Color retroColor3 = new Color32(125, 93, 9, 255);
        public Color retroColor4 = new Color32(117, 116, 0, 255);
        public Color retroColor5 = new Color32(61, 103, 40, 255);
        public Color retroColor6 = new Color32(0, 82, 101, 255);
        public Color retroColor7 = new Color32(96, 53, 97, 255);

        public Color modernColor1 = Color.white;
        public Color modernColor2 = new Color32(82, 82, 82, 255);
        public Color modernColor3 = new Color32(57, 90, 104, 255);
        public Color modernColor4 = new Color32(140, 154, 56, 255);
        public Color modernColor5 = new Color32(64, 107, 74, 255);
        public Color modernColor6 = new Color32(177, 61, 61, 255);
        public Color modernColor7 = new Color32(165, 165, 165, 255);


        Shader[] shaders = new Shader[MAX_NUM_COLORS];
        string bit16shad;
        string bit32shad;
        string bit64shad;
        string[] shaderStrings = new string[MAX_NUM_COLORS];
        public Material theMaterial;

        void Start()
        {
            if (!SystemInfo.supportsImageEffects)
            {
                enabled = false;
                return;
            }
            color1 = modernColor1;
            color2 = modernColor2;
            color3 = modernColor3;
            color4 = modernColor4;
            color5 = modernColor5;
            color6 = modernColor6;
            color7 = modernColor7;
            
            Material theMaterial = new Material(shaderStrings[numColors - 1]);
            shaderStrings[0] = RetroPixel2.pixel2;
            shaderStrings[1] = RetroPixel2.pixel3;
            shaderStrings[2] = RetroPixel2.pixel4;
            shaderStrings[3] = RetroPixel2.pixel5;
            shaderStrings[4] = RetroPixel2.pixel6;
            shaderStrings[5] = RetroPixel2.pixel7;
            shaderStrings[6] = RetroPixel2.pixel8;
            shaderStrings[7] = RetroPixel2.pixel8;
            bit16shad = RetroPixel2.pixel16;
            bit32shad = RetroPixel2.pixel32mult;
            bit64shad = RetroPixel2.pixel64;

        }

        public void OnRenderImage(RenderTexture src, RenderTexture dest)
        {
            horizontalResolution = Mathf.Clamp(horizontalResolution, 1, 3840);
            verticalResolution = Mathf.Clamp(verticalResolution, 1, 2160);
            numColors = Mathf.Clamp(numColors, 1, 8);
            Material theMaterial;
            if (bits == 16)
                theMaterial = new Material(bit16shad);
            else if (bits == 32)
                theMaterial = new Material(bit32shad);
            else if (bits == 64)
                theMaterial = new Material(bit64shad);
            else
                theMaterial = new Material(shaderStrings[numColors - 1]);
            if (theMaterial)
            {
				if (bits == 16 || bits == 32 || bits == 64)
				{
					theMaterial.SetColor ("_Color0", color0);
					theMaterial.SetColor ("_Color1", color1);
					theMaterial.SetColor ("_Color2", color2);
					theMaterial.SetColor ("_Color3", color3);
					theMaterial.SetColor ("_Color4", color4);
					theMaterial.SetColor ("_Color5", color5);
					theMaterial.SetColor ("_Color6", color6);
					theMaterial.SetColor ("_Color7", color7);
					theMaterial.SetColor ("_Color8", color8);
					theMaterial.SetColor ("_Color9", color9);
					theMaterial.SetColor ("_Color10", color10);
					theMaterial.SetColor ("_Color11", color11);
					theMaterial.SetColor ("_Color12", color12);
					theMaterial.SetColor ("_Color13", color13);
					theMaterial.SetColor ("_Color14", color14);
					theMaterial.SetColor ("_Color15", color15);

				}
				else
				{
					theMaterial.SetColor ("_Color0", color0);
					theMaterial.SetColor ("_Color1", color1);
					if (numColors > 2) theMaterial.SetColor ("_Color2", color2);
					if (numColors > 3) theMaterial.SetColor ("_Color3", color3);
					if (numColors > 4) theMaterial.SetColor ("_Color4", color4);
					if (numColors > 5) theMaterial.SetColor ("_Color5", color5);
					if (numColors > 6) theMaterial.SetColor ("_Color6", color6);
					if (numColors > 7) theMaterial.SetColor ("_Color7", color7);
				}

                
                RenderTexture scaled = RenderTexture.GetTemporary(horizontalResolution, verticalResolution);
                scaled.filterMode = FilterMode.Point;
                if (!useActualColors)
                    Graphics.Blit(src, scaled, theMaterial);
                else
                    Graphics.Blit(src, scaled);
                Graphics.Blit(scaled, dest);
                RenderTexture.ReleaseTemporary(scaled);
                UnityEngine.Object matTrash = theMaterial;
                UnityEngine.Object shadTrash = theMaterial.shader;
                DestroyImmediate(matTrash);
                DestroyImmediate(shadTrash);

            }
            else
            {
                Graphics.Blit(src, dest);
            }
        }

        void OnDisable()
        {
            if (theMaterial)
            {
                Material.DestroyImmediate(theMaterial);
            }
        }

        public void SetActualColors(bool toggle)
        {
            useActualColors = toggle;
        }

        public void SetBit(int i)
        {
            bits = i;
        }

        
        public void SetModernColors()
        {
            color1 = modernColor1;
            color2 = modernColor2;
            color3 = modernColor3;
            color4 = modernColor4;
            color5 = modernColor5;
            color6 = modernColor6;
            color7 = modernColor7;
        }
        public void SetRetroColors()
        {
            color1 = retroColor1;
            color2 = retroColor2;
            color3 = retroColor3;
            color4 = retroColor4;
            color5 = retroColor5;
            color6 = retroColor6;
            color7 = retroColor7;
        }
        public void SetSuperRetroColors()
        {
            color1 = superRetroColor1;
            color2 = superRetroColor2;
            color3 = superRetroColor3;
            color4 = superRetroColor4;
            color5 = superRetroColor5;
            color6 = superRetroColor6;
            color7 = superRetroColor7;
        }
    }
}
