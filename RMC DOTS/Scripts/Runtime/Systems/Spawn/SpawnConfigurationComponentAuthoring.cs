using Unity.Entities;
using UnityEngine;

namespace RMC.DOTS.Systems.Spawn
{
    public class SpawnConfigurationComponentAuthoring : MonoBehaviour
    {
        [Header("Prefab")]
        public GameObject SpawnPrefab;

        public class SpawnConfigurationComponentAuthoringBaker : Baker<SpawnConfigurationComponentAuthoring>
        {
            public override void Bake(SpawnConfigurationComponentAuthoring authoring)
            {
                Entity entity = GetEntity(TransformUsageFlags.Dynamic);

                AddComponent(entity, new SpawnConfigurationComponent
                {
                    SpawnPrefab = GetEntity(authoring.SpawnPrefab, TransformUsageFlags.Dynamic),
                });
            }
        }
    }
}
