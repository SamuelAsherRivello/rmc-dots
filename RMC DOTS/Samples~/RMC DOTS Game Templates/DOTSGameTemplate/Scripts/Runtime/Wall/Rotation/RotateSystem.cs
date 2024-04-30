using RMC.DOTS.SystemGroups;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace RMC.DOTS.Samples.Templates.DOTSGameTemplate
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
            new RotatePickupJob { DeltaTime = deltaTime }.ScheduleParallel();
        }
    }

    [BurstCompile]
    public partial struct RotatePickupJob : IJobEntity
    {
        public float DeltaTime;
        
        [BurstCompile]
        private void Execute(ref LocalTransform transform, in RotateComponent rotateComponent)
        {
            transform = transform.Rotate(quaternion.Euler(rotateComponent.Direction * rotateComponent.Speed * DeltaTime));
        }
    }
}