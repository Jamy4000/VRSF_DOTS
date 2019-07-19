﻿using Unity.Entities;
using UnityEngine;

namespace VRSF.Core.Raycast
{
    /// <summary>
    /// Only use to setup the Entity you need for Raycasting in VR. This component is then destroy after being converted.
    /// </summary>
    [RequiresEntityConversion]
    public class VRRaycastAuthoring : MonoBehaviour, IConvertGameObjectToEntity
    {
        [Header("The Raycast Origin for this script")]
        public ERayOrigin RayOrigin = ERayOrigin.NONE;
        
        [Header("Maximum distance of the Raycast")]
        public float MaxRaycastDistance = 200.0f;

        [Header("Layer(s) to exclude from the Raycast System.")]
        public LayerMask ExcludedLayer = new LayerMask();

        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            // We add the VRRaycastParameters as a struct to the newly created entity
            dstManager.AddComponentData(entity, new VRRaycastParameters
            {
                MaxRaycastDistance = MaxRaycastDistance,
                ExcludedLayer = ExcludedLayer
            });

            dstManager.AddComponentData(entity, new VRRaycastOrigin
            {
                RayOrigin = RayOrigin
            });

            dstManager.AddComponentData(entity, new VRRaycastOutputs
            {
                RaycastHitVar = new RaycastHitVariable(),
                RayVar = new Ray()
            });

            // We destroy this component as we don't need it anymore
            Destroy(this);
        }
    }
}