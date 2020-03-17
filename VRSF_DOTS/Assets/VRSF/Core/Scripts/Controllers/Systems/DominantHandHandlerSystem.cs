﻿using Unity.Entities;
using VRSF.Core.Inputs;
using VRSF.Core.SetupVR;

namespace VRSF.Core.Controllers
{
    /// <summary>
    /// Handle the switch of model between the two hands of the user when using Oculus Go or GearVR
    /// </summary>
    public class DominantHandHandlerSystem : ComponentSystem
    {
        protected override void OnCreate()
        {
            base.OnCreate();
            OnSetupVRReady.Listeners += Setup;
            this.Enabled = false;
        }

        protected override void OnUpdate() { }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            OnSetupVRReady.Listeners -= Setup;

            if (ChangeDominantHandEvent.IsCallbackRegistered(ChangeDominantHand))
                ChangeDominantHandEvent.Listeners -= ChangeDominantHand;
        }

        private void ChangeDominantHand(ChangeDominantHandEvent info)
        {
            if (VRSF_Components.DeviceLoaded != EDevice.GEAR_VR && VRSF_Components.DeviceLoaded != EDevice.OCULUS_GO)
            {
                UnityEngine.Debug.LogError("<b>[VRSF] :</b> This feature is only available for when you use the GearVR or Oculus Go.");
                return;
            }

            Entities.ForEach((ref GoAndGearVRInputCapture singleController) =>
            {
                singleController.IsUserRightHanded = info.NewDominantHand == EHand.RIGHT;
                DisableUnusedHand(singleController.IsUserRightHanded);
            });
        }

        private void Setup(OnSetupVRReady _)
        {
            if (VRSF_Components.DeviceLoaded != EDevice.GEAR_VR && VRSF_Components.DeviceLoaded != EDevice.OCULUS_GO)
                return;

            ChangeDominantHandEvent.Listeners += ChangeDominantHand;

            Entities.ForEach((ref GoAndGearVRInputCapture singleController) =>
            {
                DisableUnusedHand(singleController.IsUserRightHanded);
            });
        }

        private void DisableUnusedHand(bool isUserRightHanded)
        {
            VRSF_Components.LeftController.SetActive(!isUserRightHanded);
            VRSF_Components.RightController.SetActive(isUserRightHanded);
        }
    }
}