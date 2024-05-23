using RMC.DOTS.Systems.DestroyEntity;
using RMC.DOTS.Systems.PhysicsTrigger;
using Unity.Burst;
using Unity.Entities;
using Unity.Physics.Systems;
using UnityEngine;

//TODO: FixPhysics
namespace RMC.DOTS.Samples.Pong2D.Pong2D_Version02_DOTS
{
    [BurstCompile]
    [UpdateInGroup(typeof(PhysicsSystemGroup))]
    [UpdateAfter(typeof(PhysicsSimulationGroup))]
    [UpdateBefore(typeof(ExportPhysicsWorld))]
    [RequireMatchingQueriesForUpdate]
    public partial struct ProjectileHasHitGoalSystem : ISystem
    {
        private ComponentLookup<GoalComponent> _goalComponentLookup;
        
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<ProjectileHasHitGoalSystemAuthoring.ProjectileHasHitGoalSystemIsEnabled>();
            state.RequireForUpdate<EndInitializationEntityCommandBufferSystem.Singleton>();
            _goalComponentLookup = state.GetComponentLookup<GoalComponent>();
        }

        
        //NEW SYNTAX
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var ecb = SystemAPI.
                GetSingleton<BeginPresentationEntityCommandBufferSystem.Singleton>().
                CreateCommandBuffer(state.WorldUnmanaged);
            
            foreach (var (statefulTriggerEventBuffers, entity) in 
                     SystemAPI.Query<DynamicBuffer<StatefulTriggerEvent>>().
                         WithEntityAccess())
            {
                
                for (int bufferIndex = 0; bufferIndex < statefulTriggerEventBuffers.Length; bufferIndex++)
                {
                    var collisionEvent = statefulTriggerEventBuffers[bufferIndex];
                    if (collisionEvent.State == StatefulEventState.Enter)
                    {
                        //DO SOMETHING
                        break;
                    }
                }
            }
        }
        
        
        // [BurstCompile]
        // public void OnUpdate(ref SystemState state)
        // {
        //     var ecb = SystemAPI.
        //         GetSingleton<EndInitializationEntityCommandBufferSystem.Singleton>().
        //         CreateCommandBuffer(state.WorldUnmanaged);
        //
        //     _goalComponentLookup.Update(ref state);
        //
        //     // The frame AFTER PhysicsTriggerOutputComponent
        //     // Cleanup collision locally
        //     foreach (var (projectileTag, projectileHasScoredTag, entity) in SystemAPI.Query<ProjectileTag, ProjectileHasHitGoalComponent>().WithEntityAccess())
        //     {
        //         //Debug.Log($"GamePickup ({entity.Index}) Set To REMOVE on TimeFrameCount: {Time.frameCount}");
        //         ecb.RemoveComponent<ProjectileHasHitGoalComponent>(entity);
        //         
        //         // DESTROY
        //         ecb.AddComponent<DestroyEntityComponent>(entity);
        //     }
        //     
        //     // The frame DURING PhysicsTriggerOutputComponent
        //     // Process collision locally
        //     foreach (var (projectileTag, physicsTriggerOutputTag, entity) in SystemAPI.Query<ProjectileTag, PhysicsTriggerOutputComponent>().WithEntityAccess())
        //     {
        //         if (physicsTriggerOutputTag.PhysicsTriggerType == PhysicsTriggerType.Enter &&
        //             physicsTriggerOutputTag.TimeFrameCountForLastCollision <= Time.frameCount - PhysicsTriggerOutputComponent.FramesToWait)
        //         {
        //            // Debug.Log($"GamePickup ({entity.Index}) Set To ADD on TimeFrameCount: {Time.frameCount}");
        //             var goalComponent = _goalComponentLookup.GetRefRO(physicsTriggerOutputTag.TheOtherEntity);
        //             ecb.AddComponent<ProjectileHasHitGoalComponent>(entity, 
        //                     new ProjectileHasHitGoalComponent { PlayerType = goalComponent.ValueRO.PlayerType});
        //         }
        //     }
        // }
    }
}