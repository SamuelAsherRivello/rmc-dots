using Roguelite.Movement;
using Unity.Burst;
using Unity.Entities;

namespace Roguelite.Tests.Infrastructure
{
    [UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
    [BurstCompile]
    public partial struct SystemApiHasComponentSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate(SystemAPI
                .QueryBuilder()
                .WithAll<MovementStatsData, GravityTag>()
                .WithAllRW<MovementDirectionData>()
                .Build());
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            foreach (var (_, _, entity) in
                     SystemAPI
                         .Query<MovementStatsData, RefRW<MovementDirectionData>>()
                         .WithAll<GravityTag>()
                         .WithEntityAccess())
            {
                if (SystemAPI.HasComponent<MovementStatsData>(entity))
                {
                    if (SystemAPI.GetComponent<MovementStatsData>(entity).Gravity > 0)
                    {
                    }
                }
            }
        }
    }
}