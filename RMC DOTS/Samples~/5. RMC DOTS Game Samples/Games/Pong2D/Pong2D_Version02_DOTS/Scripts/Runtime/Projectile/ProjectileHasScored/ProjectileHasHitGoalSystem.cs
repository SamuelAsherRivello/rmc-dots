using Unity.Burst;
using Unity.Entities;
using Unity.Physics.PhysicsStateful;
using UnityEngine;

//TODO: FixPhysics
namespace RMC.DOTS.Samples.Pong2D.Pong2D_Version02_DOTS
{
    [BurstCompile]
    [UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
    [UpdateAfter(typeof(StatefulTriggerEventSystem))] 
    [RequireMatchingQueriesForUpdate]
    public partial struct ProjectileHasHitGoalSystem : ISystem
    {
        private ComponentLookup<ProjectileTag> _projectileTagComponentLookup;
        
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<BeginPresentationEntityCommandBufferSystem.Singleton>();
            state.RequireForUpdate<ProjectileHasHitGoalSystemAuthoring.ProjectileHasHitGoalSystemIsEnabled>();
            _projectileTagComponentLookup = state.GetComponentLookup<ProjectileTag>();
        }
        
        //NEW SYNTAX
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            _projectileTagComponentLookup.Update(ref state);
            
            var ecb = SystemAPI.
                GetSingleton<BeginPresentationEntityCommandBufferSystem.Singleton>().
                CreateCommandBuffer(state.WorldUnmanaged);
            
            
            // Goal TRIGGERS with...
            foreach (var (goalComponent, statefulEventBuffers, goalEntity) in 
                     SystemAPI.Query<RefRO<GoalComponent>, DynamicBuffer<StatefulTriggerEvent>>().
                         WithEntityAccess())
            {

                for (int bufferIndex = 0; bufferIndex < statefulEventBuffers.Length; bufferIndex++)
                {
                    var statefulEvent = statefulEventBuffers[bufferIndex];
                    if (statefulEvent.State == StatefulEventState.Enter)
                    {
                        // Goal TRIGGERS with...
                        var otherEntity = statefulEvent.GetOtherEntity(goalEntity);
                        if (_projectileTagComponentLookup.HasComponent(otherEntity))
                        {
                            ecb.AddComponent<ProjectileHasHitGoalComponent>(otherEntity, 
                                new ProjectileHasHitGoalComponent { PlayerType = goalComponent.ValueRO.PlayerType});
                            break;
                        }
                 
                    }
                }
            }
        }
    }
}