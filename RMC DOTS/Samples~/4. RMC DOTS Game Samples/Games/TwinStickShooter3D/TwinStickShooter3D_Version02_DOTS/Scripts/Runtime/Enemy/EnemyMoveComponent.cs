using Unity.Entities;

namespace RMC.DOTS.Samples.Games.TwinStickShooter3D.TwinStickShooter3D_Version02_DOTS
{
    public struct EnemyMoveComponent : IComponentData
    {
        public float TurnSpeed;
        public float MoveSpeed;

        public EnemyMoveComponent(float newMoveSpeed, float newTurnSpeed)
        {
            MoveSpeed = newMoveSpeed;
            TurnSpeed = newTurnSpeed;
        }
    }
}