using RMC.DOTS.SystemGroups;
using RMC.DOTS.Systems.PhysicsTrigger;
using Unity.Burst;
using Unity.Entities;
using UnityEngine;

namespace RMC.DOTS.Samples.Templates.DOTSGameTemplate
{
    [UpdateInGroup(typeof(UnpauseableSystemGroup))]
    public partial struct GoalWasReachedSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<BeginPresentationEntityCommandBufferSystem.Singleton>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var ecb = SystemAPI.
                GetSingleton<BeginPresentationEntityCommandBufferSystem.Singleton>().
                CreateCommandBuffer(state.WorldUnmanaged);
            
            
            int timeFrameCount = UnityEngine.Time.frameCount;
            int framesToWait = 2; //TODO: why not lower it to '0'? I guess this sysetm runs one frame after the last one?
            
            //Remove any existing tags
            foreach (var (playerTag, goalWasReachedTag, entity) in SystemAPI.Query<PlayerTag, GoalWasReachedTag>().WithEntityAccess())
            {
                Debug.Log($"GamePickup ({entity.Index}) Set To REMOVE on TimeFrameCount: {Time.frameCount}");
                ecb.RemoveComponent<GoalWasReachedTag>(entity);
            }
            
            foreach (var (playerTag, physicsTriggerOutputTag, entity) in SystemAPI.Query<PlayerTag, PhysicsTriggerOutputComponent>().WithEntityAccess())
            {
                if (physicsTriggerOutputTag.PhysicsTriggerType == PhysicsTriggerType.Enter)
                { 
                    Debug.Log($"GamePickup ({entity.Index}) Set To Enter on TimeFrameCount: {Time.frameCount}");
                    ecb.AddComponent<GoalWasReachedTag>(entity);
                }
            }
        }
    }
}