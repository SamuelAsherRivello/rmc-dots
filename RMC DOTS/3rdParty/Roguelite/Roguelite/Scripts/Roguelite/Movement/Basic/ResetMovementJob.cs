using Unity.Burst;
using Unity.Entities;

namespace Roguelite.Movement
{
    [BurstCompile]
    public partial struct ResetMovementJob : IJobEntity
    {
        [BurstCompile]
        private void Execute(ref MovementDirectionData movementDirectionData)
        {
            movementDirectionData.Movement = default;
            movementDirectionData.HorizontalInput = default;
        }
    }
}