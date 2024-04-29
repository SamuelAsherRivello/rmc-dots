using Unity.Entities;
using Unity.Mathematics;

namespace RMC.DOTS.Samples.Templates.DOTSGameTemplate
{
    public struct RotateComponent : IComponentData
    {
        public float Speed;
        public float3 Direction;
    }
}