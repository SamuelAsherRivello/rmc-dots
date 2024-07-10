using RMC.DOTS.Systems.StateMachine;
using Unity.Entities;
using Unity.Mathematics;

namespace RMC.DOTS.Demos.StateMachine.Full
{
    struct MyMovementDataComponent : IComponentData
    {
        public float3 RotationDelta;
        public float RotationDurationInSeconds;
        public float3 TranslationDelta;
        public float TranslationDurationInSeconds;
    }
}