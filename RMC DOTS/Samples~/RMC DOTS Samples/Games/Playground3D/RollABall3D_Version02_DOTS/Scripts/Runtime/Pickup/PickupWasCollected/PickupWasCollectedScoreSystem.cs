using RMC.DOTS.Systems.GameState;
using RMC.DOTS.Systems.Scoring;
using Unity.Burst;
using Unity.Entities;
using Unity.Physics.Systems;
using UnityEngine;

namespace RMC.Playground3D.RollABall3D_Version02_DOTS
{
    [UpdateInGroup(typeof(PhysicsSystemGroup))]
    [UpdateAfter(typeof(PhysicsSimulationGroup))]
    [UpdateBefore(typeof(ExportPhysicsWorld))]
    [RequireMatchingQueriesForUpdate]
    public partial struct PickupWasCollectedScoreSystem : ISystem
    {
        // This query is for all the pickup entities that have been picked up this frame
        private EntityQuery _pickupQuery;

        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<ScoringComponent>();
            _pickupQuery = SystemAPI.QueryBuilder().WithAll<PickupWasCollectedTag, PickupTag>().Build();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            // Get the number of entities we picked up this frame.
            var pickupsThisFrame = _pickupQuery.CalculateEntityCount();
            if(pickupsThisFrame <= 0) return;
            
            var pickupCounter = SystemAPI.GetSingleton<ScoringComponent>();
            pickupCounter.ScoreComponent01.ScoreCurrent += pickupsThisFrame;
            SystemAPI.SetSingleton(pickupCounter);

        }
    }
}