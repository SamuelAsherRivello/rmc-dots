using Unity.Burst;
using Unity.Entities;
using Unity.Physics.Systems;

namespace RMC.Playground3D.RollABall3D_Version02_DOTS
{
    [UpdateInGroup(typeof(PhysicsSystemGroup))]
    [UpdateAfter(typeof(PhysicsSimulationGroup))]
    [UpdateBefore(typeof(ExportPhysicsWorld))]
    [RequireMatchingQueriesForUpdate]
    public partial struct PickupWasCollectedDestroySystem : ISystem
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
            foreach (var (pickupTag, pickedUpThisFrameTag, entity) in SystemAPI.Query<PickupTag, PickupWasCollectedTag>().WithEntityAccess())
            {
                // This command doesn't immediately destroy the entity. Rather it schedules the deletion of the entity
                // for later and Unity does the actual destruction when the commands of this ECB are played back.
                ecb.DestroyEntity(entity);
            }
        }
    }
}