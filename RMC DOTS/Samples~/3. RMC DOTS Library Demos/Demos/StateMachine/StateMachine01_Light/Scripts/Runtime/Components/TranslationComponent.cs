using Unity.Entities;
using Unity.Mathematics;

namespace RMC.DOTS.Demos.StateMachine.Light
{
    public struct TranslationComponent : IComponentData
    {
        public float3 TranslationDelta;
        public float DurationInSeconds;
    }
}
