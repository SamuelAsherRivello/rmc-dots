using Unity.Entities;
using UnityEngine;

namespace RMC.DOTS.Systems.SpawnGrid
{
    /// <summary>
    /// Place this MonoBehaviour on a GameObject in the Scene
    /// To enable the <see cref="SpawnGridSystem"/>
    /// </summary>

    public class SpawnGridSystemAuthoring : MonoBehaviour
    {
        public bool IsSystemEnabled = true;
        
        public struct SpawnGridSystemIsEnabledTag : IComponentData {}
        
        public class SpawnGridSystemAuthoringBaker : Baker<SpawnGridSystemAuthoring>
        {
            public override void Bake(SpawnGridSystemAuthoring systemAuthoring)
            {
                if (systemAuthoring.IsSystemEnabled)
                {
                    var entity = GetEntity(TransformUsageFlags.None);
                    AddComponent<SpawnGridSystemIsEnabledTag>(entity);
                }
            }
        }
    }


}
