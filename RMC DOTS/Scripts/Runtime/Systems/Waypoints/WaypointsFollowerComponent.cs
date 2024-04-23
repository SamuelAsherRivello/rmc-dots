using Unity.Entities;
using UnityEngine;

namespace RMC.DOTS.Systems.Waypoints
{
    public struct WaypointsFollowerComponent : IComponentData
    {
        public float LinearSpeed;
        public float AngularSpeed;
        public int NextWaypointIndex;
    }
}