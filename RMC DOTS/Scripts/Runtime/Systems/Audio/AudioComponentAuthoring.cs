using Unity.Entities;
using UnityEngine;

namespace RMC.DOTS.Systems.Audio
{
    public class AudioComponentAuthoring : MonoBehaviour
    {
        public string AudioClipName;

        public class AudioComponentAuthoringBaker : Baker<AudioComponentAuthoring>
        {
            public override void Bake(AudioComponentAuthoring authoring)
            {
                Entity entity = GetEntity(TransformUsageFlags.Dynamic);

                AddComponent<AudioComponent>(entity,
                    new AudioComponent
                    {
                        AudioClipName = authoring.AudioClipName
                    });

            }
        }
    }
}
