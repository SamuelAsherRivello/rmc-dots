using Roguelite.Physics;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

namespace Roguelite.Movement
{
    [BurstCompile]
    public partial struct RestrictMovementJob : IJobEntity
    {
        [ReadOnly] internal ComponentLookup<LeftWallCollision> LeftWallCollisionLookup;
        [ReadOnly] internal ComponentLookup<RightWallCollision> RightWallCollisionLookup;

        [BurstCompile]
        private void Execute(
            Entity entity,
            ref MovementDirectionData movementDirectionData)
        {
            var hasCollisionOnLeft = LeftWallCollisionLookup.HasComponent(entity);
            var hasCollisionOnRight = RightWallCollisionLookup.HasComponent(entity);

            switch (movementDirectionData.Movement.x)
            {
                case > 0 when hasCollisionOnRight:
                case < 0 when hasCollisionOnLeft:
                    movementDirectionData.Movement = new float3(
                        0,
                        movementDirectionData.Movement.y,
                        movementDirectionData.Movement.z);
                    break;
            }
        }
    }
}