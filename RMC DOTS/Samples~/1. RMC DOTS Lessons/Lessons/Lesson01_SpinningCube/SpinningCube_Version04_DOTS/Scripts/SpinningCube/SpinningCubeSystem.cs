using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace RMC.DOTS.Lessons.SpinningCube.SpinningCube_Version04_DOTS
{
    //  System  ------------------------------------
    [UpdateInGroup(typeof(PresentationSystemGroup))]
    public partial struct SpinningCubeSystem : ISystem
    {
        private EntityQuery _entityQuery;
        
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<LocalTransform>();
            state.RequireForUpdate<SpinningCubeComponent>();
            
            _entityQuery = state.GetEntityQuery(
                ComponentType.ReadWrite<LocalTransform>(),
                ComponentType.ReadOnly<SpinningCubeComponent>());
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var deltaTime = SystemAPI.Time.DeltaTime;
            
            new QueryJob
            { 
                    DeltaTime = deltaTime
                    
            }.ScheduleParallel(_entityQuery);
        }
    }
    
    [BurstCompile]
    partial struct QueryJob : IJobEntity
    {
        public float DeltaTime;

        [BurstCompile]
        private void Execute(ref LocalTransform localTransform, in SpinningCubeComponent spinningCubeComponent)
        {
            var from = localTransform.Rotation;
            var delta = quaternion.Euler(spinningCubeComponent.RotationDelta * DeltaTime);
            localTransform.Rotation = math.mul(from, delta);
        }
    }
}