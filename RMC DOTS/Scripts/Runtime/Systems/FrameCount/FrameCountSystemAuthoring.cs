using Unity.Entities;
using UnityEngine;

namespace RMC.DOTS.Systems.FrameCount
{
    /// <summary>
    /// Place this MonoBehaviour on a GameObject in the Scene
    /// To enable the <see cref="PhysicsTriggerSystem"/>
    /// </summary>
    public class FrameCountSystemAuthoring : MonoBehaviour
    {
        [SerializeField] 
        public bool IsSystemEnabled = true;
        
        public struct FrameCountSystemIsEnabledTag : IComponentData {}
        
        public class FrameCountSystemAuthoringBaker : Baker<FrameCountSystemAuthoring>
        {
            public override void Bake(FrameCountSystemAuthoring authoring)
            {
                if (authoring.IsSystemEnabled)
                {
                    Entity entity = GetEntity(TransformUsageFlags.Dynamic);
                    
                    //Turn system on
                    AddComponent<FrameCountSystemIsEnabledTag>(entity);
                }
            }
        }
    }
}
