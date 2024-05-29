using RMC.DOTS.Systems.StateMachine;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace RMC.DOTS.Demos.StateMachine.Light
{
    public class MyMovementEntityTagAuthoring : MonoBehaviour
    {
        public Vector3 RotationDelta = new float3(0, 1f, 0);
        public float RotationDurationInSeconds = 1;

        [Header("Translation")]
        public Vector3 TranslationDelta = new float3(0, 0.5f, 0);
        public float TranslationDurationInSeconds = 1;

        public class Test01AuthoringBaker : Baker<MyMovementEntityTagAuthoring>
        {
            public override void Bake(MyMovementEntityTagAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);

                // TODO: Remove this? How else can I turn on the first state easily?
                //(This is more of a "how to easily find an entity?" question than an SM question)
                AddComponent<MyMovementEntityTag>(entity);
                
                //Give this entity 1) an ID and 2) a state to be in
                AddComponent<StateID>(entity);
                AddComponent<MyMovementStateSystemTag>(entity);

                AddComponent<RotationComponent>(entity,
                    new RotationComponent()
                    {
                        RotationDelta = authoring.RotationDelta,
                        DurationInSeconds = authoring.RotationDurationInSeconds
                    });
                
                AddComponent<TranslationComponent>(entity,
                    new TranslationComponent()
                    {
                        TranslationDelta = authoring.TranslationDelta,
                        DurationInSeconds = authoring.TranslationDurationInSeconds
                    });

            }
        }

    }
}