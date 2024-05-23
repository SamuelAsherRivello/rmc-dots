using RMC.DOTS.SystemGroups;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace RMC.DOTS.Samples.RollABall3D.RollABall3D_Version02_DOTS
{
    [UpdateInGroup(typeof(PauseablePresentationSystemGroup))]
    public partial struct PickupRotationSystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            // Although we get deltaTime in a different way than in Unity MonoBehaviours, it still works the same
            float deltaTime = SystemAPI.Time.DeltaTime;
            
            // Schedule the job to rotate all pickup entities. This is using the ScheduleParallel option which means it
            // can execute in parallel to other jobs, so long as those jobs aren't reading/writing from the components
            // this job is writing to.
            new RotatePickupJob { DeltaTime = deltaTime }.ScheduleParallel();
        }
    }

    /// <summary>
    /// This job rotates the all pickups in the game world for a visual effect. Rotation speed and direction are
    /// determined by the PickupRotation component
    /// </summary>
    [BurstCompile]
    public partial struct RotatePickupJob : IJobEntity
    {
        public float DeltaTime;
        
        [BurstCompile]
        private void Execute(ref LocalTransform transform, in PickupRotationComponent pickupRotationComponent)
        {
            transform = transform.Rotate(quaternion.Euler(pickupRotationComponent.Direction * pickupRotationComponent.Speed * DeltaTime));
        }
    }
}