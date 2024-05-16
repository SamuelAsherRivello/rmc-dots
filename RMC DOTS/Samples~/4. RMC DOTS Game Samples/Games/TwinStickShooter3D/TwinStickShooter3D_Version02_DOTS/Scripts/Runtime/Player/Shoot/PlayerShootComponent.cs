using Unity.Entities;

namespace RMC.DOTS.Samples.Games.TwinStickShooter3D.TwinStickShooter3D_Version02_DOTS
{
    /// <summary>
    /// Contains all the data needed for the player to shoot via
    /// <see cref="PlayerShootSystem"/>
    /// </summary>
    public struct PlayerShootComponent : IComponentData
    {
        // PUBLIC (Without "_"), Call directly
        public readonly Entity BulletPrefab;    // Prefab of the bullet entity
        public readonly float BulletSpeed;      // Speed of the bullet
        public readonly float BulletFireRate;   // Cooldown time between shots in seconds
        
        // INTERNAL (With "_"), Do not call directly
        public float _CooldownTimer;

        public PlayerShootComponent(Entity bulletPrefab, float bulletSpeed, float bulletFireRate)
        {
            BulletPrefab = bulletPrefab;
            BulletSpeed = bulletSpeed;
            BulletFireRate = bulletFireRate;
            _CooldownTimer = -1;
        }
    }
}