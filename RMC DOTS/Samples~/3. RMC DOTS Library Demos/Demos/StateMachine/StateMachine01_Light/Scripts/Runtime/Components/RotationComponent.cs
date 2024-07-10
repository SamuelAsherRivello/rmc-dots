using Unity.Entities;
using Unity.Mathematics;

namespace RMC.DOTS.Demos.StateMachine.Light
{
    public struct RotationComponent : IComponentData
    {
        public float3 RotationDelta;
        public float DurationInSeconds;
    }
}

