using Unity.Entities;
using UnityEngine;

namespace RMC.DOTS.Samples.Templates.DOTSGameTemplate
{
    public class GoalWasReachedSystemAuthoring : MonoBehaviour
    {
        [SerializeField] 
        public bool IsSystemEnabled = true;
        
        public struct GoalWasReachedSystemIsEnabledTag : IComponentData {}
        
        public class GoalWasReachedSystemAuthoringBaker : Baker<GoalWasReachedSystemAuthoring>
        {
            public override void Bake(GoalWasReachedSystemAuthoring componentAuthoring)
            {
                if (componentAuthoring.IsSystemEnabled)
                {
                    Entity entity = GetEntity(TransformUsageFlags.Dynamic);
                    AddComponent<GoalWasReachedSystemIsEnabledTag>(entity);
                }
            }
        }
    }
}