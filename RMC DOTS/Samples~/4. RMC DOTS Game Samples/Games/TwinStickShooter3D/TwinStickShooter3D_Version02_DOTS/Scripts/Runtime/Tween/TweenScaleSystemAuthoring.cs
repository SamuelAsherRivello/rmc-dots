using Unity.Entities;
using UnityEngine;

namespace RMC.DOTS.Samples.Games.TwinStickShooter3D.TwinStickShooter3D_Version02_DOTS
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