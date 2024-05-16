using Unity.Entities;

namespace RMC.DOTS.Samples.Games.TwinStickShooter3D.TwinStickShooter3D_Version02_DOTS
{
    public struct HealthComponent : IComponentData
    {
        public float CurrentHealth;
        public float MaxHealth;

        public HealthComponent(float newCurrentHealth, float newMaxHealth)
        {
            CurrentHealth = newCurrentHealth;
            MaxHealth = newMaxHealth;
        }
    }
}