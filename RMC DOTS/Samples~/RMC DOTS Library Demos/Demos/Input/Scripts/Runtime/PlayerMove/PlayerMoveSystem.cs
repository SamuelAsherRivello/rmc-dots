using RMC.DOTS.SystemGroups;
using RMC.DOTS.Systems.Input;
using RMC.DOTS.Systems.Player;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace RMC.DOTS.Demos.Input
{
    [BurstCompile]
    [UpdateInGroup(typeof(PauseableSystemGroup))]
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
            // First get the current input value from the PlayerMoveInput component. This component is set in the
            // GetPlayerInputSystem that runs earlier in the frame.
            float2 move = SystemAPI.GetSingleton<InputComponent>().MoveFloat2;
            float2 look = SystemAPI.GetSingleton<InputComponent>().LookFloat2;
            float deltaTime = SystemAPI.Time.DeltaTime;
            float multiplier = 10f;
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
             
            // Loop through all players. Move each
            foreach (var localTransform in 
                     SystemAPI.Query<RefRW<LocalTransform>>().WithAll<PlayerTag>())
            {
                localTransform.ValueRW.Position.x += moveComposite.x * (deltaTime * multiplier);
                localTransform.ValueRW.Position.z += moveComposite.y * (deltaTime * multiplier);
            }
        }
    }
}