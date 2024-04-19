using Unity.Entities;
using Unity.Mathematics;

namespace RMC.DOTS.Samples.RollABall3D.RollABall3D_Version02_DOTS
{
    /// <summary>
    /// This data component defines the speed and direction the pickups will rotate in the game world.
    /// </summary>
    public struct PickupRotationComponent : IComponentData
    {
        public float Speed;
        public float3 Direction;
    }
}