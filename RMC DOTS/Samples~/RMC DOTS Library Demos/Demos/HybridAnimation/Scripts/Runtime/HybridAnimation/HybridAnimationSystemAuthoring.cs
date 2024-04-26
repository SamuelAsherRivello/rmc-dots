using Unity.Entities;
using UnityEngine;

namespace RMC.DOTS.Demos.HybridAnimation
{
    public class HybridAnimationSystemAuthoring : MonoBehaviour
    {
        [SerializeField] 
        public bool IsSystemEnabled = true;
        
        public struct HybridAnimationSystemIsEnabledTag : IComponentData {}
        
        public class HybridAnimationSystemBaker : Baker<HybridAnimationSystemAuthoring>
        {
            public override void Bake(HybridAnimationSystemAuthoring authoring)
            {
                if (authoring.IsSystemEnabled)
                {
                    Entity inputEntity = GetEntity(TransformUsageFlags.Dynamic);
                    AddComponent<HybridAnimationSystemIsEnabledTag>(inputEntity);
                }
            }
        }
    }
}
