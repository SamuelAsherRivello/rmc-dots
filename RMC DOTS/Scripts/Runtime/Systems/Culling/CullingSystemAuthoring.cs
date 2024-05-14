using Unity.Entities;
using UnityEngine;

namespace RMC.DOTS.Systems.Culling
{
    /// <summary>
    /// Place this MonoBehaviour on a GameObject in the Scene
    /// To enable the <see cref="CullingSystem"/>
    /// </summary>
    public class CullingSystemAuthoring : MonoBehaviour
    {
        [SerializeField] 
        public bool IsSystemEnabled = true;

        [Tooltip("1.0 = use actual screen bounds, < 1 means use smaller, > 1 means use bigger.")]
        [SerializeField] 
        public float ScreenSizeMultiplier = 1;
        
        public struct CullingSystemIsEnabledTag : IComponentData {}

        public struct CullingSystemConfigurationComponent : IComponentData
        {
            public readonly float ScreenSizeMultiplier;

            public CullingSystemConfigurationComponent(float screenSizeMultiplier = 1)
            {
                ScreenSizeMultiplier = screenSizeMultiplier;
            }
        }

        public class AudioSystemAuthoringBaker : Baker<CullingSystemAuthoring>
        {
            public override void Bake(CullingSystemAuthoring authoring)
            {
                Entity entity = GetEntity(TransformUsageFlags.Dynamic);
                
                if (authoring.IsSystemEnabled)
                {
                    //Turn system on
                    AddComponent<CullingSystemIsEnabledTag>(entity);
                    AddComponent<CullingSystemConfigurationComponent>(entity, new CullingSystemConfigurationComponent
                        (
                            authoring.ScreenSizeMultiplier
                        ));
                }
            }
        }
    }
}
