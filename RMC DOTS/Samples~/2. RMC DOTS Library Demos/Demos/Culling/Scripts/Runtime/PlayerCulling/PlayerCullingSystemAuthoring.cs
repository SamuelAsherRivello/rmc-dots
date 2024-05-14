using Unity.Entities;
using UnityEngine;

namespace RMC.DOTS.Demos.Input
{
    public class PlayerCullingSystemAuthoring : MonoBehaviour
    {
        [SerializeField] 
        public bool IsSystemEnabled = true;
        
        public struct PlayerCullingSystemIsEnabledTag : IComponentData {}
        
        public class PlayerCullingSystemAuthoringBaker : Baker<PlayerCullingSystemAuthoring>
        {
            public override void Bake(PlayerCullingSystemAuthoring authoring)
            {
                if (authoring.IsSystemEnabled)
                {
                    Entity inputEntity = GetEntity(TransformUsageFlags.Dynamic);
                    AddComponent<PlayerCullingSystemIsEnabledTag>(inputEntity);
                }
            }
        }
    }
}
