using RMC.DOTS.SystemGroups;
using RMC.DOTS.Systems.Scoring;
using Unity.Burst;
using Unity.Entities;
using UnityEngine;

namespace RMC.DOTS.Demos.PhysicsTriggerSystem
{
    [UpdateInGroup(typeof(PauseableSystemGroup))]
    [RequireMatchingQueriesForUpdate]
    public partial struct PlayerPhysicsTriggerSystem : ISystem
    {
        // This query is for all the pickup entities that have been picked up this frame
        private EntityQuery _pickupQuery;

        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<ScoringComponent>();
            _pickupQuery = SystemAPI.QueryBuilder().WithAll<PlayerTag, PlayerWasTriggeredTag>().Build();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            // Get the number of entities we picked up this frame.
            var pickupsThisFrame = _pickupQuery.CalculateEntityCount();
            if(pickupsThisFrame <= 0) return;
            
            Debug.Log("Hit");

        }
    }
}