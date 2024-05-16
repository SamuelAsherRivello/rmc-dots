using RMC.DOTS.Systems.GameState;
using RMC.DOTS.Systems.Scoring;
using Unity.Burst;
using Unity.Entities;
using Unity.Physics.Systems;
using UnityEngine;

namespace RMC.DOTS.Samples.Pong2D.Pong2D_Version02_DOTS
{
    [BurstCompile]
    [UpdateInGroup(typeof(PhysicsSystemGroup))]
    [UpdateAfter(typeof(PhysicsSimulationGroup))]
    [UpdateBefore(typeof(ExportPhysicsWorld))]
    [RequireMatchingQueriesForUpdate]
    public partial struct ProjectileHasHitGoalScoreSystem : ISystem
    {
        // This query is for all the pickup entities that have been picked up this frame
        private EntityQuery _pickupQuery;
        
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<ProjectileHasHitGoalSystemAuthoring.ProjectileHasHitGoalSystemIsEnabled>();
            state.RequireForUpdate<EndSimulationEntityCommandBufferSystem.Singleton>();
            state.RequireForUpdate<ScoringComponent>();
            state.RequireForUpdate<ProjectileTag>();
            state.RequireForUpdate<ProjectileHasHitGoalComponent>();
            _pickupQuery = SystemAPI.QueryBuilder().WithAll<ProjectileHasHitGoalComponent, ProjectileTag>().Build();
            state.RequireForUpdate(_pickupQuery);
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var ecb = SystemAPI.
                GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>().
                CreateCommandBuffer(state.WorldUnmanaged);

            ScoringComponent scoringComponent = default(ScoringComponent);
            var wasScoreChanged = false;
            
            foreach (var (projectileTag, projectileHasScoredTag, entity) in SystemAPI
                         .Query<ProjectileTag, ProjectileHasHitGoalComponent>().WithEntityAccess())
            {
                
                scoringComponent = SystemAPI.GetSingleton<ScoringComponent>();
                Debug.LogWarning("scoringComponent was: " + scoringComponent.ScoreComponent01.ScoreCurrent);
                
                if (projectileHasScoredTag.PlayerType == PlayerType.Human)
                {
                    scoringComponent.ScoreComponent01.ScoreCurrent += 1;
                    wasScoreChanged = true;
                }
                else 
                {
                    scoringComponent.ScoreComponent02.ScoreCurrent += 1;
                    wasScoreChanged = true;
                }

                ecb.RemoveComponent<ProjectileHasHitGoalComponent>(entity);
            }

            if (wasScoreChanged)
            {
                SystemAPI.SetSingleton(scoringComponent);
            }
       
            Debug.LogWarning("scoringComponent is: " + scoringComponent.ScoreComponent01.ScoreCurrent);
        }
    }
}