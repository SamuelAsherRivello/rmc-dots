using RMC.DOTS.SystemGroups;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace RMC.DOTS.Systems.Spawn
{
    [UpdateInGroup(typeof(PauseablePresentationSystemGroup))]
    [BurstCompile]
    public partial class SpawnSystem : SystemBase
    {
        private EndSimulationEntityCommandBufferSystem _ecbSystem;
        
        public bool CanSpawn
        {   
            get
            {
                return SystemAPI.HasSingleton<SpawnSystemAuthoring.SpawnerSystemIsEnabledTag>();
            }
        }
        
        public void Spawn(SpawnRequestComponent spawnRequestComponent)
        {
            if (!CanSpawn)
            {
                Debug.LogError("Spawn() failed because CanSpawn is false. Check CanSpawn first.");
                return;
            }

            var entity = EntityManager.CreateEntity();
            _ecbSystem.CreateCommandBuffer().AddComponent<SpawnRequestComponent>(entity, spawnRequestComponent);

        }
        
        [BurstCompile]
        protected override void OnCreate()
        {
            _ecbSystem = World.GetOrCreateSystemManaged<EndSimulationEntityCommandBufferSystem>();
            RequireForUpdate<SpawnConfigurationComponent>();
            RequireForUpdate<SpawnRequestComponent>();
            RequireForUpdate<SpawnSystemAuthoring.SpawnerSystemIsEnabledTag>();
        }
        
        [BurstCompile]
        protected override void OnUpdate()
        {
            var ecb = _ecbSystem.CreateCommandBuffer();
            
            var localTransformLookup = GetComponentLookup<LocalTransform>();
            var spawnerConfigurationComponent = SystemAPI.GetSingleton<SpawnConfigurationComponent>();
            
            // Iterate over the entities
            Entities.
                WithStructuralChanges().
                ForEach((Entity entity, ref SpawnRequestComponent spawnerRequestComponent) =>
            {
                var prefab = spawnerConfigurationComponent.SpawnPrefab;
                
                //Spawn just ONE, but allow for more later...
                var instances = EntityManager.Instantiate(prefab, 1, Allocator.Temp);
                for (var i = 0; i < instances.Length; i++)
                {
                    Entity instanceEntity = instances[i];
                    var transform = localTransformLookup.GetRefRW(instanceEntity);
                    transform.ValueRW.Position = spawnerRequestComponent.Position;
                    transform.ValueRW.Rotation = quaternion.identity;
                    ecb.AddComponent<WasSpawnedTag>(instanceEntity);
                }
                ecb.RemoveComponent<SpawnRequestComponent>(entity);
            })
            .WithoutBurst().Run();
        }
    }
}
