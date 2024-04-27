using RMC.DOTS.Systems.Input;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Physics.Extensions;
using Unity.Physics.Systems;

namespace RMC.DOTS.Samples.Pong2D.Pong2D_Version02_DOTS
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
            // First get the current input value from the PlayerMoveInput component. This component is set in the
            // GetPlayerInputSystem that runs earlier in the frame.
            float2 move = SystemAPI.GetSingleton<InputComponent>().MoveFloat2;
            float2 look = SystemAPI.GetSingleton<InputComponent>().LookFloat2;
            float deltaTime = SystemAPI.Time.DeltaTime;
            float2 moveComposite = float2.zero;
            
            // Here we support EITHER look or move to move around
            // Prioritize MOVE, if no MOVE is set, then use look
            if (move.y != 0)
            {
                moveComposite.y =  move.y;
            }
            else
            {
                moveComposite.y = look.y;
            }
            
            foreach (var (velocity, mass, paddleMoveComponent) in 
                     SystemAPI.Query<RefRW<PhysicsVelocity>,PhysicsMass, PaddleMoveComponent>().WithAll<PaddleHumanTag>())
            {
                // Only move in the y
                float currentMoveInput = moveComposite.y * paddleMoveComponent.Value * deltaTime;
                velocity.ValueRW.Linear.y = velocity.ValueRW.Linear.x + currentMoveInput;
            }
        }
    }
}