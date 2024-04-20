using RMC.DOTS.SystemGroups;
using Unity.Burst;
using Unity.Entities;

namespace RMC.DOTS.Systems.DestroyEntity
{
    [UpdateInGroup(typeof(UnpauseableSystemGroup))]
    [RequireMatchingQueriesForUpdate]
    public partial struct DestroyEntitySystem : ISystem
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
            
            foreach (var (destroyEntityTag, entity) in SystemAPI.Query<RefRO<DestroyEntityTag>>().WithEntityAccess())
            {
                // This command doesn't immediately destroy the entity. Rather it schedules the deletion of the entity
                // for later and Unity does the actual destruction when the commands of this ECB are played back.
                ecb.DestroyEntity(entity);
            }
        }
    }
}