using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace RMC.DOTS.Lessons.SpinningCube.SpinningCube_Version02_DOTS_A
{
    //  System  ------------------------------------
    [BurstCompile]
    [UpdateInGroup(typeof(PresentationSystemGroup))]
    public partial struct SpinningCubeSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<LocalTransform>();
            state.RequireForUpdate<SpinningCubeComponent>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state) 
        {
            foreach (var (localTransform, spinningCubeComponent)
                     in SystemAPI.Query<RefRW<LocalTransform>, RefRO<SpinningCubeComponent>>())
            {
                var from = localTransform.ValueRW.Rotation;
                var delta = quaternion.Euler(spinningCubeComponent.ValueRO.RotationDelta * SystemAPI.Time.DeltaTime);
                localTransform.ValueRW.Rotation = math.mul(from, delta);
            }
        }
    }
}