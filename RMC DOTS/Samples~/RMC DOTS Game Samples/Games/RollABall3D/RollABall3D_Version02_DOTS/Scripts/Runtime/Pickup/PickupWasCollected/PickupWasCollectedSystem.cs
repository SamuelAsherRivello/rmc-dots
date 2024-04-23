using RMC.DOTS.SystemGroups;
using RMC.DOTS.Systems.DestroyEntity;
using RMC.DOTS.Systems.PhysicsTrigger;
using Unity.Burst;
using Unity.Entities;
using UnityEngine;

namespace RMC.DOTS.Samples.RollABall3D.RollABall3D_Version02_DOTS
{
    [UpdateInGroup(typeof(PauseableSystemGroup))]
    [RequireMatchingQueriesForUpdate]
    public partial struct PickupWasCollectedSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<EndInitializationEntityCommandBufferSystem.Singleton>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var ecb = SystemAPI.
                GetSingleton<EndInitializationEntityCommandBufferSystem.Singleton>().
                CreateCommandBuffer(state.WorldUnmanaged);
            
            
            //Remove any existing tags
            foreach (var (pickupTag, physicsTriggerOutputTag, entity) in SystemAPI.Query<PickupTag, PickupWasCollectedTag>().WithEntityAccess())
            {
                //Debug.Log($"GamePickup ({entity.Index}) Set To REMOVE on TimeFrameCount: {Time.frameCount}");
                ecb.RemoveComponent<PickupWasCollectedTag>(entity);
            }
            
            foreach (var (pickupTag, physicsTriggerOutputTag, entity) in SystemAPI.Query<PickupTag, PhysicsTriggerOutputComponent>().WithEntityAccess())
            {
                if (physicsTriggerOutputTag.PhysicsTriggerType == PhysicsTriggerType.Enter &&
                    physicsTriggerOutputTag.TimeFrameCountForLastCollision <= Time.frameCount - PhysicsTriggerOutputComponent.FramesToWait)
                { 
                    //Debug.Log($"GamePickup ({entity.Index}) Set To Enter on TimeFrameCount: {Time.frameCount}");
                    ecb.AddComponent<PickupWasCollectedTag>(entity);
                    ecb.AddComponent<DestroyEntityComponent>(entity);
                  
                }
            }
        }
    }
}