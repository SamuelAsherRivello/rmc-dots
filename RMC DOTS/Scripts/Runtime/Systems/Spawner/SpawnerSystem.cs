using RMC.DOTS.SystemGroups;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace RMC.DOTS.Systems.Spawner
{
    [UpdateInGroup(typeof(PauseableSystemGroup))]
    [BurstCompile]
    public partial class SpawnerSystem : SystemBase
    {
        private EndSimulationEntityCommandBufferSystem _ecbSystem;
        
        public bool CanSpawn
        {   
            get
            {
                return SystemAPI.HasSingleton<SpawnerSystemAuthoring.SpawnerSystemIsEnabledTag>();
            }
        }
        
        public void Spawn(SpawnerRequestComponent spawnerRequestComponent)
        {
            if (!CanSpawn)
            {
                Debug.LogError("Spawn() failed because CanSpawn is false. Check CanSpawn first.");
                return;
            }

            var entity = EntityManager.CreateEntity();
            _ecbSystem.CreateCommandBuffer().AddComponent<SpawnerRequestComponent>(entity, spawnerRequestComponent);

        }
        
        [BurstCompile]
        protected override void OnCreate()
        {
            _ecbSystem = World.GetOrCreateSystemManaged<EndSimulationEntityCommandBufferSystem>();
            RequireForUpdate<SpawnerConfigurationComponent>();
            RequireForUpdate<SpawnerRequestComponent>();
            RequireForUpdate<SpawnerSystemAuthoring.SpawnerSystemIsEnabledTag>();
        }
        
        [BurstCompile]
        protected override void OnUpdate()
        {
            var ecb = _ecbSystem.CreateCommandBuffer();
            
            var localTransformLookup = GetComponentLookup<LocalTransform>();
            var spawnerConfigurationComponent = SystemAPI.GetSingleton<SpawnerConfigurationComponent>();
            
            // Iterate over the entities
            Entities.
                WithStructuralChanges().
                ForEach((Entity entity, ref SpawnerRequestComponent spawnerRequestComponent) =>
            {
                var prefab = spawnerConfigurationComponent.Prefab;
                
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
                ecb.RemoveComponent<SpawnerRequestComponent>(entity);
            })
            .WithoutBurst().Run();
        }
    }
}
