using RMC.DOTS.SystemGroups;
using RMC.DOTS.Systems.Player;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace RMC.DOTS.Samples.Templates.DOTSGameTemplate
{
    [UpdateInGroup(typeof(PauseableSystemGroup))]
    [RequireMatchingQueriesForUpdate]
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
            
            foreach (var (localTransform, playerTag, goalWasReached, entity) in 
                     SystemAPI.Query<RefRW<LocalTransform>,PlayerTag, GoalWasReachedTag>().WithEntityAccess())
            {
                ecb.AddComponent<PlayerResetPositionExecuteOnceTag>(entity);
            }
        }
    }
}