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
    public partial struct ProjectileHasScoredScoreSystem : ISystem
    {
        // This query is for all the pickup entities that have been picked up this frame
        private EntityQuery _pickupQuery;
        
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<EndSimulationEntityCommandBufferSystem.Singleton>();
            state.RequireForUpdate<ScoringComponent>();
            state.RequireForUpdate<ProjectileTag>();
            state.RequireForUpdate<ProjectileHasScoredComponent>();
            _pickupQuery = SystemAPI.QueryBuilder().WithAll<ProjectileHasScoredComponent, ProjectileTag>().Build();
            state.RequireForUpdate(_pickupQuery);
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var ecb = SystemAPI.
                GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>().
                CreateCommandBuffer(state.WorldUnmanaged);
            
            var scoringComponent = SystemAPI.GetSingleton<ScoringComponent>();
            
            foreach (var (projectileTag, projectileHasScoredTag, entity) in SystemAPI
                         .Query<ProjectileTag, ProjectileHasScoredComponent>().WithEntityAccess())
            {
                if (projectileHasScoredTag.PlayerType == PlayerType.Human)
                {
                    scoringComponent.ScoreComponent01.ScoreCurrent += 1;
                }
                else 
                {
                    scoringComponent.ScoreComponent02.ScoreCurrent += 1;
                }

                ecb.RemoveComponent<ProjectileHasScoredComponent>(entity);
            }
    
            SystemAPI.SetSingleton(scoringComponent);

        }
    }
}