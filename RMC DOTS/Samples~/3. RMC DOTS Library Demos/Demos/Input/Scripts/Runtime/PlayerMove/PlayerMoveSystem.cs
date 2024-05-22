using RMC.DOTS.SystemGroups;
using RMC.DOTS.Systems.Input;
using RMC.DOTS.Systems.Player;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace RMC.DOTS.Demos.Input
{
    [BurstCompile]
    [UpdateInGroup(typeof(PauseablePresentationSystemGroup))]
    public partial struct PlayerMoveSystem : ISystem
    {
        
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<PlayerMoveSystemAuthoring.PlayerMoveSystemIsEnabledTag>();
            state.RequireForUpdate<InputComponent>();
        }

        
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            ////////////////////////////////////////
            // #1 - Get a reference to the InputComponent
            // which is calculated for you by RMC DOTS
            var inputComponent = SystemAPI.GetSingleton<InputComponent>();
            
            ////////////////////////////////////////
            // #2 - Store some/all local references
            // for easier access
            float2 move = inputComponent.MoveFloat2;                 //WASD, Gamepad Left Stick
            float2 look = inputComponent.LookFloat2;                 //ARROWS, Gamepad Right Stick
            bool isPressedAction1 = inputComponent.IsPressedAction1; //SPACE, South Button
            bool isPressedAction2 = inputComponent.IsPressedAction2; //ENTER, East Button
            
            ////////////////////////////////////////
            // #3 - Handle the input however you like
            // for the needs of your game ...
            float deltaTime = SystemAPI.Time.DeltaTime;
            float moveSpeed = 10f;
            float scaleSpeed = 8f;
            float2 moveComposite = float2.zero;
            
            // Use left stick *OR* right stick for movement
            if (math.length(move) > 0.001f)
            {
                moveComposite = move;
            }
            else
            {
                moveComposite = look;
            }
            
            // Use any buttons to double scale
            float scale = 1;
            if (isPressedAction1 || isPressedAction2)
            {
                scale = 2;
            }

             
            // Move the player
            foreach (var localTransform in 
                     SystemAPI.Query<RefRW<LocalTransform>>().WithAll<PlayerTag>())
            {
                localTransform.ValueRW.Position.x += moveComposite.x * (deltaTime * moveSpeed);
                localTransform.ValueRW.Position.z += moveComposite.y * (deltaTime * moveSpeed);
            }
            
            
            // Scale the player
            foreach (var localTransform in 
                     SystemAPI.Query<RefRW<LocalTransform>>().WithAll<PlayerTag>())
            {
                localTransform.ValueRW.Scale = 
                    math.lerp(localTransform.ValueRW.Scale, scale, deltaTime * scaleSpeed);
            }
        }
    }
}