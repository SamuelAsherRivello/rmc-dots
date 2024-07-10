using Unity.Entities;
using Unity.Transforms;

namespace RMC.DOTS.Demos.StateMachine.Full
{
    public class MyMovementTranslationState : MyMovementBaseState
    {
        ///////////////////////////////////////////////////
        // StateMachine Comment #04: Note that the API
        // language here feels a bit different from a typical
        // DOTS foreach loop. This is because we are using
        // the StateMachineSystemBase API.
        ///////////////////////////////////////////////////
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
            if (StateElapsedTimeInSeconds >= myMovementDataComponent.TranslationDurationInSeconds)
            {
                ///////////////////////////////////////////////////
                // StateMachine Comment #06: Do transition here
                ///////////////////////////////////////////////////
                RequestStateChangePerTransitions(entity);
            }
            else
            {
                ///////////////////////////////////////////////////
                // StateMachine Comment #05: Do state logic here
                ///////////////////////////////////////////////////

                // Update Component
                localTransform.Position += myMovementDataComponent.TranslationDelta * deltaTime;
                
                // Set Component
                EntityManager.SetComponentData(entity, localTransform);
            }
        }
    }
}