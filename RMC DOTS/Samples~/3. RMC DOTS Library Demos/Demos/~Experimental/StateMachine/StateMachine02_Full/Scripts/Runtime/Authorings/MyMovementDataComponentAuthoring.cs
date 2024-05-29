using RMC.DOTS.Systems.StateMachine;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace RMC.DOTS.Demos.StateMachine.Full
{
    public class MyMovementDataComponentAuthoring : MonoBehaviour
    {
        [Header("Rotation")]
        public Vector3 RotationDelta = new float3(0, 1f, 0);
        public float RotationDurationInSeconds = 1;

        [Header("Translation")]
        public Vector3 TranslationDelta = new float3(0, 0.5f, 0);
        public float TranslationDurationInSeconds = 1;

        public class MyMovementDataComponentAuthoringBaker : 
            Baker<MyMovementDataComponentAuthoring>
        {
            public override void Bake(MyMovementDataComponentAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);

                //Give this entity 1) an ID and 2) a state to be in
                AddComponent<StateID>(entity);
                AddComponent<MyMovementDataComponent>(entity, new MyMovementDataComponent
                {
                    RotationDelta = authoring.RotationDelta,
                    RotationDurationInSeconds = authoring.RotationDurationInSeconds,
                    TranslationDelta = authoring.TranslationDelta,
                    TranslationDurationInSeconds = authoring.TranslationDurationInSeconds
                });
            }
        }
    }
}