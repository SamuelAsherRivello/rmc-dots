using RMC.DOTS.SystemGroups;
using RMC.DOTS.Systems.Audio;
using RMC.DOTS.Systems.Player;
using Unity.Burst;
using Unity.Entities;

namespace RMC.DOTS.Samples.Templates.DOTSGameTemplate
{
    [UpdateInGroup(typeof(UnpauseableSystemGroup))]
    [RequireMatchingQueriesForUpdate]
    public partial struct GoalWasReachedAudioSystem : ISystem
    {
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
            
            foreach (var (playerTag, goalWasReachedTag, entity) in SystemAPI.Query<PlayerTag, GoalWasReachedTag>().WithEntityAccess())
            {
                //Debug.Log("Play this sound: " + entity.Index + " fc: " + Time.frameCount);
                Entity audioEntity = ecb.CreateEntity();
                ecb.AddComponent<AudioComponent>(audioEntity, 
                    new AudioComponent
                    {
                        AudioClipName = "Pickup01",
                        TimeTillPlayInSeconds = 0.25f
                    });
            }
        }
    }
}