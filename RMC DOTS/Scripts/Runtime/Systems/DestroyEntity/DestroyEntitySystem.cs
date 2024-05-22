using RMC.DOTS.SystemGroups;
using Unity.Burst;
using Unity.Entities;
using UnityEngine;

namespace RMC.DOTS.Systems.DestroyEntity
{
    [UpdateInGroup(typeof(UnpauseablePresentationSystemGroup))]
    [RequireMatchingQueriesForUpdate]
    public partial struct DestroyEntitySystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<DestroyEntitySystemAuthoring.DestroyEntitySystemIsEnabledTag>();
            state.RequireForUpdate<EndInitializationEntityCommandBufferSystem.Singleton>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var ecb = SystemAPI.
                GetSingleton<EndInitializationEntityCommandBufferSystem.Singleton>().
                CreateCommandBuffer(state.WorldUnmanaged);

            var deltaTime = SystemAPI.Time.DeltaTime;
            
            foreach (var (destroyEntityComponent, entity) in SystemAPI.Query<RefRW<DestroyEntityComponent>>().WithEntityAccess())
            {
                destroyEntityComponent.ValueRW.TimeTillDestroyInSeconds -= deltaTime;
                if (destroyEntityComponent.ValueRW.TimeTillDestroyInSeconds <= 0)
                {
                    // This command doesn't immediately destroy the entity. Rather it schedules the deletion of the entity
                    // for later and Unity does the actual destruction when the commands of this ECB are played back.
                    ecb.DestroyEntity(entity);
                }
            }
        }

        /// <summary>
        /// Experimental idea. This allows any scope to destroy an entity with a delay
        /// while respecting if an existing component already exists.
        ///
        /// NOTE: 1. I tried to pass in less parameters. Failed.Perhaps doable.
        /// NOTE: 2. I tried to make it non-static and lookup the system. Failed. Perhaps doable.
        /// 
        /// </summary>
        public static void DestroyEntity(
            ref EntityCommandBuffer ecb,
            ComponentLookup<DestroyEntityComponent> _destroyEntityComponentLookup, 
            Entity entity,
            float timeTillDestroyInSeconds = -1)
        {
            bool hasDestroyEntityComponent = _destroyEntityComponentLookup.HasComponent(entity);

            if (!hasDestroyEntityComponent)
            {
                ecb.AddComponent<DestroyEntityComponent>(entity, new DestroyEntityComponent
                {
                    TimeTillDestroyInSeconds = timeTillDestroyInSeconds
                });
            }
            else
            {
                var destroyEntityComponent = _destroyEntityComponentLookup.GetRefRW(entity);
                destroyEntityComponent.ValueRW.TimeTillDestroyInSeconds = timeTillDestroyInSeconds;
            }
        }

    }
}