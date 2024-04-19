using Roguelite.Movement;
using Unity.Burst;
using Unity.Entities;

namespace Roguelite.Tests.Infrastructure
{
    [UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
    [BurstCompile]
    [DisableAutoCreation]
    public partial struct MainThreadResetMovementSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate(SystemAPI
                .QueryBuilder()
                .WithAllRW<MovementDirectionData>()
                .Build());
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            foreach (var movementDirectionData in SystemAPI.Query<RefRW<MovementDirectionData>>())
            {
                movementDirectionData.ValueRW.Movement = default;
                movementDirectionData.ValueRW.HorizontalInput = default;
            }
        }
    }
}