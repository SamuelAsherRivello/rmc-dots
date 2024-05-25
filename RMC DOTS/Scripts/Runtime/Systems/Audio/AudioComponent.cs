using RMC.Audio;
using RMC.Audio.Data.Types;
using Unity.Collections;
using Unity.Entities;

namespace RMC.DOTS.Systems.Audio
{
    public struct AudioComponent : IComponentData
    {
        public FixedString128Bytes AudioClipName;
        public readonly float Volume;
        public readonly float Pitch;
        public readonly float DelayInSeconds;
        public readonly bool IsLooping;
        
        public AudioComponent 
        (
            string audioClipName, 
            float volume = AudioConstants.VolumeDefault, 
            float pitch = AudioConstants.PitchDefault, 
            float delayInSeconds = AudioConstants.DelayInSecondsDefault, 
            bool isLooping = AudioConstants.IsLoopingDefault)
        {
            AudioClipName = new FixedString128Bytes(audioClipName);
            Volume = volume;
            Pitch = pitch;
            DelayInSeconds = delayInSeconds;
            IsLooping = isLooping;
        }
        
        
        public AudioManagerPlayParameters CreateAudioManagerPlayParameters()
        {
            return new AudioManagerPlayParameters
            (
                Volume,
                Pitch,
                DelayInSeconds,
                IsLooping
            );
        }
    }
}