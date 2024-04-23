using System;
using Unity.Entities;
using UnityEngine;

namespace RMC.DOTS.Systems.Random
{
    
    /// <summary>
    /// NOTE: This does not have a SYSTEM per se, but I like the naming this way.
    /// It has instead, just a component and tag that UNRELATED systems check for
    /// </summary>
    public class RandomSystemAuthoring : MonoBehaviour
    {
        [SerializeField] 
        public bool IsSystemEnabled = true;
        
        [Tooltip("Set to false for deterministic results.")]
        [SerializeField] 
        public bool IsRandomSeed = true;
        
        public struct RandomSystemIsEnabledTag : IComponentData {}
        
        public class RandomComponentAuthoringBaker : Baker<RandomSystemAuthoring>
        {
            public override void Bake(RandomSystemAuthoring authoring)
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
