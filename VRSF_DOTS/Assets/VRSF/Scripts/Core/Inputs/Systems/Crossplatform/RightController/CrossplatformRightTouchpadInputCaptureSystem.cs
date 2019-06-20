﻿using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;
using VRSF.Core.Controllers;
using VRSF.Core.SetupVR;

namespace VRSF.Core.Inputs
{
    /// <summary>
    /// System common for the VR Headsets, capture the touchpad inputs for the right controllers
    /// </summary>
    public class CrossplatformRightTouchpadInputCaptureSystem : JobComponentSystem
    {
        protected override void OnCreateManager()
        {
            OnSetupVRReady.Listeners += CheckDevice;
            base.OnCreateManager();
        }

        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            var touchpadJob = new TouchpadInputCapture()
            {
                ThumbPosition = new float2(Input.GetAxis("HorizontalRight"), Input.GetAxis("VerticalRight")),
                RightThumbClickDown = Input.GetButtonDown("RightThumbClick"),
                RightThumbClickUp = Input.GetButtonUp("RightThumbClick"),
                RightThumbTouchDown = Input.GetButtonDown("RightThumbTouch"),
                RightThumbTouchUp = Input.GetButtonUp("RightThumbTouch")
            };

            return touchpadJob.Schedule(this, inputDeps);
        }

        protected override void OnDestroyManager()
        {
            OnSetupVRReady.Listeners -= CheckDevice;
            base.OnDestroyManager();
        }

        struct TouchpadInputCapture : IJobForEach<CrossplatformInputCapture>
        {
            public float2 ThumbPosition;

            public bool RightThumbClickDown;
            public bool RightThumbClickUp;

            public bool RightThumbTouchDown;
            public bool RightThumbTouchUp;

            public void Execute(ref CrossplatformInputCapture c0)
            {
                RightInputsParameters.ThumbPosition = ThumbPosition;

                // Check Click Events
                if (RightThumbClickDown)
                {
                    RightInputsParameters.TouchpadClick = true;
                    new ButtonClickEvent(EHand.RIGHT, EControllersButton.TOUCHPAD);
                }
                else if (RightThumbClickUp)
                {
                    RightInputsParameters.TouchpadClick = false;
                    new ButtonUnclickEvent(EHand.RIGHT, EControllersButton.TOUCHPAD);
                }
                // Check Touch Events if user is not clicking
                else if (!RightInputsParameters.TouchpadClick && RightThumbTouchDown)
                {
                    RightInputsParameters.TouchpadTouch = true;
                    new ButtonTouchEvent(EHand.RIGHT, EControllersButton.TOUCHPAD);
                }
                else if (RightThumbTouchUp)
                {
                    RightInputsParameters.TouchpadTouch = false;
                    new ButtonUntouchEvent(EHand.RIGHT, EControllersButton.TOUCHPAD);
                }
            }
        }

        #region PRIVATE_METHODS
        private void CheckDevice(OnSetupVRReady info)
        {
            this.Enabled = VRSF_Components.DeviceLoaded != EDevice.SIMULATOR;
        }
        #endregion PRIVATE_METHODS
    }
}