using RMC.DOTS.Systems.PhysicsTrigger;
using Unity.Burst;
using Unity.Entities;
using Unity.Physics.Systems;
using UnityEngine;

namespace RMC.Playground3D.RollABall3D_Version02_DOTS
{
    [UpdateInGroup(typeof(PhysicsSystemGroup))]
    [UpdateAfter(typeof(PhysicsSimulationGroup))]
    [UpdateBefore(typeof(ExportPhysicsWorld))]
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
            
            
            int timeFrameCount = UnityEngine.Time.frameCount;
            int framesToWait = 1; //TODO: why not lower it to '0'? I guess this sysetm runs one frame after the last one?
            
            //Remove any existing tags
            foreach (var (pickupTag, physicsTriggerOutputTag, entity) in SystemAPI.Query<PickupTag, PickupWasCollectedTag>().WithEntityAccess())
            {
                //Debug.Log($"GamePickup ({entity.Index}) Set To REMOVE on TimeFrameCount: {Time.frameCount}");
                ecb.RemoveComponent<PickupWasCollectedTag>(entity);
            }
            
            foreach (var (pickupTag, physicsTriggerOutputTag, entity) in SystemAPI.Query<PickupTag, PhysicsTriggerOutputTag>().WithEntityAccess())
            {
                if (physicsTriggerOutputTag.PhysicsTriggerType == PhysicsTriggerType.Enter &&
                    physicsTriggerOutputTag.TimeFrameCountForLastCollision <= timeFrameCount - framesToWait)
                { 
                    //Debug.Log($"GamePickup ({entity.Index}) Set To Enter on TimeFrameCount: {Time.frameCount}");
                    ecb.AddComponent<PickupWasCollectedTag>(entity);
                }
            }
        }
    }
}