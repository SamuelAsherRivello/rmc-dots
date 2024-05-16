using Unity.Entities;
using Unity.Mathematics;

namespace RMC.DOTS.Samples.Games.TwinStickShooter3D.TwinStickShooter3D_Version02_DOTS
{
    public struct RotateComponent : IComponentData
    {
        public float Speed;
        public float3 Direction;
    }
}