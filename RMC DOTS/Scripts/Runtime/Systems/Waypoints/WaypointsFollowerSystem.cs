using RMC.DOTS.SystemGroups;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace RMC.DOTS.Systems.Waypoints
{
    [BurstCompile]
    [UpdateInGroup(typeof(PauseableSystemGroup))]
    public partial class WaypointsFollowerSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            float deltaTime = World.Time.DeltaTime;
            
            Entities.ForEach(
                (ref LocalTransform localTransform,
                ref WaypointsFollowerComponent waypointsFollowerComponent,
                in DynamicBuffer<WaypointBufferElementData> waypoints) =>
            {
                
                float3 movement = waypoints[waypointsFollowerComponent.NextWaypointIndex].Position 
                                  - localTransform.Position;
                
                // Increment the next waypoint index if the current waypoint is reached
                if(math.distance(localTransform.Position, 
                       waypoints[waypointsFollowerComponent.NextWaypointIndex].Position) < 0.1f)
                {
                    //TODO: Add a WillWrap value?
                    //TODO: Add an GO event or DOTS tag when reaching each waypoint?
                    waypointsFollowerComponent.NextWaypointIndex = 
                        (waypointsFollowerComponent.NextWaypointIndex + 1) % waypoints.Length;
                }

                // Face
                var targetRotation = quaternion.LookRotation(-movement, Vector3.up);
                localTransform.Rotation = math.slerp(localTransform.Rotation, targetRotation, 
                    waypointsFollowerComponent.AngularSpeed * deltaTime);
                
                // Move
                localTransform.Position += math.normalize(movement) * deltaTime *
                                           waypointsFollowerComponent.LinearSpeed;
               
            }).Schedule();
        }
    }
}