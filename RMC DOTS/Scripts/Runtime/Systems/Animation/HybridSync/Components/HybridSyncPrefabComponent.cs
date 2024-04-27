using Unity.Entities;
using UnityEngine;

namespace RMC.DOTS.Demos.HybridSync
{
    public class HybridSyncPrefabComponent : IComponentData
    {
        public GameObject Prefab;
        public Transform Transform;
    }
}