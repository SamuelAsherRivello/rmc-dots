using Unity.Entities;

namespace RMC.DOTS.Samples.Games.TwinStickShooter3D.TwinStickShooter3D_Version02_DOTS
{
    /// <summary>
    /// Contains all the data needed for the enemy to drop a gem via
    /// <see cref="WasHitSystem"/>
    /// </summary>
    public struct GemSpawnComponent : IComponentData
    {
        public readonly Entity GemPrefab;   
        public readonly float GemSpeed;      

        public GemSpawnComponent(Entity gemPrefab, float gemSpeed)
        {
            GemPrefab = gemPrefab;
            GemSpeed = gemSpeed;
        }
    }
}