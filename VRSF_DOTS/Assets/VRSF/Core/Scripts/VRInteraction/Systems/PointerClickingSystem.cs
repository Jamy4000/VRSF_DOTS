﻿using Unity.Entities;
using VRSF.Core.Events;
using VRSF.Core.Controllers;
using VRSF.Core.Inputs;
using VRSF.Core.Raycast;
using VRSF.Core.SetupVR;

namespace VRSF.Core.VRInteractions
{
    /// <summary>
    /// Handle the Click event in VR. Basically link the Raycast system and the Input System.
    /// CANNOT BE JOBIFIED as we need to send transform info in the ObjectWasClickedEvent
    /// </summary>
    public class PointerClickingSystem : ComponentSystem
    {
        #region ComponentSystem_Methods
        protected override void OnCreate()
        {
            OnSetupVRReady.Listeners += Setup;
            base.OnCreate();
        }

        protected override void OnUpdate()
        {
            Entities.ForEach((ref PointerClick pointerClick, ref StartClickingEventComp startClickingEvent, ref TriggerInputCapture triggerInputCapture, ref BaseInputCapture baseInput, ref VRRaycastOutputs raycastOutputs) =>
            {
                if (pointerClick.CanClick)
                {
                    switch (triggerInputCapture.Hand)
                    {
                        case EHand.LEFT:
                            CheckHit(raycastOutputs.RaycastHitVar, out InteractionVariableContainer.IsClickingSomethingLeft, ERayOrigin.LEFT_HAND);
                            break;
                        case EHand.RIGHT:
                            CheckHit(raycastOutputs.RaycastHitVar, out InteractionVariableContainer.IsClickingSomethingRight, ERayOrigin.RIGHT_HAND);
                            break;
                    }
                }
            });
        }

        protected override void OnDestroy()
        {
            OnSetupVRReady.Listeners -= Setup;
            base.OnDestroy();
        }
        #endregion


        #region PRIVATE_METHODS
        private void CheckHit(RaycastHitVariable hitVar, out bool hasClickSomething, ERayOrigin origin)
        {
            //If nothing is hit, we set the hasClickSomething value to false
            if (hitVar.IsNull)
            {
                hasClickSomething = false;
            }
            else
            {
                hasClickSomething = true;
                new ObjectWasClickedEvent(origin, hitVar.Value.collider.transform);
            }
        }

        private void Setup(OnSetupVRReady info)
        {
            this.Enabled = GetEntityQuery(typeof(PointerClick)).CalculateLength() > 0;
        }
        #endregion PRIVATE_METHODS
    }
}