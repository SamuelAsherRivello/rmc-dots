using RMC.DOTS.SystemGroups;
using RMC.DOTS.Systems.GameState;
using Unity.Burst;
using Unity.Entities;
using Unity.Physics;
using Unity.Transforms;
using RMC.DOTS.Systems.FollowTarget;
using UnityEngine;

namespace RMC.DOTS.Samples.Games.TwinStickShooter3D.TwinStickShooter3D_Version02_DOTS
{
    [UpdateInGroup(typeof(PauseableSystemGroup))]
    [BurstCompile]
    public partial struct EnemyMoveSystem : ISystem
    {
        private ComponentLookup<LocalTransform> _localTransformLookup;
        private LayerMask _layerMask;

        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<EnemyMoveSystemAuthoring.EnemyMoveSystemIsEnabledTag>();
            state.RequireForUpdate<EnemyTag>();
            state.RequireForUpdate<EnemyMoveComponent>();
            state.RequireForUpdate<LocalTransform>();
            state.RequireForUpdate<PhysicsMass>();
            state.RequireForUpdate<PhysicsVelocity>();
            _localTransformLookup = state.GetComponentLookup<LocalTransform>();
            state.RequireForUpdate<GameStateComponent>();
        }
		
        public void OnUpdate(ref SystemState state)
        {
            GameStateComponent gameStateComponent = SystemAPI.GetSingleton<GameStateComponent>();

            bool enable = gameStateComponent.GameState == GameState.RoundStarted;
            foreach (var followerComponent in SystemAPI.Query<RefRW<FollowerComponent>>().WithAll<EnemyTag>())
                followerComponent.ValueRW.IsEnabled = enable;
        }
    }
}