using System;
using Unity.Entities;
using UnityEngine;

namespace RMC.DOTS.Systems.Random
{
    public class RandomComponentAuthoring : MonoBehaviour
    {
        [SerializeField] 
        public bool IsSystemEnabled = true;
        
        [Tooltip("Set to false for deterministic results.")]
        [SerializeField] 
        public bool IsRandomSeed = true;
        
        public struct RandomSystemIsEnabledTag : IComponentData {}
        
        public class RandomComponentAuthoringBaker : Baker<RandomComponentAuthoring>
        {
            public override void Bake(RandomComponentAuthoring authoring)
            {
                if (authoring.IsSystemEnabled)
                {
                    Entity entity = GetEntity(TransformUsageFlags.Dynamic);

                    uint seed = 0;
                    if (authoring.IsRandomSeed)
                    {
                        seed = (uint)UnityEngine.Random.Range(0, Int32.MaxValue);
                    }
                    AddComponent<RandomSystemIsEnabledTag>(entity);
                    AddComponent<RandomComponent>(entity,
                        new RandomComponent
                        {
                            Random = Unity.Mathematics.Random.CreateFromIndex(seed)
                        });
                }
            }
        }
    }
}
