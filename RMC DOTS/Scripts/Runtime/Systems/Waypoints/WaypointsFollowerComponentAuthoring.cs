using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace RMC.DOTS.Systems.Waypoints
{
    public class WaypointsFollowerComponentAuthoring : MonoBehaviour
    {
        public float LinearSpeed = 10;
        public float AngularSpeed = 10;
        public List<Transform> Waypoints;

        public class WaypointsFollowerComponentAuthoringBaker : Baker<WaypointsFollowerComponentAuthoring>
        {
            public override void Bake(WaypointsFollowerComponentAuthoring authoring)
            {
                Entity entity = GetEntity(TransformUsageFlags.Dynamic);

                // Add data INSIDE the component
                AddComponent<WaypointsFollowerComponent>(entity,
                    new WaypointsFollowerComponent
                    {
                        LinearSpeed = authoring.LinearSpeed,
                        AngularSpeed = authoring.AngularSpeed
                    });
                
                
                // Add data OUTSIDE the component
                if (authoring.Waypoints == null)
                {
                    return;
                }
                
                var waypointBuffer = AddBuffer<WaypointBufferElementData>(entity);
 
                for (int i = 0; i < authoring.Waypoints.Count; i++)
                {
                    if (authoring.Waypoints[i] == null)
                    {
                        continue;
                    }
                    
                    DependsOn(authoring.Waypoints[i]);
 
                    waypointBuffer.Add(new WaypointBufferElementData
                    {
                        Position = authoring.Waypoints[i].position
                    });
                }

            }
        }
    }
}
