using RMC.DOTS.Demos.Input;
using RMC.DOTS.SystemGroups;
using RMC.DOTS.Systems.Player;
using Unity.Burst;
using Unity.Entities;
using UnityEngine;

namespace RMC.DOTS.Demos.PhysicsTrigger
{
    [UpdateInGroup(typeof(PauseableSystemGroup))]
    [RequireMatchingQueriesForUpdate]
    public partial struct PlayerPhysicsTriggerSystem : ISystem
    {
        // This query is for all the pickup entities that have been picked up this frame
        private EntityQuery _pickupQuery;

        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<PlayerPhysicsTriggerSystemAuthoring.PlayerPhysicsTriggerSystemIsEnabledTag>();
            _pickupQuery = SystemAPI.QueryBuilder().WithAll<PlayerTag, PlayerWasTriggeredTag>().Build();
        }

        //No burst sice I'm using an implicit toString in my Debug.Log
        public void OnUpdate(ref SystemState state)
        {
            // Get the number of entities we picked up this frame.
            var pickupsThisFrame = _pickupQuery.CalculateEntityCount();
            if(pickupsThisFrame <= 0) return;
            Debug.Log("The Player Hit The Goal #" + pickupsThisFrame + " Times this frame");
        }
    }
}