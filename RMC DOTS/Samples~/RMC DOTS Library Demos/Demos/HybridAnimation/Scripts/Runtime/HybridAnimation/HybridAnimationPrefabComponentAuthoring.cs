using Unity.Entities;
using UnityEngine;

namespace RMC.DOTS.Demos.HybridAnimation
{
    public class HybridAnimationPrefabComponentAuthoring : MonoBehaviour
    {
        public GameObject Prefab;

        public class HybridAnimationPrefabComponentAuthoringBaker : Baker<HybridAnimationPrefabComponentAuthoring>
        {
            public override void Bake(HybridAnimationPrefabComponentAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponentObject(entity, new HybridAnimationPrefabComponent { Value = authoring.Prefab });
            }
        }
    }
}