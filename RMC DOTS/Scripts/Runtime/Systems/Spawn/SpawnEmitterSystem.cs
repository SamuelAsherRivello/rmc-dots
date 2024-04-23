using RMC.DOTS.SystemGroups;
using Unity.Burst;
using Unity.Entities;
using UnityEngine;

namespace RMC.DOTS.Systems.Spawn
{
    [UpdateInGroup(typeof(PauseableSystemGroup))]
    [BurstCompile]
    public partial struct SpawnEmitterSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<SpawnSystemAuthoring.SpawnerSystemIsEnabledTag>();
            state.RequireForUpdate<EndInitializationEntityCommandBufferSystem.Singleton>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var ecb = SystemAPI.
                GetSingleton<EndInitializationEntityCommandBufferSystem.Singleton>().
                CreateCommandBuffer(state.WorldUnmanaged);
            
            var deltaTime = SystemAPI.Time.DeltaTime;

            foreach (var (spawnEmitterComponent, entity) in SystemAPI.Query<RefRW<SpawnEmitterComponent>>()
                         .WithEntityAccess())
            {
                spawnEmitterComponent.ValueRW.TimeTillSpawnInSeconds -= deltaTime;
                if (spawnEmitterComponent.ValueRO.TimeTillSpawnInSeconds <= 0)
                {
                    // Add the SPAWN
                    //Let's create X this time
                    var spawnsPerOperation = spawnEmitterComponent.ValueRO.SpawnsPerOperation;

                    for (int i = 0; i < spawnsPerOperation; i++)
                    {
                        //And be sure we don't create more than the total lifetime supply
                        if (--spawnEmitterComponent.ValueRW.SpawnsPerTotal >= 0)
                        {
                            var newEntity = ecb.CreateEntity();
                            ecb.AddComponent<SpawnRequestComponent>(newEntity,
                                new SpawnRequestComponent
                                {
                                    Position = spawnEmitterComponent.ValueRO.SpawnPosition
                                });
                        }
                    }
                    
                    if (spawnEmitterComponent.ValueRW.SpawnsPerTotal <= 0)
                    {
                        ecb.RemoveComponent<SpawnEmitterComponent>(entity);
                    }
                    else
                    {
                        // Remove the COMPONENT for the emitter (But keep the entity in case its visual)
                        spawnEmitterComponent.ValueRW.TimeTillSpawnInSeconds = spawnEmitterComponent.ValueRW.TimeTillSpawnInSecondsRO;
                    }
                }
            }
        }
    }
}
