using Unity.Entities;

namespace RMC.DOTS.Systems.Random
{
    public struct RandomComponent : IComponentData
    {
        public Unity.Mathematics.Random Random;
    }
}