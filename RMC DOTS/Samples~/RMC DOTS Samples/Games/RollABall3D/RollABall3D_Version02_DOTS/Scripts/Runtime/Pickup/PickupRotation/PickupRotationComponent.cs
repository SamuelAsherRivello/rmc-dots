using Unity.Entities;
using Unity.Mathematics;

namespace RMC.DOTS.Samples.RollABall3D.RollABall3D_Version02_DOTS
{
    public struct PickupRotationComponent : IComponentData
    {
        public float Speed;
        public float3 Direction;
    }
}