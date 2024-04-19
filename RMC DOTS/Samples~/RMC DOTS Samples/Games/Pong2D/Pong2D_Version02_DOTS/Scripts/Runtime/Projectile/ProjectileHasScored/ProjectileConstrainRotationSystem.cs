using RMC.DOTS.Systems.Input;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Physics.Systems;
using Unity.Transforms;

namespace RMC.DOTS.Samples.Pong2D.Pong2D_Version02_DOTS
{
    [UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
    [UpdateBefore(typeof(PhysicsSystemGroup))]
    public partial struct ProjectileConstrainRotationSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            foreach (var localTransform in  SystemAPI.Query<RefRW<LocalTransform>>().WithAll<ProjectileTag>())
            {
                localTransform.ValueRW.Rotation = quaternion.identity;
            }
        }
    }
}