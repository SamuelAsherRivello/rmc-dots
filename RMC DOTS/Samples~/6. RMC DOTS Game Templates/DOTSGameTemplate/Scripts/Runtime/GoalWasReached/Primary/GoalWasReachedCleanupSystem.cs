using RMC.DOTS.SystemGroups;
using RMC.DOTS.Systems.Player;
using Unity.Entities;
using UnityEngine;

namespace RMC.DOTS.Samples.Templates.DOTSGameTemplate
{
    [UpdateInGroup(typeof(UnpauseablePresentationSystemGroup))]
    [UpdateAfter(typeof(PlayerResetPositionSystem))]
    public partial struct GoalWasReachedCleanupSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<GoalWasReachedSystemAuthoring.GoalWasReachedSystemIsEnabledTag>();
            state.RequireForUpdate<BeginPresentationEntityCommandBufferSystem.Singleton>();
            state.RequireForUpdate<PlayerTag>();
        }

        public void OnUpdate(ref SystemState state)
        {
            var ecb = SystemAPI.
                GetSingleton<BeginPresentationEntityCommandBufferSystem.Singleton>().
                CreateCommandBuffer(state.WorldUnmanaged);
            
            foreach (var (playerTag, entity) in 
                     SystemAPI.Query<RefRO<PlayerTag>>().
                         WithAll<GoalWasReachedExecuteOnceTag>().
                         WithEntityAccess())
            {
                ecb.RemoveComponent<GoalWasReachedExecuteOnceTag>(entity);
            }
        }
    }
}