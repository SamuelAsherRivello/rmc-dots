using RMC.DOTS.Demos.Input;
using RMC.DOTS.SystemGroups;
using RMC.DOTS.Systems.Player;
using Unity.Burst;
using Unity.Entities;
using UnityEngine;

namespace RMC.DOTS.Demos.PhysicsTrigger
{
    [BurstCompile]
    [UpdateInGroup(typeof(PauseableSystemGroup))]
    [RequireMatchingQueriesForUpdate]
    public partial struct PlayerPhysicsTriggerSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<PlayerPhysicsTriggerSystemAuthoring.PlayerPhysicsTriggerSystemIsEnabledTag>();
        }

        
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            foreach (var (playerTag, playerWasTriggeredTag, entity) in 
                     SystemAPI.Query<RefRO<PlayerTag>, RefRO<PlayerWasTriggeredTag>>().WithEntityAccess())
            {
                Debug.Log($"The player hit a goal.\n\n");
            }
        }
    }
}