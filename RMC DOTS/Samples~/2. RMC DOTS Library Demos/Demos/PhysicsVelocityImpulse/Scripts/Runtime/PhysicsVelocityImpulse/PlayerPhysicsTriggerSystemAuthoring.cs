using Unity.Entities;
using UnityEngine;

namespace RMC.DOTS.Demos.Input
{
    public class PlayerPhysicsTriggerSystemAuthoring : MonoBehaviour
    {
        [SerializeField] 
        public bool IsSystemEnabled = true;
        
        public struct PlayerPhysicsTriggerSystemIsEnabledTag : IComponentData {}
        
        public class PlayerPhysicsTriggerSystemAuthoringBaker : Baker<PlayerPhysicsTriggerSystemAuthoring>
        {
            public override void Bake(PlayerPhysicsTriggerSystemAuthoring authoring)
            {
                if (authoring.IsSystemEnabled)
                {
                    Entity inputEntity = GetEntity(TransformUsageFlags.Dynamic);
                    AddComponent<PlayerPhysicsTriggerSystemIsEnabledTag>(inputEntity);
                }
            }
        }
    }
}
