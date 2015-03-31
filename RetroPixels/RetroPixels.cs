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

        public int numColors = MAX_NUM_COLORS;

        public string shader2;
        public string shader3;
        public string shader4;
        public string shader5;
        public string shader6;
        public string shader7;
        public string shader8;


        public Color color0 = Color.black;
        public Color color1 = Color.white;
        public Color color2 = new Color32(255, 75, 75, 255);
        public Color color3 = new Color32(255, 186, 19, 255);
        public Color color4 = new Color32(234, 233, 0, 255);
        public Color color5 = new Color32(132, 207, 69, 255);
        public Color color6 = new Color32(0, 165, 202, 255);
        public Color color7 = new Color32(192, 106, 194, 255);

        Shader[] shaders = new Shader[MAX_NUM_COLORS];
        string[] shaderStrings = new string[MAX_NUM_COLORS];
        public Material theMaterial;

        void Start()
        {
            if (!SystemInfo.supportsImageEffects)
            {
                enabled = false;
                return;
            }
            Material theMaterial = new Material(shaderStrings[numColors - 1]);
            shaderStrings[0] = RetroPixel2.pixel2;
            shaderStrings[1] = RetroPixel2.pixel3;
            shaderStrings[2] = RetroPixel2.pixel4;
            shaderStrings[3] = RetroPixel2.pixel5;
            shaderStrings[4] = RetroPixel2.pixel6;
            shaderStrings[5] = RetroPixel2.pixel7;
            shaderStrings[6] = RetroPixel2.pixel8;
            shaderStrings[7] = RetroPixel2.pixel8;

        }

        public void OnRenderImage(RenderTexture src, RenderTexture dest)
        {
            horizontalResolution = Mathf.Clamp(horizontalResolution, 1, 2048);
            verticalResolution = Mathf.Clamp(verticalResolution, 1, 2048);
            numColors = Mathf.Clamp(numColors, 2, 8);
            Material theMaterial;
            theMaterial = new Material(shaderStrings[numColors - 1]);
            if (theMaterial)
            {
                theMaterial.SetColor("_Color0", color0);
                theMaterial.SetColor("_Color1", color1);
                if (numColors > 2) theMaterial.SetColor("_Color2", color2);
                if (numColors > 3) theMaterial.SetColor("_Color3", color3);
                if (numColors > 4) theMaterial.SetColor("_Color4", color4);
                if (numColors > 5) theMaterial.SetColor("_Color5", color5);
                if (numColors > 6) theMaterial.SetColor("_Color6", color6);
                if (numColors > 7) theMaterial.SetColor("_Color7", color7);

                RenderTexture scaled = RenderTexture.GetTemporary(horizontalResolution, verticalResolution);
                scaled.filterMode = FilterMode.Point;
                Graphics.Blit(src, scaled, theMaterial);
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
    }
}
