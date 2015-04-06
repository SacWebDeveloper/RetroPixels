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
        private RPEffectController toggler;
        private RetroPixel pix;

        public override void OnLevelLoaded(LoadMode mode)
        {
            if (mode != LoadMode.NewGame && mode != LoadMode.LoadGame)
                return;
            else
            {
                var cameraController = GameObject.FindObjectOfType<CameraController>().gameObject;
                pix = cameraController.AddComponent<RetroPixel>();
                pix.enabled = false;
                toggler = cameraController.AddComponent<RPEffectController>();
            }
        }
        public override void OnLevelUnloading()
        {
            GameObject.Destroy(toggler);
            GameObject.Destroy(pix);
        }
        public override void OnReleased()
        {
            GameObject.Destroy(toggler);
            GameObject.Destroy(pix);
        }
    }
}
