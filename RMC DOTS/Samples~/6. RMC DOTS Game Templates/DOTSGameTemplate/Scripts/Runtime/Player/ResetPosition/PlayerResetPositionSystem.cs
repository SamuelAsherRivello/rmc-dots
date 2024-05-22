using RMC.DOTS.SystemGroups;
using RMC.DOTS.Systems.Audio;
using RMC.DOTS.Systems.Player;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;
using UnityEngine;


namespace RMC.DOTS.Samples.Templates.DOTSGameTemplate
{
    [UpdateInGroup(typeof(UnpauseablePresentationSystemGroup))]
    public partial struct PlayerResetPositionSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<PlayerResetPositionSystemAuthoring.PlayerResetPositionSystemIsEnabledTag>();
            state.RequireForUpdate<BeginPresentationEntityCommandBufferSystem.Singleton>();
            state.RequireForUpdate<PlayerResetPositionExecuteOnceTag>();
            state.RequireForUpdate<PlayerTag>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var ecb = SystemAPI.
                GetSingleton<BeginPresentationEntityCommandBufferSystem.Singleton>().
                CreateCommandBuffer(state.WorldUnmanaged);
            
            foreach (var (localTransform, physicsVelocity, entity) in 
                     SystemAPI.Query<RefRW<LocalTransform>, RefRW<PhysicsVelocity>>().
                         WithAll<PlayerTag, PlayerResetPositionExecuteOnceTag>().
                         WithEntityAccess())
            {
                
                //
                Entity audioEntity = ecb.CreateEntity();
                ecb.AddComponent<AudioComponent>(audioEntity,
                    new AudioComponent
                    (
                         "Click01"
                    ));
                
                //
                localTransform.ValueRW.Position = new float3(-3, 0, 0);
                localTransform.ValueRW.Rotation = quaternion.identity;
                
                //
                physicsVelocity.ValueRW.Linear = float3.zero;
                physicsVelocity.ValueRW.Angular = float3.zero;
                
                //
                ecb.RemoveComponent<PlayerResetPositionExecuteOnceTag>(entity);
                
            }
        }
    }
}