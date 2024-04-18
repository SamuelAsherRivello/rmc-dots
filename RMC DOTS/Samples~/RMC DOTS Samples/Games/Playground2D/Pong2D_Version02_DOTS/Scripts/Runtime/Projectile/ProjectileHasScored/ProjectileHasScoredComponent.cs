using Unity.Entities;

namespace RMC.Playground3D.Pong2D_Version02_DOTS
{
    public struct ProjectileHasScoredComponent : IComponentData
    {
        public PlayerType PlayerType;
    }
}