using Unity.Entities;
using UnityEngine;

namespace RMC.DOTS.Demos.HybridSync
{
    public class HybridSyncPrefabComponentAuthoring : MonoBehaviour
    {
        public GameObject Prefab;
        public Transform InitialTransform;
        
        public class HybridSyncPrefabComponentBaker : Baker<HybridSyncPrefabComponentAuthoring>
        {
            public override void Bake(HybridSyncPrefabComponentAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponentObject(entity, 
                    new HybridSyncPrefabComponent
                    {
                        Prefab = authoring.Prefab,
                        Transform = authoring.InitialTransform
                    });
            }
        }
    }
}