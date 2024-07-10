using RMC.DOTS.Systems.Player;
using Unity.Entities;
using Unity.Physics.PhysicsStateful;
using UnityEngine;

namespace RMC.DOTS.Samples.Templates.DOTSGameTemplate
{
    [UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
    [UpdateAfter(typeof(StatefulTriggerEventSystem))] 
    public partial struct GoalWasReachedSystem : ISystem
    {
        private ComponentLookup<GoalTag> _goalTagComponentLookup;
        
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<GoalWasReachedSystemAuthoring.GoalWasReachedSystemIsEnabledTag>();
            state.RequireForUpdate<BeginPresentationEntityCommandBufferSystem.Singleton>();
            state.RequireForUpdate<PlayerTag>();

            _goalTagComponentLookup = state.GetComponentLookup<GoalTag>();
        }

        public void OnUpdate(ref SystemState state)
        {
            var ecb = SystemAPI.
                GetSingleton<BeginPresentationEntityCommandBufferSystem.Singleton>().
                CreateCommandBuffer(state.WorldUnmanaged);
            
            _goalTagComponentLookup.Update(ref state);
            
            foreach (var (statefulEventBuffers, playerEntity) in 
                     SystemAPI.Query<DynamicBuffer<StatefulCollisionEvent>>().
                         WithAll<PlayerTag>().
                         WithNone<GoalWasReachedExecuteOnceTag>().
                         WithEntityAccess())
            {

                for (int bufferIndex = 0; bufferIndex < statefulEventBuffers.Length; bufferIndex++)
                {
                    var statefulEvent = statefulEventBuffers[bufferIndex];
                    if (statefulEvent.State == StatefulEventState.Enter)
                    {
                        var otherEntity = statefulEvent.GetOtherEntity(playerEntity);
                        if (_goalTagComponentLookup.HasComponent(otherEntity))
                        {
                            ecb.AddComponent<GoalWasReachedExecuteOnceTag>(playerEntity);
                            break;
                        }
                     
                        break;
                    }
                }
            }
        }
    }
}