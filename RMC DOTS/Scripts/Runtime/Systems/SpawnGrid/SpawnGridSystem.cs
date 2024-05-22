using RMC.DOTS.SystemGroups;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace RMC.DOTS.Systems.SpawnGrid
{
    [UpdateInGroup(typeof(PauseablePresentationSystemGroup))]
    [BurstCompile]
    public partial class SpawnGridSystem : SystemBase
    {
        private EndSimulationEntityCommandBufferSystem _ecbSystem;

        [BurstCompile]
        protected override void OnCreate()
        {
            _ecbSystem = World.GetOrCreateSystemManaged<EndSimulationEntityCommandBufferSystem>();
            RequireForUpdate<SpawnGridSystemAuthoring.SpawnGridSystemIsEnabledTag>();
        }

        [BurstCompile]
        protected override void OnUpdate()
        {
            var ecb = _ecbSystem.CreateCommandBuffer();
            var localTransformLookup = GetComponentLookup<LocalTransform>();
            var spawnedGridElementComponentLookup = GetComponentLookup<SpawnedGridElementComponent>();

            Entities
                .WithStructuralChanges().WithAll<SpawnGridSystemExecuteOnceTag>()
                .ForEach((Entity entity, ref SpawnGridComponent spawnGridComponent) =>
                {
                    var instanceCount = spawnGridComponent.Rows * spawnGridComponent.Columns * spawnGridComponent.Floors;
                    var instances = EntityManager.Instantiate(spawnGridComponent.Prefab, instanceCount, Allocator.Temp);

                    for (var i = 0; i < instances.Length; i++)
                    {
                        float rowSpacing = 0;
                        if (spawnGridComponent.Rows > 1)
                        {
                            rowSpacing = (spawnGridComponent.ToPosition.x - spawnGridComponent.FromPosition.x) / (spawnGridComponent.Rows - 1);
                        }

                        float colSpacing = 0;
                        if (spawnGridComponent.Columns > 1)
                        {
                            colSpacing = (spawnGridComponent.ToPosition.z - spawnGridComponent.FromPosition.z) / (spawnGridComponent.Columns - 1);
                        }

                        float floorSpacing = 0;
                        if (spawnGridComponent.Floors > 1)
                        {
                            floorSpacing = (spawnGridComponent.ToPosition.y - spawnGridComponent.FromPosition.y) / (spawnGridComponent.Floors - 1);
                        }

                        Entity instanceEntity = instances[i];
                        var transform = localTransformLookup.GetRefRW(instanceEntity);

                        // Calculate the position based on the grid parameters
                        int row = i % spawnGridComponent.Rows;
                        int column = (i / spawnGridComponent.Rows) % spawnGridComponent.Columns;
                        int floor = i / (spawnGridComponent.Rows * spawnGridComponent.Columns);

                        Vector3 position = new Vector3(
                            spawnGridComponent.FromPosition.x + row * rowSpacing,
                            spawnGridComponent.FromPosition.y + floor * floorSpacing,
                            spawnGridComponent.FromPosition.z + column * colSpacing
                        );

                        // Set the position of the transform
                        transform.ValueRW.Position = position;
                        transform.ValueRW.Rotation = quaternion.identity;
                        
                        //Store the initial position
                        //NOTE: This is not used by "RMC DOTS" library, except in the Demos
                        ecb.AddComponent<SpawnedGridElementComponent>(instanceEntity, new SpawnedGridElementComponent
                        {
                            PositionInitial = transform.ValueRW.Position,
                            Row = row,
                            Column = column,
                            Floor = floor
                        });

                    }
                    ecb.RemoveComponent<SpawnGridSystemExecuteOnceTag>(entity);
                })
                .WithoutBurst()
                .Run();
        }
    }
}
