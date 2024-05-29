using RMC.DOTS.Systems.StateMachine;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace RMC.DOTS.Demos.StateMachine
{
    public class MyMovementRotationState : MyMovementBaseState
    {
        public override void OnUpdate(Entity entity)
        {
            base.OnUpdate(entity);
            var deltaTime = System.World.Time.DeltaTime;
                
            // Check Component
            if (!EntityManager.HasComponent<LocalTransform>(entity) ||
                !EntityManager.HasComponent<RotationComponent>(entity))
            {
                return;
            }
                
            // Get Component
            RotationComponent rotationComponent = EntityManager.GetComponentData<RotationComponent>(entity);
            LocalTransform localTransform = EntityManager.GetComponentData<LocalTransform>(entity);
            
            // Consider Transition
            if (StateElapsedTimeInSeconds >= rotationComponent.DurationInSeconds)
            {
                RequestStateChangePerTransitions(entity);
            }
            else
            {
                // Update Component
                quaternion rotation = quaternion.Euler(rotationComponent.RotationDelta * deltaTime);
                localTransform.Rotation = math.mul(localTransform.Rotation, rotation);
                
                // Set Component
                EntityManager.SetComponentData(entity, localTransform);
            }
        }
    }
}