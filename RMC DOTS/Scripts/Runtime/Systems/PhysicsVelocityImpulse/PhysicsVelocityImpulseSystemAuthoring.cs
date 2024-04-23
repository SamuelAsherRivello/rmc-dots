using Unity.Entities;
using UnityEngine;

namespace RMC.DOTS.Systems.PhysicsVelocityImpulse
{
    /// <summary>
    /// Place this MonoBehaviour on a GameObject in the Scene
    /// To enable the <see cref="PhysicsVelocityImpulseSystem"/>
    /// </summary>
    public class PhysicsVelocityImpulseSystemAuthoring : MonoBehaviour
    {
        [SerializeField] 
        public bool IsSystemEnabled = true;
        
        public struct PhysicsVelocityImpulseSystemIsEnabledTag : IComponentData {}
        
        public class PhysicsVelocityImpulseSystemAuthoringBaker : Baker<PhysicsVelocityImpulseSystemAuthoring>
        {
            public override void Bake(PhysicsVelocityImpulseSystemAuthoring authoring)
            {
                if (authoring.IsSystemEnabled)
                {
                    Entity entity = GetEntity(TransformUsageFlags.Dynamic);
                    
                    //Turn system on
                    AddComponent<PhysicsVelocityImpulseSystemIsEnabledTag>(entity);
                }
            }
        }
    }
}