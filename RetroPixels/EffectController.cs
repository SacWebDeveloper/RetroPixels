using System;
using System.Collections.Generic;
using UnityEngine;
using ICities;
using ColossalFramework;

namespace RetroPixels
{
    public class RPEffectController : MonoBehaviour
    {
        public bool showSettingsPanel = false;
        private Rect windowRect = new Rect(64, 64, 200, 200); //was 64,250,803,466
        RetroPixel pix;

        float rounderV = 256;
        float rounderH = 256;
        float rounderN = 4;

        float vert = 256;
        float hor = 256;
        float num = 4;

        void Start()
        {
            pix = GetComponent<RetroPixel>();
        }
        void OnGUI()
        {
            if (showSettingsPanel)
            {
                windowRect = GUI.Window(391436, windowRect, SettingsPanel, "RetroPixels");
            }
        }

        void SettingsPanel(int wnd)
        {
            pix.enabled = GUILayout.Toggle(pix.enabled, "State");
            GUILayout.Space(5);
            GUILayout.Label("Sorry for Decimal Points");
            GUILayout.Space(5);
            GUILayout.Label("Horizontal: " + rounderH.ToString());
            hor = GUILayout.HorizontalSlider(hor, 0, 3840);
            rounderH = Mathf.Round(hor * 100f) / 100f;
            pix.horizontalResolution = Mathf.RoundToInt(rounderH);

            GUILayout.Label("Vertical: " + rounderV.ToString());
            vert = GUILayout.HorizontalSlider(vert, 0, 2160);
            rounderV = Mathf.Round(vert * 100f) / 100f;
            pix.verticalResolution = Mathf.RoundToInt(rounderV);

            GUILayout.Label("Colors: " + rounderN.ToString());
            num = GUILayout.HorizontalSlider(num, 0, 8);
            rounderN = Mathf.Round(num * 100f) / 100f;
            pix.numColors = Mathf.RoundToInt(rounderN);
            GUILayout.Space(10f);

        } 

        void Update()
        {
            if (Input.GetKeyUp(KeyCode.Minus))
                showSettingsPanel = !showSettingsPanel;
        }
    }
}
