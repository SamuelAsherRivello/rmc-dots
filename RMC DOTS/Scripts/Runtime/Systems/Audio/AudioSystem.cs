using System;
using RMC.Audio;
using RMC.DOTS.SystemGroups;
using RMC.DOTS.Systems.FrameCount;
using Unity.Entities;
using UnityEngine;

namespace RMC.DOTS.Systems.Audio
{
   
    /// <summary>
    /// This system plays the pickup sound effect when a pickup has been picked up
    /// </summary>
    [UpdateInGroup(typeof(UnpauseableSystemGroup), OrderFirst = true)]
    public partial class AudioSystem : SystemBase
    {
        // Query all sound request components
        private EntityQuery _entityQuery;

        // Create buffer to request deletion of component after playing sound
        private BeginPresentationEntityCommandBufferSystem _commandBufferSystem;

        protected override void OnCreate()
        {
            _commandBufferSystem = World.GetOrCreateSystemManaged<BeginPresentationEntityCommandBufferSystem>();
            RequireForUpdate<AudioComponent>();
            RequireForUpdate<AudioSystemAuthoring.AudioSystemConfigurationComponent>();
            RequireForUpdate<AudioSystemAuthoring.AudioSystemIsEnabledTag>();
        }

        protected override void OnUpdate()
        {
            var audioSystemConfigurationComponent = SystemAPI.GetSingleton<AudioSystemAuthoring.AudioSystemConfigurationComponent>();
            var ecb = _commandBufferSystem.CreateCommandBuffer();
            var deltaTime = SystemAPI.Time.DeltaTime;
            
            if (audioSystemConfigurationComponent.IsDebug)
            {
                Debug.Log($"Update");
            }
     
            
            // Iterate over the entities
            Entities
                .ForEach((Entity entity, ref AudioComponent audioComponent) =>
                {
                    if (audioSystemConfigurationComponent.IsDebug)
                    {
                        Debug.Log($"Playing audio: {audioComponent.AudioClipName.Value}");
                    }
                    try
                    {
                        
                        AudioManagerPlayParameters audioManagerPlayParameters = new AudioManagerPlayParameters
                        (
                            audioComponent.Volume,
                            audioComponent.Pitch,
                            audioComponent.DelayInSeconds,
                            audioComponent.IsLooping
                            );
                        AudioManager.Instance.PlayAudioClip(audioComponent.AudioClipName.Value, audioManagerPlayParameters);
                    }
                    catch (Exception e)
                    {
                        Debug.LogError("AudioManager Exception: " + e.Message);
                    }
                
                    ecb.RemoveComponent<AudioComponent>(entity);
                    
                }).WithoutBurst().Run();
        }
    }
}