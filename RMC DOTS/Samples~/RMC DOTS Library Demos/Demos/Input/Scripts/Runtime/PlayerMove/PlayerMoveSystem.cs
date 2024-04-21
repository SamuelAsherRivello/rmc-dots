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
            // First get the current input value from the InputComponent component.
            // This component is set in the InputSystem that runs every frame.
            float2 move = SystemAPI.GetSingleton<InputComponent>().Move;
            float deltaTime = SystemAPI.Time.DeltaTime;
            
            // Loop through all players. Move each
            foreach (var localTransform in 
                     SystemAPI.Query<RefRW<LocalTransform>>().WithAll<PlayerTag>())
            {
                localTransform.ValueRW.Position.x += move.x * deltaTime * 10;
                localTransform.ValueRW.Position.z += move.y * deltaTime * 10;
            }
        }
    }
}