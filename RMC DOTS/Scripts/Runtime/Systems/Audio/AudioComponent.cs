using RMC.Audio.Data.Types;
using Unity.Collections;
using Unity.Entities;

namespace RMC.DOTS.Systems.Audio
{
    /// <summary>
    /// </summary>
    public struct AudioComponent : IComponentData
    {
        public FixedString128Bytes AudioClipName;
        public float Volume;
        public float Pitch;
        public float DelayInSeconds;
        public bool IsLooping;
        
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

    }
}