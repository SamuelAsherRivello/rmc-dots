using Unity.Entities;
using UnityEngine;

namespace RMC.DOTS.Demos.Input
{
    public class PlayerMoveSystemAuthoring : MonoBehaviour
    {
        [SerializeField] 
        public bool IsSystemEnabled = true;
        
        public struct PlayerMoveSystemIsEnabledTag : IComponentData {}
        
        public class PlayerMoveSystemAuthoringBaker : Baker<PlayerMoveSystemAuthoring>
        {
            public override void Bake(PlayerMoveSystemAuthoring authoring)
            {
                if (authoring.IsSystemEnabled)
                {
                    Entity inputEntity = GetEntity(TransformUsageFlags.Dynamic);
                    AddComponent<PlayerMoveSystemIsEnabledTag>(inputEntity);
                }
            }
        }
    }
}
