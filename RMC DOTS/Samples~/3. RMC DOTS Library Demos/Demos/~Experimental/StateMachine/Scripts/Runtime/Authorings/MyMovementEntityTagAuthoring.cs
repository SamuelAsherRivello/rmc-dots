using RMC.DOTS.Systems.StateMachine;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace RMC.DOTS.Demos.StateMachine
{
    public class MyMovementEntityTagAuthoring : MonoBehaviour
    {
        public Entity entity;

        public class Test01AuthoringBaker : Baker<MyMovementEntityTagAuthoring>
        {
            public override void Bake(MyMovementEntityTagAuthoring authoring)
            {
                authoring.entity = GetEntity(TransformUsageFlags.Dynamic);

                // TODO: Remove this? How else can I turn on the first state easily?
                //(This is more of a "how to easily find an entity?" question than an SM question)
                AddComponent<MyMovementEntityTag>(authoring.entity);
                
                //Give this entity 1) an ID and 2) a state to be in
                AddComponent<StateID>(authoring.entity);
                AddComponent<MyMovementStateSystemTag>(authoring.entity);

                AddComponent<RotationComponent>(authoring.entity,
                    new RotationComponent()
                    {
                        RotationDelta = new float3(0, 0.25f, 0),
                        DurationInSeconds = 1
                    });
                
                AddComponent<TranslationComponent>(authoring.entity,
                    new TranslationComponent()
                    {
                        TranslationDelta = new float3(0, 0.25f, 0),
                        DurationInSeconds = 1
                    });

            }
        }

    }
}