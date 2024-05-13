using Unity.Entities;
using UnityEngine;

namespace RMC.DOTS.Systems.Spawn
{
    /// <summary>
    /// Place this MonoBehaviour on a GameObject in the Scene
    /// To enable the <see cref="SpawnSystem"/>
    /// </summary>

    public class SpawnSystemAuthoring : MonoBehaviour
    {
        public bool IsSystemEnabled = true;
        
        public struct SpawnerSystemIsEnabledTag : IComponentData {}
        
        public class SpawnerSystemAuthoringBaker : Baker<SpawnSystemAuthoring>
        {
            public override void Bake(SpawnSystemAuthoring authoring)
            {
                if (authoring.IsSystemEnabled)
                {
                    var entity = GetEntity(TransformUsageFlags.None);
                    AddComponent<SpawnerSystemIsEnabledTag>(entity);
        
                }
            }
        }
    }


}
