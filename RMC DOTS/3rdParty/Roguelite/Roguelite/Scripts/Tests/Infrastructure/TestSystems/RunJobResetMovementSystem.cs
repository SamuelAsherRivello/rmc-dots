using Roguelite.Movement;
using Unity.Burst;
using Unity.Entities;

namespace Roguelite.Tests.Infrastructure
{
    [UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
    [BurstCompile]
    [DisableAutoCreation]
    public partial struct RunJobResetMovementSystem : ISystem
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
            new ResetMovementJob().Run();
        }
    }
}