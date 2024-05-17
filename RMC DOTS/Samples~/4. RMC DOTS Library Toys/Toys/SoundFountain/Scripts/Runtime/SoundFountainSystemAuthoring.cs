using Unity.Entities;
using UnityEngine;

namespace RMC.DOTS.Toys.Fountain
{
    public class SoundFountainSystemAuthoring : MonoBehaviour
    {
        [SerializeField] 
        public bool IsSystemEnabled = true;
        
        public struct SoundFountainSystemIsEnabledTag : IComponentData {}
        
        public class SoundFountainSystemAuthoringBaker : Baker<SoundFountainSystemAuthoring>
        {
            public override void Bake(SoundFountainSystemAuthoring authoring)
            {
                if (authoring.IsSystemEnabled)
                {
                    Entity inputEntity = GetEntity(TransformUsageFlags.Dynamic);
                    AddComponent<SoundFountainSystemIsEnabledTag>(inputEntity);
                }
            }
        }
    }
}