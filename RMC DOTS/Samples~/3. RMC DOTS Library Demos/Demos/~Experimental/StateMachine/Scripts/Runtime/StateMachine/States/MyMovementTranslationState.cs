using Unity.Entities;
using Unity.Transforms;

namespace RMC.DOTS.Demos.StateMachine
{
    public class MyMovementTranslationState : MyMovementBaseState
    {
        public override void OnUpdate(Entity entity)
        {
            base.OnUpdate(entity);
            var deltaTime = System.World.Time.DeltaTime;

            // Check Component
            if (!EntityManager.HasComponent<LocalTransform>(entity) ||
                !EntityManager.HasComponent<TranslationComponent>(entity))
            {
                return;
            }

            // Get Component
            TranslationComponent translationComponent = EntityManager.GetComponentData<TranslationComponent>(entity);
            LocalTransform localTransform = EntityManager.GetComponentData<LocalTransform>(entity);

            // Consider Transition
            if (StateElapsedTimeInSeconds >= translationComponent.DurationInSeconds)
            {
                RequestStateChangePerTransitions(entity);
            }
            else
            {
                // Update Component
                localTransform.Position += translationComponent.TranslationDelta * deltaTime;
                
                // Set Component
                EntityManager.SetComponentData(entity, localTransform);
            }
        }
    }
}