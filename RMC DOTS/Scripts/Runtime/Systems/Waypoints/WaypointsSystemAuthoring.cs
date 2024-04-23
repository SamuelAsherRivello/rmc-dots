using Unity.Entities;
using UnityEngine;

namespace RMC.DOTS.Systems.Waypoints
{
    /// <summary>
    /// Place this MonoBehaviour on a GameObject in the Scene
    /// To enable the <see cref="WaypointsFollowerSystem"/>
    /// </summary>
    public class WaypointsSystemAuthoring : MonoBehaviour
    {
        [SerializeField] 
        public bool IsSystemEnabled = true;
        
        public struct WaypointsSystemIsEnabledTag : IComponentData {}
        
        public class WaypointsSystemAuthoringBaker : Baker<WaypointsSystemAuthoring>
        {
            public override void Bake(WaypointsSystemAuthoring authoring)
            {
                if (authoring.IsSystemEnabled)
                {
                    Entity entity = GetEntity(TransformUsageFlags.Dynamic);
                    
                    //Turn system on
                    AddComponent<WaypointsSystemIsEnabledTag>(entity);
                }
            }
        }
    }
}