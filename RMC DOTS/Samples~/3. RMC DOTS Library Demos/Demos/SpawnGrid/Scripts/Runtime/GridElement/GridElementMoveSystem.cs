using RMC.DOTS.SystemGroups;
using RMC.DOTS.Systems.SpawnGrid;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace RMC.DOTS.Demos.SpawnGrid
{
    [UpdateInGroup(typeof(PauseableSystemGroup))]
    [BurstCompile]
    public partial class GridElementMoveSystem : SystemBase
    {
        [BurstCompile]
        protected override void OnCreate()
        {
            RequireForUpdate<SpawnGridSystemAuthoring.SpawnGridSystemIsEnabledTag>();
        }
        
        [BurstCompile]
        protected override void OnUpdate()
        {
            var elapsedTime = (float)SystemAPI.Time.ElapsedTime;
            var deltaTime = (float)SystemAPI.Time.DeltaTime;

            foreach (var (localTransform, spawnedGridElementComponent, gridElementMoveComponent) 
                     in SystemAPI.Query<RefRW<LocalTransform>, RefRO<SpawnedGridElementComponent>, RefRW<GridElementMoveComponent>>())
            {
                
                // MODIFY HERE TO CREATE NEW 'WAVE FUNCTIONS' OF MOVEMENT
                // RANDOM, FUN EXPERIMENTATION HERE...
                var localAmplitudeModifier = (spawnedGridElementComponent.ValueRO.Row + 1 ) * 1.0001f + 
                                             (spawnedGridElementComponent.ValueRO.Column + 1 ) * 1.0001f;
                
                var localSpeedModifier = (spawnedGridElementComponent.ValueRO.Floor + 1 ) * 1.0001f;
                
                
                // DO NOT TOUCH
                localTransform.ValueRW.Position.y = spawnedGridElementComponent.ValueRO.PositionInitial.y +  
                                                    gridElementMoveComponent.ValueRW.Amplitude * localAmplitudeModifier *
                                                    math.sin(elapsedTime * gridElementMoveComponent.ValueRW.Speed * localSpeedModifier);
            }
        }
    }
}
