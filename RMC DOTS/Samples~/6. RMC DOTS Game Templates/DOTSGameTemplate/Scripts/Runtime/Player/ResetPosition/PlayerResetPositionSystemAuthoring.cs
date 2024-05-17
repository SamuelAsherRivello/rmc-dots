using Unity.Entities;
using UnityEngine;

namespace RMC.DOTS.Samples.Templates.DOTSGameTemplate
{
    public class PlayerResetPositionSystemAuthoring : MonoBehaviour
    {
        [SerializeField] 
        public bool IsSystemEnabled = true;
        
        public struct PlayerResetPositionSystemIsEnabledTag : IComponentData {}
        
        public class PlayerResetPositionSystemAuthoringBaker : Baker<PlayerResetPositionSystemAuthoring>
        {
            public override void Bake(PlayerResetPositionSystemAuthoring authoring)
            {
                if (authoring.IsSystemEnabled)
                {
                    Entity inputEntity = GetEntity(TransformUsageFlags.Dynamic);
                    AddComponent<PlayerResetPositionSystemIsEnabledTag>(inputEntity);
                }
            }
        }
    }
}