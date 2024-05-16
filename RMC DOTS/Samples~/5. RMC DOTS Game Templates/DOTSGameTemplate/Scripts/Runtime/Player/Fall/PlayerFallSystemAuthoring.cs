using Unity.Entities;
using UnityEngine;

namespace RMC.DOTS.Samples.Templates.DOTSGameTemplate
{
    public class PlayerFallSystemAuthoring : MonoBehaviour
    {
        [SerializeField] 
        public bool IsSystemEnabled = true;
        
        public struct PlayerFallSystemIsEnabledTag : IComponentData {}
        
        public class PlayerFallSystemAuthoringBaker : Baker<PlayerFallSystemAuthoring>
        {
            public override void Bake(PlayerFallSystemAuthoring authoring)
            {
                if (authoring.IsSystemEnabled)
                {
                    Entity inputEntity = GetEntity(TransformUsageFlags.Dynamic);
                    AddComponent<PlayerFallSystemIsEnabledTag>(inputEntity);
                }
            }
        }
    }
}