﻿using Unity.Entities;
using VRSF.Core.Events;
using VRSF.Core.Raycast;

namespace VRSF.Core.Controllers
{
    /// <summary>
    /// System to handle the visibility of the Pointers based on whether it's hitting something
    /// </summary>
    public class PointerStateSystem : ComponentSystem
    {
        struct Filter : IComponentData
        {
            public PointerVisibilityComponents PointerVisibility;
            public ScriptableRaycastComponent ScriptableRaycast;
        }


        #region ComponentSystem_Methods
        protected override void OnStartRunning()
        {
            base.OnStartRunning();
            // Just check if there's at least one entity
            Entities.ForEach((ref Filter e) =>
            {
                ObjectWasHoveredEvent.Listeners += OnSomethingWasHit;
                return;
            });
        }

        protected override void OnUpdate()
        {
            Entities.ForEach((ref Filter e) =>
            {
                if (e.ScriptableRaycast.IsSetup)
                    CheckPointerState(e);
            });
        }

        protected override void OnDestroyManager()
        {
            // Just check if there's at least one entity
            Entities.ForEach((ref Filter e) =>
            {
                ObjectWasHoveredEvent.Listeners -= OnSomethingWasHit;
                return;
            });
            base.OnDestroyManager();
        }
        #endregion ComponentSystem_Methods


        #region PRIVATE_METHODS
        private void OnSomethingWasHit(ObjectWasHoveredEvent info)
        {
            Entities.ForEach((ref Filter e) =>
            {
                if (info.RaycastOrigin == e.ScriptableRaycast.RayOrigin && info.ObjectHovered != null)
                    e.PointerVisibility.PointerState = EPointerState.ON;
            });
        }

        /// <summary>
        /// Set the state of the pointer based on the hits 
        /// </summary>
        private void CheckPointerState(Filter e)
        {
            if (e.ScriptableRaycast.RaycastHitVar.IsNull && e.PointerVisibility.PointerState == EPointerState.ON)
                e.PointerVisibility.PointerState = EPointerState.DISAPPEARING;
        }
        #endregion PRIVATE_METHODS
    }
}