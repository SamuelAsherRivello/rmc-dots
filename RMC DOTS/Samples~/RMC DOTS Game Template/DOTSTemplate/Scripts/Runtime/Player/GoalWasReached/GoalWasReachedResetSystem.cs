using RMC.DOTS.SystemGroups;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace RMC.DOTS.Samples.DOTSTemplate
{
    [UpdateInGroup(typeof(PauseableSystemGroup))]
    [RequireMatchingQueriesForUpdate]
    public partial struct GoalWasReachedResetSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            // We will use one of the built-in entity command buffers to destroy the pickup entity
            // So we just put this line in here to ensure this exists before writing commands to it
            state.RequireForUpdate<EndInitializationEntityCommandBufferSystem.Singleton>();
        }

        /// <summary>
        /// OnUpdate methods are typically ran every frame, but due to the [RequireMatchingQueriesForUpdate] attribute
        /// on this ISystem struct, it will only run when the query defined below has matching entities. Meaning this
        /// system will only run when there is an entity that has been picked up that needs to be destroyed.
        /// </summary>
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var ecb = SystemAPI.
                GetSingleton<EndInitializationEntityCommandBufferSystem.Singleton>().
                CreateCommandBuffer(state.WorldUnmanaged);

            // Iterate through all the entities that have a "PickedUpThisFrameTag" on it. We'll just need a reference
            // to the entity so we can destroy it.
            foreach (var (localTransform, playerTag, goalWasReached) in 
                     SystemAPI.Query<RefRW<LocalTransform>,PlayerTag, GoalWasReachedTag>().WithAll<PlayerTag>())
            {
                localTransform.ValueRW.Position = new float3(-3, 0, 0);

            }
        }
    }
}