using Unity.Entities;
using UnityEngine;

namespace RMC.DOTS.Systems.Audio
{
    public class AudioComponentAuthoring : MonoBehaviour
    {
        public string AudioClipName;
        public float Volume = 1;
        public float Pitch = 1;
        public float DelayInSeconds = 0;
        public bool IsLooping = false;

        
        public class AudioComponentAuthoringBaker : Baker<AudioComponentAuthoring>
        {
            public override void Bake(AudioComponentAuthoring authoring)
            {
                Entity entity = GetEntity(TransformUsageFlags.Dynamic);

                AddComponent<AudioComponent>(entity,
                    new AudioComponent
                    (
                        authoring.AudioClipName,
                        authoring.Volume,
                        authoring.Pitch,
                        authoring.DelayInSeconds,
                        authoring.IsLooping
                    ));

            }
        }
    }
}
