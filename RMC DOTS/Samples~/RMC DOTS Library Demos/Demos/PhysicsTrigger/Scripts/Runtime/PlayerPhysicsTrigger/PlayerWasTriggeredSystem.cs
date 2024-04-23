using RMC.DOTS.Demos.Input;
using RMC.DOTS.SystemGroups;
using RMC.DOTS.Systems.PhysicsTrigger;
using RMC.DOTS.Systems.Player;
using Unity.Burst;
using Unity.Entities;
using UnityEngine;

namespace RMC.DOTS.Demos.PhysicsTrigger
{
    [UpdateInGroup(typeof(UnpauseableSystemGroup))]
    public partial struct PlayerWasTriggeredSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<PlayerPhysicsTriggerSystemAuthoring.PlayerPhysicsTriggerSystemIsEnabledTag>();
            state.RequireForUpdate<BeginPresentationEntityCommandBufferSystem.Singleton>();
        }

        
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var ecb = SystemAPI.
                GetSingleton<BeginPresentationEntityCommandBufferSystem.Singleton>().
                CreateCommandBuffer(state.WorldUnmanaged);
            
            //Remove any existing tags
            foreach (var (playerTag, goalWasReachedTag, entity) in SystemAPI.Query<PlayerTag, PlayerWasTriggeredTag>().WithEntityAccess())
            {
                //Debug.Log($"GamePickup ({entity.Index}) Set To REMOVE on TimeFrameCount: {Time.frameCount}");
                ecb.RemoveComponent<PlayerWasTriggeredTag>(entity);
            }
            
            foreach (var (playerTag, physicsTriggerOutputTag, entity) in SystemAPI.Query<PlayerTag, PhysicsTriggerOutputComponent>().WithEntityAccess())
            {
                if (physicsTriggerOutputTag.PhysicsTriggerType == PhysicsTriggerType.Enter &&
                    physicsTriggerOutputTag.TimeFrameCountForLastCollision <= Time.frameCount - PhysicsTriggerOutputComponent.FramesToWait)
                { 
                    //Debug.Log($"GamePickup ({entity.Index}) Set To Enter on TimeFrameCount: {Time.frameCount}");
                    ecb.AddComponent<PlayerWasTriggeredTag>(entity);
                }
            }
        }
    }
}