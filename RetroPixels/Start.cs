using UnityEngine;
using ColossalFramework;
using ICities;

namespace RetroPixels
{
    public class Mod : IUserMod
    {
        public string Name
        {
            get { return "8 Bit Skies"; }
        }

        public string Description
        {
            get { return "Someone please make a driving mod"; }
        }
    }

    public class ModLoad : LoadingExtensionBase
    {
        public override void OnLevelLoaded(LoadMode mode)
        {
            if (mode != LoadMode.NewGame && mode != LoadMode.LoadGame)
                return;
            else
            {
                var cameraController = GameObject.FindObjectOfType<CameraController>().gameObject;
                RetroPixel pix = cameraController.AddComponent<RetroPixel>();
                cameraController.AddComponent<RPEffectController>();
            }
        }
    }
}
