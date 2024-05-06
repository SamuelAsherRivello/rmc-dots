using RMC.DOTS.SystemGroups;
using RMC.DOTS.Systems.PhysicsTrigger;
using RMC.DOTS.Systems.Player;
using Unity.Burst;
using Unity.Entities;
using UnityEngine;

namespace RMC.DOTS.Samples.Templates.DOTSGameTemplate
{
    [UpdateInGroup(typeof(UnpauseableSystemGroup))]
    public partial struct GoalWasReachedSystem : ISystem
    {
        public int TimeFrameCountForLastCollision;
        
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<GoalWasReachedSystemAuthoring.GoalWasReachedSystemIsEnabledTag>();
            state.RequireForUpdate<BeginPresentationEntityCommandBufferSystem.Singleton>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var ecb = SystemAPI.
                GetSingleton<BeginPresentationEntityCommandBufferSystem.Singleton>().
                CreateCommandBuffer(state.WorldUnmanaged);

            //Remove any existing tags
            foreach (var (playerTag, goalWasReachedTag, entity) in SystemAPI.Query<PlayerTag, GoalWasReachedTag>().WithEntityAccess())
            {
                //Debug.Log($"GamePickup ({entity.Index}) Set To REMOVE on TimeFrameCount: {Time.frameCount}");
                ecb.RemoveComponent<GoalWasReachedTag>(entity);
            }
            foreach (var (playerTag, physicsTriggerOutputTag, entity) in SystemAPI.Query<PlayerTag, PhysicsTriggerOutputComponent>().WithEntityAccess())
            {
                if (physicsTriggerOutputTag.PhysicsTriggerType == PhysicsTriggerType.Enter &&
                    physicsTriggerOutputTag.TimeFrameCountForLastCollision != TimeFrameCountForLastCollision)
                { 
                    //Debug.Log($"GamePickup ({entity.Index}) Set To Enter on TimeFrameCount: {Time.frameCount}");
                    
                    //TODO: Ideally, I don't have to check this to prevent the sometimes-repeated calls, but this works great with workaround
                    TimeFrameCountForLastCollision = physicsTriggerOutputTag.TimeFrameCountForLastCollision;
                    ecb.AddComponent<GoalWasReachedTag>(entity);
                }
            }
        }
    }
}