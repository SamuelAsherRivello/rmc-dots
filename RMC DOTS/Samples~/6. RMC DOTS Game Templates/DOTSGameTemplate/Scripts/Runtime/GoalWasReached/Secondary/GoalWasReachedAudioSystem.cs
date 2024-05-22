using RMC.Audio.Data.Types;
using RMC.DOTS.SystemGroups;
using RMC.DOTS.Systems.Audio;
using RMC.DOTS.Systems.Player;
using Unity.Entities;
using UnityEngine;

namespace RMC.DOTS.Samples.Templates.DOTSGameTemplate
{
    
    [UpdateInGroup(typeof(UnpauseablePresentationSystemGroup))]
    [UpdateBefore(typeof(GoalWasReachedCleanupSystem))]
    public partial struct GoalWasReachedAudioSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<GoalWasReachedSystemAuthoring.GoalWasReachedSystemIsEnabledTag>();
            state.RequireForUpdate<BeginPresentationEntityCommandBufferSystem.Singleton>();
        }

        public void OnUpdate(ref SystemState state)
        {
            var ecb = SystemAPI.
                GetSingleton<BeginPresentationEntityCommandBufferSystem.Singleton>().
                CreateCommandBuffer(state.WorldUnmanaged);
            
            foreach (var (playerTag, entity) 
                     in SystemAPI.Query<PlayerTag>().
                         WithAll<GoalWasReachedExecuteOnceTag>().
                         WithEntityAccess())
            {
                Entity audioEntity = ecb.CreateEntity();
                ecb.AddComponent<AudioComponent>(audioEntity,
                    new AudioComponent
                    (
                        "Pickup01",
                        AudioConstants.VolumeDefault,
                        AudioConstants.PitchDefault,
                        0.25f
                    ));
            }
        }
    }
}