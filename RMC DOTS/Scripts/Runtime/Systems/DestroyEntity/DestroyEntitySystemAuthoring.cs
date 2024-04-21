using Unity.Entities;
using UnityEngine;

namespace RMC.DOTS.Systems.DestroyEntity
{
    /// <summary>
    /// Place this MonoBehaviour on a GameObject in the Scene
    /// To enable the <see cref="DestroyEntitySystem"/>
    /// </summary>
    public class DestroyEntitySystemAuthoring : MonoBehaviour
    {
        [SerializeField] 
        public bool IsSystemEnabled = true;
        
        public struct DestroyEntitySystemIsEnabledTag : IComponentData {}
        
        public class DestroyEntitySystemAuthoringBaker : Baker<DestroyEntitySystemAuthoring>
        {
            public override void Bake(DestroyEntitySystemAuthoring systemAuthoring)
            {
                if (systemAuthoring.IsSystemEnabled)
                {
                    Entity entity = GetEntity(TransformUsageFlags.Dynamic);
                    
                    //Turn system on
                    AddComponent<DestroyEntitySystemIsEnabledTag>(entity);
                }
            }
        }
    }
}
