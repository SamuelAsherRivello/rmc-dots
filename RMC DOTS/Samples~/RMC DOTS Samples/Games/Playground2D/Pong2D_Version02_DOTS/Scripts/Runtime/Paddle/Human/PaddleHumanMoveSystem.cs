using RMC.DOTS.Systems.Input;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Physics.Extensions;
using Unity.Physics.Systems;

namespace RMC.Playground3D.Pong2D_Version02_DOTS
{
    [UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
    [UpdateBefore(typeof(PhysicsSystemGroup))]
    public partial struct PaddleHumanMoveSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<InputComponent>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            float2 inputComponentMove = SystemAPI.GetSingleton<InputComponent>().Move;
            float deltaTime = SystemAPI.Time.DeltaTime;

            foreach (var (velocity, mass, paddleMoveComponent) in 
                     SystemAPI.Query<RefRW<PhysicsVelocity>,PhysicsMass, PaddleMoveComponent>().WithAll<PaddleHumanTag>())
            {
                // Only move in the y
                float currentMoveInput = inputComponentMove.y * paddleMoveComponent.Value * deltaTime;
                velocity.ValueRW.Linear.y = velocity.ValueRW.Linear.x + currentMoveInput;
            }
        }
    }
}