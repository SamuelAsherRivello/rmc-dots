using Unity.Entities;
using Unity.Mathematics;

namespace RMC.DOTS.Demos.StateMachine
{
    public struct TranslationComponent : IComponentData
    {
        public float3 TranslationDelta;
        public float DurationInSeconds;
    }
}
