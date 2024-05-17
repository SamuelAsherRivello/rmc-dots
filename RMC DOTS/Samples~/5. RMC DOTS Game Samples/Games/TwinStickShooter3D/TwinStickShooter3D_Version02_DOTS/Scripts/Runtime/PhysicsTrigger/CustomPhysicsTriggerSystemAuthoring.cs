using Unity.Entities;
using UnityEngine;

namespace RMC.DOTS.Samples.Games.TwinStickShooter3D.TwinStickShooter3D_Version02_DOTS
{
    public class CustomPhysicsTriggerSystemAuthoring : MonoBehaviour
    {
        [SerializeField] 
        public bool IsSystemEnabled = true;
        
        public struct CustomPhysicsTriggerSystemIsEnabledTag : IComponentData {}
        
        public class CustomPhysicsTriggerSystemAuthoringBaker : Baker<CustomPhysicsTriggerSystemAuthoring>
        {
            public override void Bake(CustomPhysicsTriggerSystemAuthoring authoring)
            {
                if (authoring.IsSystemEnabled)
                {
                    Entity entity = GetEntity(TransformUsageFlags.Dynamic);
                    AddComponent<CustomPhysicsTriggerSystemIsEnabledTag>(entity);
                }
            }
        }
    }
}