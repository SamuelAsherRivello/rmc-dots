using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace RMC.DOTS.Lessons.SpinningCube.SpinningCube_Version03_DOTS
{
    /// <summary>
    /// See <see cref="VersionComparisonComments"/>
    /// </summary>
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
            float deltaTime = SystemAPI.Time.DeltaTime;
            new SpinningCubeJob
            {
                
                DeltaTime = deltaTime
                
            }.ScheduleParallel();
        }
        
        
        [BurstCompile]
        public partial struct SpinningCubeJob : IJobEntity
        {
            public float DeltaTime;
        
            [BurstCompile]
            private void Execute(ref LocalTransform transform, in SpinningCubeComponent spinningCubeComponent)
            {
                transform = transform.Rotate(
                    quaternion.Euler(spinningCubeComponent.RotationDelta * DeltaTime));
            }
        }
    }
}