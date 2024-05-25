using Unity.Burst;
using Unity.Entities;
using Unity.Physics.PhysicsStateful;
using UnityEngine;

//TODO: FixPhysics
namespace RMC.DOTS.Samples.RollABall3D.RollABall3D_Version02_DOTS
{
    [UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
    [UpdateAfter(typeof(StatefulTriggerEventSystem))] 
    public partial struct PickupWasCollectedSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<BeginPresentationEntityCommandBufferSystem.Singleton>();
            state.RequireForUpdate<PickupTag>();
        }
        
        //NEW SYNTAX
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var ecb = SystemAPI.
                GetSingleton<BeginPresentationEntityCommandBufferSystem.Singleton>().
                CreateCommandBuffer(state.WorldUnmanaged);
            
            
            // ADD NEW TAG
            foreach (var (statefulEventBuffers, entity) in 
                     SystemAPI.Query<DynamicBuffer<StatefulTriggerEvent>>().
                         WithNone<PickupWasCollectedTag>().
                         WithAll<PickupTag>().
                         WithEntityAccess())
            {
                
                for (int bufferIndex = 0; bufferIndex < statefulEventBuffers.Length; bufferIndex++)
                {
                    var statefulEvent = statefulEventBuffers[bufferIndex];
                    if (statefulEvent.State == StatefulEventState.Enter)
                    {
                        //DO SOMETHING
                        //Debug.Log($"GamePickup ({entity.Index}) Set To Enter on TimeFrameCount: {Time.frameCount}");
                        ecb.AddComponent<PickupWasCollectedTag>(entity);
                      
                        break;
                    }
                }
            }
        }
    }
}