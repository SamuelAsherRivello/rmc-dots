using RMC.DOTS.SystemGroups;
using RMC.DOTS.Systems.Player;
using RMC.DOTS.Systems.Scoring;
using Unity.Burst;
using Unity.Entities;
using UnityEngine;

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
            state.RequireForUpdate<GoalWasReachedSystemAuthoring.GoalWasReachedSystemIsEnabledTag>();
            state.RequireForUpdate<ScoringComponent>();
        }
        
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            foreach (var (playerTag, goalWasReached) in 
                     SystemAPI.Query<PlayerTag, GoalWasReachedTag>())
            {
                var scoringComponent = SystemAPI.GetSingleton<ScoringComponent>();
                scoringComponent.ScoreComponent01.ScoreCurrent += 1;
                SystemAPI.SetSingleton(scoringComponent);
            }
        }
    }
}