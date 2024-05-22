using RMC.Core.Utilities;
using RMC.DOTS.SystemGroups;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Physics.Extensions;
using Unity.Transforms;
using UnityEngine;

namespace RMC.DOTS.Systems.FollowTarget
{
    [UpdateInGroup(typeof(PauseablePresentationSystemGroup))]
    [BurstCompile]
    public partial struct FollowTargetSystem : ISystem
    {

        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<FollowTargetSystemAuthoring.FollowTargetSystemAuthoringIsEnabledTag>();
            state.RequireForUpdate<FollowerComponent>();
            state.RequireForUpdate<TargetComponent>();
        }
		
        public void OnUpdate(ref SystemState state)
        {
            float deltaTime = SystemAPI.Time.DeltaTime;
  
            
            // Move towards the target
            foreach 
            (
                var (followerLocalTransform, physicsVelocity, physicsMass, followerComponent) in
                SystemAPI.Query<RefRW<LocalTransform>, RefRW<PhysicsVelocity>, RefRO<PhysicsMass>, RefRO<FollowerComponent>>()
            )
            {

                if (!followerComponent.ValueRO.IsEnabled)
                {
                    // TODO: maybe slow down movement here
                    continue;
                }
                
                // Find the target
                float3 targetPosition = float3.zero;
                bool hasFoundTarget = false;
                foreach 
                (
                    var (targetLocalTransform, targetComponent) in
                    SystemAPI.Query < RefRO<LocalTransform>, RefRO<TargetComponent> > ()
                    ) 
                {
       
                    // Layer mask match?
                    bool isMemberWithinTargets = LayerMaskUtility.LayerMaskContainsLayerMask(
                        targetComponent.ValueRO.MemberOfLayerMask, 
                        followerComponent.ValueRO.TargetsLayerMask);
                    
                    
                    if (isMemberWithinTargets)
                    {
                        //TODO: This is grabbing the FIRST match? Instead do something else?
                        targetPosition = targetLocalTransform.ValueRO.Position;
                        hasFoundTarget = true;
                    }
                }

                //Not close enough?
                float3 deltaPosition = targetPosition - followerLocalTransform.ValueRO.Position;
                if (!hasFoundTarget || math.length(deltaPosition) > followerComponent.ValueRO.Radius )
                {
                    continue;
                }
                
                deltaPosition = math.normalizesafe(deltaPosition);

                physicsVelocity.ValueRW.ApplyLinearImpulse(in physicsMass.ValueRO, 
                    deltaTime * followerComponent.ValueRO.LinearSpeed * deltaPosition);

                quaternion targetRotation = quaternion.EulerXYZ(new float3(0.0f, math.atan2(deltaPosition.x, deltaPosition.z), 0.0f));
                followerLocalTransform.ValueRW.Rotation = math.slerp(followerLocalTransform.ValueRO.Rotation, targetRotation, followerComponent.ValueRO.AngularSpeed * deltaTime);
            }
        }
    }
}