using Unity.Entities;
using UnityEngine;

namespace RMC.DOTS.Systems.Timer
{
    /// <summary>
    /// Place this MonoBehaviour on a GameObject in the Scene
    /// To enable the <see cref="TimerSystem"/>
    /// </summary>
    public class TimerSystemAuthoring : MonoBehaviour
    {
        [SerializeField] 
        public bool IsSystemEnabled = true;
        
        public struct TimerSystemIsEnabledTag : IComponentData {}
        
        public class TimerSystemAuthoringBaker : Baker<TimerSystemAuthoring>
        {
            public override void Bake(TimerSystemAuthoring authoring)
            {
                if (authoring.IsSystemEnabled)
                {
                    Entity entity = GetEntity(TransformUsageFlags.Dynamic);
                    
                    //Turn system on
                    AddComponent<TimerSystemIsEnabledTag>(entity);
                }
            }
        }
    }
}
