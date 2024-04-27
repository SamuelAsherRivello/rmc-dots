using Unity.Entities;
using UnityEngine;

namespace RMC.DOTS.Demos.HybridSync
{
    public class HybridSyncSystemAuthoring : MonoBehaviour
    {
        [SerializeField] 
        public bool IsSystemEnabled = true;
        
        public struct HybridSyncSystemIsEnabledTag : IComponentData {}
        
        public class HybridSyncSystemAuthoringBaker : Baker<HybridSyncSystemAuthoring>
        {
            public override void Bake(HybridSyncSystemAuthoring authoring)
            {
                if (authoring.IsSystemEnabled)
                {
                    Entity inputEntity = GetEntity(TransformUsageFlags.Dynamic);
                    AddComponent<HybridSyncSystemIsEnabledTag>(inputEntity);
                }
            }
        }
    }
}
