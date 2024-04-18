using Unity.Collections;
using Unity.Entities;
namespace RMC.DOTS.Systems.Audio
{
    /// <summary>
    /// </summary>
    public struct AudioComponent : IComponentData
    {
        public FixedString128Bytes  AudioClipName;
    }
}