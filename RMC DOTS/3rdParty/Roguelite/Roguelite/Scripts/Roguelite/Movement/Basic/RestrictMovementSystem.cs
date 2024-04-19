using Roguelite.Physics;
using Unity.Burst;
using Unity.Entities;

namespace Roguelite.Movement
{
    [UpdateInGroup(typeof(FixedStepSimulationSystemGroup), OrderLast = true)]
    [BurstCompile]
    public partial struct RestrictMovementSystem : ISystem
    {
        private ComponentLookup<LeftWallCollision> _leftWallCollisionLookup;
        private ComponentLookup<RightWallCollision> _rightWallCollisionLookup;

        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate(SystemAPI
                .QueryBuilder()
                .WithAllRW<MovementDirectionData>()
                .Build());
            _leftWallCollisionLookup = SystemAPI.GetComponentLookup<LeftWallCollision>(true);
            _rightWallCollisionLookup = SystemAPI.GetComponentLookup<RightWallCollision>(true);
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            _leftWallCollisionLookup.Update(ref state);
            _rightWallCollisionLookup.Update(ref state);

            new RestrictMovementJob
            {
                LeftWallCollisionLookup = _leftWallCollisionLookup,
                RightWallCollisionLookup = _rightWallCollisionLookup,
            }.Schedule();
        }
    }
}