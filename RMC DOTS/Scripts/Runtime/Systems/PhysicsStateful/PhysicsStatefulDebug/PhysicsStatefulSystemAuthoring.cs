using RMC.DOTS.Systems.FrameCount;
using Unity.Entities;
using UnityEngine;

namespace Unity.Physics.PhysicsStateful
{
    /// <summary>
    /// Place this MonoBehaviour on a GameObject in the Scene
    /// To enable the <see cref="PhysicsStatefulDebugSystem"/>
    /// To enable the <see cref="StatefulTriggerEventSystem"/>
    /// To enable the <see cref="StatefulCollisionEventSystem"/>
    /// 
    /// </summary>
    public class PhysicsStatefulSystemAuthoring : MonoBehaviour
    {
        [Header("Enable Systems (Required)")]
        [SerializeField] 
        public bool CollisionSystemIsEnabled = true;
        
        [SerializeField] 
        public bool TriggerSystemIsEnabled = true;
        
        [Header("Enable Debugging (Optional)")]
        [SerializeField] 
        public bool DebugSystemIsEnabled = true;
        
        public struct CollisionSystemIsEnabledIsEnabledTag : IComponentData {}
        public struct TriggerSystemIsEnabledIsEnabledTag : IComponentData {}
        public struct DebugSystemIsEnabledIsEnabledTag : IComponentData {}

        public class PhysicsStatefulDebugSystemAuthoringBaker : Baker<PhysicsStatefulSystemAuthoring>
        {
            public override void Bake(PhysicsStatefulSystemAuthoring authoring)
            {
                if (authoring.CollisionSystemIsEnabled || authoring.TriggerSystemIsEnabled || authoring.DebugSystemIsEnabled)
                {
                    Entity inputEntity = GetEntity(TransformUsageFlags.Dynamic);
                    
                    //Turn system on
                    if (authoring.CollisionSystemIsEnabled)
                    {
                        AddComponent<CollisionSystemIsEnabledIsEnabledTag>(inputEntity);
                    }
                    if (authoring.TriggerSystemIsEnabled)
                    {
                        AddComponent<TriggerSystemIsEnabledIsEnabledTag>(inputEntity);
                    }
                    if (authoring.DebugSystemIsEnabled)
                    {
                        AddComponent<DebugSystemIsEnabledIsEnabledTag>(inputEntity);
                    }
                }
            }
        }
    }
}
