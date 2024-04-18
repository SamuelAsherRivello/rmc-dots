using RMC.Core.Audio;
using RMC.DOTS.Systems.GameState;
using Unity.Entities;
using UnityEngine;

namespace RMC.DOTS.Systems.Audio
{
    /// <summary>
    /// This system plays the pickup sound effect when a pickup has been picked up
    /// </summary>
    [UpdateBefore(typeof(GameStateSystem))]
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    public partial class AudioSystem : SystemBase
    {
        // Query all sound request components
        private EntityQuery _entityQuery;

        // Create buffer to request deletion of component after playing sound
        private BeginPresentationEntityCommandBufferSystem _commandBufferSystem;

        protected override void OnCreate()
        {
            _commandBufferSystem = this.World.GetOrCreateSystemManaged<BeginPresentationEntityCommandBufferSystem>();
            RequireForUpdate<AudioComponent>();
            RequireForUpdate<AudioSystemAuthoring.AudioSystemConfigurationComponent>();
            RequireForUpdate<AudioSystemAuthoring.AudioSystemIsEnabledTag>();
        }

        protected override void OnUpdate()
        {
            var ecb = _commandBufferSystem.CreateCommandBuffer();
            var audioSystemConfigurationComponent = SystemAPI.GetSingleton<AudioSystemAuthoring.AudioSystemConfigurationComponent>();
           
            // Iterate over the entities
            Entities
                .ForEach((Entity entity, ref AudioComponent audioComponent) =>
                {
                    if (audioSystemConfigurationComponent.IsDebug)
                    {
                        Debug.Log($"Playing audio: {audioComponent.AudioClipName.Value}");
                    }
                    AudioManager.Instance.PlayAudioClip(audioComponent.AudioClipName.Value);
                    ecb.RemoveComponent<AudioComponent>(entity);
                    
                }).WithoutBurst().Run();
        }
    }
}