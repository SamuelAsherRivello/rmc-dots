using RMC.DOTS.SystemGroups;
using RMC.DOTS.Systems.Player;
using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;

namespace RMC.DOTS.Samples.Templates.DOTSGameTemplate
{
    [UpdateInGroup(typeof(UnpauseablePresentationSystemGroup))]
    [UpdateBefore(typeof(GoalWasReachedCleanupSystem))]
    public partial struct GoalWasReachedResetSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<GoalWasReachedSystemAuthoring.GoalWasReachedSystemIsEnabledTag>();
            state.RequireForUpdate<BeginPresentationEntityCommandBufferSystem.Singleton>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var ecb = SystemAPI.
                GetSingleton<BeginPresentationEntityCommandBufferSystem.Singleton>().
                CreateCommandBuffer(state.WorldUnmanaged);
            
            foreach (var (localTransform, entity) in 
                     SystemAPI.Query<RefRW<LocalTransform>>().
                         WithAll<PlayerTag,GoalWasReachedExecuteOnceTag>().
                         WithEntityAccess())
            {
                ecb.AddComponent<PlayerResetPositionExecuteOnceTag>(entity);
            }
        }
    }
}