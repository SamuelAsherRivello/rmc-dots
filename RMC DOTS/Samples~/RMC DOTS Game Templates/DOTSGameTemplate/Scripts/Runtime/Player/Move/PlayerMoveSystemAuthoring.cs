using Unity.Entities;
using UnityEngine;

namespace RMC.DOTS.Samples.Templates.DOTSGameTemplate
{
    public class PlayerMoveSystemAuthoring : MonoBehaviour
    {
        [SerializeField] 
        public bool IsSystemEnabled = true;
        
        public struct MoveSystemIsEnabledTag : IComponentData {}
        
        public class PlayerMoveSystemAuthoringBaker : Baker<PlayerMoveSystemAuthoring>
        {
            public override void Bake(PlayerMoveSystemAuthoring authoring)
            {
                if (authoring.IsSystemEnabled)
                {
                    Entity inputEntity = GetEntity(TransformUsageFlags.Dynamic);
                    AddComponent<MoveSystemIsEnabledTag>(inputEntity);
                }
            }
        }
    }
}