using RMC.DOTS.SystemGroups;
using RMC.DOTS.Systems.Player;
using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;

namespace RMC.DOTS.Samples.Templates.DOTSGameTemplate
{
    [UpdateInGroup(typeof(UnpauseableSystemGroup))]
    public partial struct PlayerFallSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<
                PlayerFallSystemAuthoring.PlayerFallSystemIsEnabledTag>();
            state.RequireForUpdate<BeginPresentationEntityCommandBufferSystem.Singleton>();
            state.RequireForUpdate<PlayerTag>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var ecb = SystemAPI.
                GetSingleton<BeginPresentationEntityCommandBufferSystem.Singleton>().
                CreateCommandBuffer(state.WorldUnmanaged);
            
            foreach (var (localTransform, entity) in SystemAPI.Query<RefRW<LocalTransform>>().WithAll<PlayerTag>().WithEntityAccess())
            {
                if (localTransform.ValueRW.Position.y < -10)
                {
                    ecb.AddComponent<PlayerResetPositionExecuteOnceTag>(entity);
                }
            }
        }
    }
}