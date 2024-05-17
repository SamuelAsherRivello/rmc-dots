using Unity.Entities;
using UnityEngine;

namespace RMC.DOTS.Systems.Timer
{
    /// <summary>
    /// Place this MonoBehaviour on a GameObject in the Scene
    /// To enable the <see cref="TimerDemoSystem"/>
    /// </summary>
    public class TimerDemoSystemAuthoring : MonoBehaviour
    {
        [SerializeField] 
        public bool IsSystemEnabled = true;
        
        public struct TimerDemoSystemIsEnabledTag : IComponentData {}
        
        public class TimerDemoSystemBaker : Baker<TimerDemoSystemAuthoring>
        {
            public override void Bake(TimerDemoSystemAuthoring authoring)
            {
                if (authoring.IsSystemEnabled)
                {
                    Entity entity = GetEntity(TransformUsageFlags.Dynamic);
                    
                    //Turn system on
                    AddComponent<TimerDemoSystemIsEnabledTag>(entity);
                }
            }
        }
    }
}
