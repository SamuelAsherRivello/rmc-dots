using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace RMC.DOTS.Demos.StateMachine.Full
{
    public class MyMovementRotationState : MyMovementBaseState
    {
        public override void OnUpdate(Entity entity)
        {
            base.OnUpdate(entity);
            var deltaTime = StateMachineSystemBase.World.Time.DeltaTime;
                
            // Check Component
            if (!EntityManager.HasComponent<LocalTransform>(entity) ||
                !EntityManager.HasComponent<MyMovementDataComponent>(entity))
            {
                return;
            }
                
            // Get Component
            MyMovementDataComponent myMovementDataComponent = EntityManager.GetComponentData<MyMovementDataComponent>(entity);
            LocalTransform localTransform = EntityManager.GetComponentData<LocalTransform>(entity);
            
            // Consider Transition
            if (StateElapsedTimeInSeconds >= myMovementDataComponent.RotationDurationInSeconds)
            {
                RequestStateChangePerTransitions(entity);
            }
            else
            {
                // Update Component
                quaternion rotation = quaternion.Euler(myMovementDataComponent.RotationDelta * deltaTime);
                localTransform.Rotation = math.mul(localTransform.Rotation, rotation);
                
                // Set Component
                EntityManager.SetComponentData(entity, localTransform);
            }
        }
    }
}