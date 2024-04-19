using System.Linq;
using RMC.DOTS.Systems.Input;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Physics.Systems;
using Unity.Transforms;

namespace RMC.DOTS.Samples.Pong2D.Pong2D_Version02_DOTS
{
    [UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
    [UpdateBefore(typeof(PhysicsSystemGroup))]
    public partial struct PaddleCPUMoveSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<ProjectileTag>();
        }

        public void OnUpdate(ref SystemState state)
        {
            // There may be more than one projectile, but we only need one
            var firstProjectileTag = SystemAPI.QueryBuilder().WithAll<ProjectileTag>().Build().ToEntityArray(Allocator.Temp)[0];

            //
            float deltaTime = SystemAPI.Time.DeltaTime;
            var localTransformHuman = state.EntityManager.GetComponentData<LocalTransform>(firstProjectileTag);
;
            foreach (var (velocity, localTransformCPU, mass, paddleMoveComponent) in 
                     SystemAPI.Query<RefRW<PhysicsVelocity>, LocalTransform, PhysicsMass, PaddleMoveComponent>().WithAll<PaddleCPUTag>())
            {
                // How far away from target?
                var deltaY = localTransformHuman.Position.y - localTransformCPU.Position.y;
                
                // Only move in the y
                float currentMoveInput = deltaY * paddleMoveComponent.Value * deltaTime;
                velocity.ValueRW.Linear.y = velocity.ValueRW.Linear.x + currentMoveInput;
            }
        }
    }
}