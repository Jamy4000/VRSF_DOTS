﻿using Unity.Entities;
using VRDF.Core.Inputs;

namespace VRDF.Core.CBRA
{
    /// <summary>
    /// Handle the Stop Touching events for CBRAs Entities
    /// </summary>
    [UpdateAfter(typeof(TouchingEventsRemover))]
    public class CBRAStopTouchingSystem : ComponentSystem
    {
        private EntityManager _entityManager;

        protected override void OnCreate()
        {
            // Cache the EntityManager in a field, so we don't have to get it every frame
            _entityManager = World.EntityManager;
        }

        protected override void OnUpdate()
        {
            Entities.ForEach((Entity entity, ref StopTouchingEventComp stopTouchingEvent, ref CBRATag cbraEvents) =>
            {
                if (_entityManager.HasComponent(entity, VRInteractions.InputTypeGetter.GetTypeFor(stopTouchingEvent.ButtonInteracting)) && CBRADelegatesHolder.StopTouchingEvents.TryGetValue(entity, out System.Action action))
                    action.Invoke();
            });
        }
    }
}