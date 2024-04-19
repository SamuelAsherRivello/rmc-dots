using RMC.DOTS.SystemGroups;
using RMC.DOTS.Systems.Input;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Physics.Extensions;

namespace RMC.DOTS.Samples.DOTSTemplate
{
    /// <summary>
    /// This system moves the player in 3D space.
    /// </summary>
    [UpdateInGroup(typeof(PauseableSystemGroup))]
    public partial struct MoveSystem : ISystem
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
            float2 inputComponentMove = SystemAPI.GetSingleton<InputComponent>().Move;
            float deltaTime = SystemAPI.Time.DeltaTime;

            // Although there is only one player in the game world, we still define systems as if we were executing over
            // a group of players. Inside this idiomatic foreach, we apply force to the player based off the current
            // move input and the force strength.
            foreach (var (velocity, mass, moveForce) in 
                     SystemAPI.Query<RefRW<PhysicsVelocity>,PhysicsMass, MoveComponent>().WithAll<PlayerTag>())
            {
                float3 moveInput3d = new float3(inputComponentMove.x, 0f, inputComponentMove.y);
                float3 currentMoveInput = moveInput3d * moveForce.Value * deltaTime;
                velocity.ValueRW.ApplyLinearImpulse(in mass, currentMoveInput);
            }
        }
    }
}