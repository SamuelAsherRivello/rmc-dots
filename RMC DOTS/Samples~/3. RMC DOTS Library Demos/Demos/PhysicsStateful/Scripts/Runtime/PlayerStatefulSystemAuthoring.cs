using Unity.Entities;
using UnityEngine;

namespace Unity.Physics.PhysicsStateful
{
    public class PlayerStatefulSystemAuthoring : MonoBehaviour
    {
        [SerializeField] 
        public bool IsEnabled = true;
        
        public struct PlayerStatefulSystemAuthoringIsEnabledTag : IComponentData {}

        public class PlayerStatefulSystemAuthoringBaker : Baker<PlayerStatefulSystemAuthoring>
        {
            public override void Bake(PlayerStatefulSystemAuthoring authoring)
            {
                if (authoring.IsEnabled)
                {
                    Entity inputEntity = GetEntity(TransformUsageFlags.Dynamic);
                    AddComponent<PlayerStatefulSystemAuthoringIsEnabledTag>(inputEntity);
                }
            }
        }
    }
}
