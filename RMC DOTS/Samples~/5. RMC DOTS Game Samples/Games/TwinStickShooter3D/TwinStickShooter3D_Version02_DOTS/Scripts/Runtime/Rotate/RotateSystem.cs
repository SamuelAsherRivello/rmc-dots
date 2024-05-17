using RMC.DOTS.SystemGroups;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace RMC.DOTS.Samples.Games.TwinStickShooter3D.TwinStickShooter3D_Version02_DOTS
{
    [UpdateInGroup(typeof(PauseableSystemGroup))]
    public partial struct RotateSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<RotateSystemAuthoring.RotateSystemIsEnabledTag>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            float deltaTime = SystemAPI.Time.DeltaTime;

            foreach (var (rotateComponent, localTransform)
                     in SystemAPI.Query<RefRO<RotateComponent>, RefRW<LocalTransform>>())
            {
                localTransform.ValueRW = localTransform.ValueRW.Rotate(
                    
                    quaternion.Euler(rotateComponent.ValueRO.Direction * 
                                     rotateComponent.ValueRO.Speed * 
                                     deltaTime)
                    );
            }
        }
    }
}