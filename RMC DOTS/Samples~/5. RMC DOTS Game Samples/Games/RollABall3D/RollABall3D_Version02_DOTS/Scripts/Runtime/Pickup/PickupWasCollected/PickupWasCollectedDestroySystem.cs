using RMC.DOTS.SystemGroups;
using RMC.DOTS.Systems.DestroyEntity;
using Unity.Burst;
using Unity.Entities;

namespace RMC.DOTS.Samples.RollABall3D.RollABall3D_Version02_DOTS
{
    [UpdateInGroup(typeof(UnpauseablePresentationSystemGroup))]
    public partial struct PickupWasCollectedDestroySystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<EndInitializationEntityCommandBufferSystem.Singleton>();
            state.RequireForUpdate<PickupTag>();
            state.RequireForUpdate<PickupWasCollectedTag>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var ecb = SystemAPI.
                GetSingleton<EndInitializationEntityCommandBufferSystem.Singleton>().
                CreateCommandBuffer(state.WorldUnmanaged);
            
            foreach (var (pickupTag, pickupWasCollectedTag, entity) 
                     in SystemAPI.Query<PickupTag, PickupWasCollectedTag>().
                         WithEntityAccess())
            {
                ecb.RemoveComponent<PickupWasCollectedTag>(entity);
                ecb.AddComponent<DestroyEntityComponent>(entity);
            }
        }
    }
}