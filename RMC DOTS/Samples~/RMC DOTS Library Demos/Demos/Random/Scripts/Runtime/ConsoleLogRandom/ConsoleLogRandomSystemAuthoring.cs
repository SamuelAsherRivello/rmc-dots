using System;
using Unity.Entities;
using UnityEngine;

namespace RMC.DOTS.Demos.Random.ConsoleLogRandom
{
    public class ConsoleLogRandomSystemAuthoring : MonoBehaviour
    {
        [SerializeField] 
        public bool IsSystemEnabled = true;
        
        public struct ConsoleLogRandomSystemIsEnabledTag : IComponentData {}
        
        public class ConsoleLogRandomSystemAuthoringBaker : Baker<ConsoleLogRandomSystemAuthoring>
        {
            public override void Bake(ConsoleLogRandomSystemAuthoring authoring)
            {
                if (authoring.IsSystemEnabled)
                {
                    Entity entity = GetEntity(TransformUsageFlags.Dynamic);
                    AddComponent<ConsoleLogRandomSystemIsEnabledTag>(entity);
                }
            }
        }
    }
}
