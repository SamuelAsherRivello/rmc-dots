using RMC.DOTS.SystemGroups;
using RMC.DOTS.Systems.Culling;
using RMC.DOTS.Systems.Player;
using Unity.Burst;
using Unity.Entities;
using UnityEngine;

namespace RMC.DOTS.Demos.Input
{
    [BurstCompile]
    [UpdateInGroup(typeof(PauseableSystemGroup))]
    public partial struct PlayerCullingSystem : ISystem
    {
        
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<BeginPresentationEntityCommandBufferSystem.Singleton>();
            state.RequireForUpdate<CullingSystemAuthoring.CullingSystemIsEnabledTag>();
            state.RequireForUpdate<CullingComponent>();
            //
            state.RequireForUpdate<PlayerTag>();
            state.RequireForUpdate<PlayerCullingSystemAuthoring.PlayerCullingSystemIsEnabledTag>();
        }
        
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var ecb = SystemAPI.
                GetSingleton<BeginPresentationEntityCommandBufferSystem.Singleton>().
                CreateCommandBuffer(state.WorldUnmanaged);
            
            foreach (var (cullingComponent, entity) in 
                     SystemAPI.Query<RefRO<CullingComponent>>().
                         WithAll<PlayerTag>().
                         WithEntityAccess())
            {
                
                if (cullingComponent.ValueRO.IsOffscreen.HasValue &&
                    cullingComponent.ValueRO.IsOffscreen.Value)
                {
                    Debug.Log(string.Format("Entity ({0}), IsOffscreen = {1}, so DestroyEntity.",
                        entity.Index,
                        cullingComponent.ValueRO.IsOffscreen.Value
                    ));
                    ecb.DestroyEntity(entity);
                }
            }
        }
    }
}