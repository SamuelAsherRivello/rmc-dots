using Unity.Entities;
using UnityEngine;

namespace RMC.DOTS.Systems.Timer
{
    /// <summary>
    /// Place this MonoBehaviour on a GameObject in the Scene
    /// To enable the <see cref="BaseTimerSystem"/>
    /// </summary>
    public class BaseTimerSystemAuthoring : MonoBehaviour
    {
        [SerializeField] 
        public bool IsSystemEnabled = true;
        
        public struct BaseTimerSystemIsEnabledTag : IComponentData {}
        
        public class DestroyEntitySystemAuthoringBaker : Baker<BaseTimerSystemAuthoring>
        {
            public override void Bake(BaseTimerSystemAuthoring systemAuthoring)
            {
                if (systemAuthoring.IsSystemEnabled)
                {
                    Entity entity = GetEntity(TransformUsageFlags.Dynamic);
                    
                    //Turn system on
                    AddComponent<BaseTimerSystemIsEnabledTag>(entity);
                }
            }
        }
    }
}
