using Unity.Entities;
using UnityEngine;

namespace RMC.DOTS.Systems.Spawner
{
    /// <summary>
    /// Place this MonoBehaviour on a GameObject in the Scene
    /// To enable the <see cref="SpawnerSystem"/>
    /// </summary>

    public class SpawnerSystemAuthoring : MonoBehaviour
    {
        public GameObject Prefab;
        public bool IsSystemEnabled = true;
        
        public struct SpawnerSystemIsEnabledTag : IComponentData {}
        
        public class SpawnerSystemAuthoringBaker : Baker<SpawnerSystemAuthoring>
        {
            public override void Bake(SpawnerSystemAuthoring systemAuthoring)
            {
                if (systemAuthoring.IsSystemEnabled)
                {
                    var entity = GetEntity(TransformUsageFlags.None);
                    AddComponent<SpawnerSystemIsEnabledTag>(entity);
                    AddComponent(entity, new SpawnerConfigurationComponent
                    {
                        Prefab = GetEntity(systemAuthoring.Prefab, TransformUsageFlags.Dynamic),
                    });
            
                }
            }
        }
    }


}
