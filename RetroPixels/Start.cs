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
        private EBS_RetroPixel ebsRP;
        private EBS_OldSchoolPix ebsOSP;

        public override void OnLevelLoaded(LoadMode mode)
        {
            if (mode != LoadMode.NewGame && mode != LoadMode.LoadGame)
                return;
            else
            {
                var cameraController = GameObject.FindObjectOfType<CameraController>().gameObject;
                ebsRP = cameraController.AddComponent<EBS_RetroPixel>();
                ebsRP.enabled = false;
                ebsOSP = cameraController.AddComponent<EBS_OldSchoolPix>();
                ebsOSP.enabled = false;
                toggler = cameraController.AddComponent<RPEffectController>();
                toggler.enabled = true;
            }
        }
        public override void OnLevelUnloading()
        {
            GameObject.Destroy(toggler);
            GameObject.Destroy(ebsRP);
            GameObject.Destroy(ebsOSP);
        }
        public override void OnReleased()
        {
            GameObject.Destroy(toggler);
            GameObject.Destroy(ebsRP);
            GameObject.Destroy(ebsOSP);
        }
    }
}
