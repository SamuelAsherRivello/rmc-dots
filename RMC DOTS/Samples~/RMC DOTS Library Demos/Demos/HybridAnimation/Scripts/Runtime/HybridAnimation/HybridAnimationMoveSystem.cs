using RMC.DOTS.Demos.HybridAnimation;
using RMC.DOTS.SystemGroups;
using RMC.DOTS.Systems.Input;
using RMC.DOTS.Systems.Player;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace RMC.DOTS.Demos.HybridAnimation
{
    [BurstCompile]
    [UpdateInGroup(typeof(PauseableSystemGroup))]
    public partial struct HybridAnimationMoveSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<HybridAnimationSystemAuthoring.HybridAnimationSystemIsEnabledTag>();
            state.RequireForUpdate<InputComponent>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            // First get the current input value from the PlayerMoveInput component. This component is set in the
            // GetPlayerInputSystem that runs earlier in the frame.
            float2 move = SystemAPI.GetSingleton<InputComponent>().Move;
            float2 look = SystemAPI.GetSingleton<InputComponent>().Look;
            float deltaTime = SystemAPI.Time.DeltaTime;
            float linearSpeed = 10f;
            float angularSpeed = 30f;
            float2 moveComposite = float2.zero;
            
            // Here we support EITHER look or move to move around
            // Prioritize MOVE, if no MOVE is set, then use look
            if (move.x != 0)
            {
                moveComposite.x = move.x;
            }
            else
            {
                moveComposite.x = look.x;
            }

            if (move.y != 0)
            {
                moveComposite.y =  move.y;
            }
            else
            {
                moveComposite.y = look.y;
            }
            
            bool isMoving = math.length(moveComposite) > 0;
             
            // Loop through all players. Move each
            foreach (var (localTransform, hybridAnimationAnimatorComponent) in
                     SystemAPI.Query<LocalTransform, HybridAnimationAnimatorComponent>())
            {
                
                // Keyframes
                hybridAnimationAnimatorComponent.Value.SetFloat("Blend", isMoving ? 1 : 0);

                // Move
                var movement = new Vector3(
                    moveComposite.x,
                    0,
                    moveComposite.y
                ) * deltaTime * linearSpeed;

                hybridAnimationAnimatorComponent.Value.transform.position += movement;

                // Face
                if (movement.magnitude > 0.01f)
                {
                    var targetRotation =
                        quaternion.LookRotation(movement, Vector3.up);

                    hybridAnimationAnimatorComponent.Value.transform.rotation =
                        math.slerp(hybridAnimationAnimatorComponent.Value.transform.rotation, 
                            targetRotation,
                            angularSpeed * deltaTime);
                }
            }
        }
    }
}