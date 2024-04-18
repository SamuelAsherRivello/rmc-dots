using Unity.Entities;
using UnityEngine;

namespace RMC.DOTS.Systems.Audio
{
    /// <summary>
    /// Place this MonoBehaviour on a GameObject in the Scene
    /// To enable the <see cref="AudioSystem"/>
    /// </summary>
    public class AudioSystemAuthoring : MonoBehaviour
    {
        [SerializeField] 
        public bool IsSystemEnabled = true;
        
        [SerializeField] 
        public bool IsDebug = false;

        
        public struct AudioSystemIsEnabledTag : IComponentData {}

        public struct AudioSystemConfigurationComponent : IComponentData
        {
            public bool IsDebug;
        }
        
        public class AudioSystemAuthoringBaker : Baker<AudioSystemAuthoring>
        {
            public override void Bake(AudioSystemAuthoring systemAuthoring)
            {
                Entity entity = GetEntity(TransformUsageFlags.Dynamic);
                
                AddComponent<AudioSystemConfigurationComponent>(entity,
                    new AudioSystemConfigurationComponent { IsDebug = systemAuthoring.IsDebug });
                
                if (systemAuthoring.IsSystemEnabled)
                {
                    //Turn system on
                    AddComponent<AudioSystemIsEnabledTag>(entity);
                }
            }
        }
    }
}
