using RMC.DOTS.SystemGroups;
using RMC.DOTS.Systems.DestroyEntity;
using Unity.Burst;
using Unity.Entities;
using Unity.Physics.PhysicsStateful;
using UnityEngine;

//TODO: FixPhysics
namespace RMC.DOTS.Samples.RollABall3D.RollABall3D_Version02_DOTS
{
    [UpdateInGroup(typeof(PauseablePresentationSystemGroup))]
    [RequireMatchingQueriesForUpdate]
    public partial struct PickupWasCollectedSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<EndInitializationEntityCommandBufferSystem.Singleton>();
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
        //     
        //     //Remove any existing tags
        //     foreach (var (pickupTag, physicsTriggerOutputTag, entity) in SystemAPI.Query<PickupTag, PickupWasCollectedTag>().WithEntityAccess())
        //     {
        //         //Debug.Log($"GamePickup ({entity.Index}) Set To REMOVE on TimeFrameCount: {Time.frameCount}");
        //         ecb.RemoveComponent<PickupWasCollectedTag>(entity);
        //     }
        //     
        //     foreach (var (pickupTag, physicsTriggerOutputTag, entity) in SystemAPI.Query<PickupTag, PhysicsTriggerOutputComponent>().WithEntityAccess())
        //     {
        //         if (physicsTriggerOutputTag.PhysicsTriggerType == PhysicsTriggerType.Enter &&
        //             physicsTriggerOutputTag.TimeFrameCountForLastCollision <= Time.frameCount - PhysicsTriggerOutputComponent.FramesToWait)
        //         { 
        //             //Debug.Log($"GamePickup ({entity.Index}) Set To Enter on TimeFrameCount: {Time.frameCount}");
        //             ecb.AddComponent<PickupWasCollectedTag>(entity);
        //             ecb.AddComponent<DestroyEntityComponent>(entity);
        //           
        //         }
        //     }
        // }
    }
}