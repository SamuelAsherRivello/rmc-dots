using Unity.Burst;
using Unity.Entities;

namespace Roguelite.Movement
{
    [UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
    [BurstCompile]
    public partial struct ResetMovementSystem : ISystem
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
            new ResetMovementJob().ScheduleParallel();
        }
    }
}