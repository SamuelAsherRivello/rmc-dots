using Unity.Entities;
using UnityEngine;

namespace RMC.DOTS.Systems.Tween
{
    public class TweenScaleSystemAuthoring : MonoBehaviour
    {
        [SerializeField] 
        public bool IsSystemEnabled = true;
        
        public struct TweenScaleSystemIsEnabledTag : IComponentData {}
        
        public class TweenScaleSystemAuthoringBaker : Baker<TweenScaleSystemAuthoring>
        {
            public override void Bake(TweenScaleSystemAuthoring authoring)
            {
                if (authoring.IsSystemEnabled)
                {
                    Entity entity = GetEntity(TransformUsageFlags.Dynamic);
                    AddComponent<TweenScaleSystemIsEnabledTag>(entity);
                }
            }
        }
    }
}