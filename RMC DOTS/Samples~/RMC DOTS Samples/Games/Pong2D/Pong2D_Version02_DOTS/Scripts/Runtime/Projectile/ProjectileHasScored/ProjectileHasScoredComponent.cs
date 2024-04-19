using Unity.Entities;

namespace RMC.DOTS.Samples.Pong2D.Pong2D_Version02_DOTS
{
    public struct ProjectileHasScoredComponent : IComponentData
    {
        public PlayerType PlayerType;
    }
}