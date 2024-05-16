using Unity.Entities;
using Unity.Mathematics;

namespace RMC.DOTS.Samples.Games.TwinStickShooter3D.TwinStickShooter3D_Version02_DOTS
{
    readonly partial struct HealthAspect : IAspect
    {
        readonly RefRW<HealthComponent> HealthComponent;

        public void DealDamage(float damage)
        {
            //Debug.Log($"Dealing damage={damage} to CurrentHealth={CurrentHealth}");
            HealthComponent.ValueRW.CurrentHealth = math.max(0.0f, HealthComponent.ValueRW.CurrentHealth - damage);
        }

        public bool IsDead => HealthComponent.ValueRO.CurrentHealth == 0.0f;

        public float CurrentHealth => HealthComponent.ValueRO.CurrentHealth;
    }
}
