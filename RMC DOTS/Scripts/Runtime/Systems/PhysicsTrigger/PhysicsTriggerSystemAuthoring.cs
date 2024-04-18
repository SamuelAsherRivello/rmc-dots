using Unity.Entities;
using UnityEngine;

namespace RMC.DOTS.Systems.PhysicsTrigger
{
    /// <summary>
    /// Place this MonoBehaviour on a GameObject in the Scene
    /// To enable the <see cref="PhysicsTriggerSystem"/>
    /// </summary>
    public class PhysicsTriggerSystemAuthoring : MonoBehaviour
    {
        [SerializeField] 
        public bool IsSystemEnabled = true;
        
        public struct PhysicsTriggerSystemIsEnabledTag : IComponentData {}
        
        public class PhysicsTriggerSystemAuthoringBaker : Baker<PhysicsTriggerSystemAuthoring>
        {
            public override void Bake(PhysicsTriggerSystemAuthoring authoring)
            {
                if (authoring.IsSystemEnabled)
                {
                    Entity entity = GetEntity(TransformUsageFlags.Dynamic);
                    
                    //Turn system on
                    AddComponent<PhysicsTriggerSystemIsEnabledTag>(entity);
                }
            }
        }
    }
}