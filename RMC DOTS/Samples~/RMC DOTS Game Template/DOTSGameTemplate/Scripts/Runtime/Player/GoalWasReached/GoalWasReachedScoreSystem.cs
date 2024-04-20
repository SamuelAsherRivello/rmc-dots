using RMC.DOTS.SystemGroups;
using RMC.DOTS.Systems.Scoring;
using Unity.Burst;
using Unity.Entities;

namespace RMC.DOTS.Samples.Templates.DOTSGameTemplate
{
    [UpdateInGroup(typeof(PauseableSystemGroup))]
    [RequireMatchingQueriesForUpdate]
    public partial struct GoalWasReachedScoreSystem : ISystem
    {
        // This query is for all the pickup entities that have been picked up this frame
        private EntityQuery _pickupQuery;

        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<ScoringComponent>();
            _pickupQuery = SystemAPI.QueryBuilder().WithAll<PlayerTag,GoalWasReachedTag>().Build();
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