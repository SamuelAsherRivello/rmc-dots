using Unity.Entities;
using UnityEngine;

namespace RMC.DOTS.Samples.Templates.DOTSGameTemplate
{
    public class MoveSystemAuthoring : MonoBehaviour
    {
        [SerializeField] 
        public bool IsSystemEnabled = true;
        
        public struct MoveSystemIsEnabledTag : IComponentData {}
        
        public class MoveSystemAuthoringBaker : Baker<MoveSystemAuthoring>
        {
            public override void Bake(MoveSystemAuthoring authoring)
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