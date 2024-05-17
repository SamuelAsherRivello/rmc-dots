using Unity.Entities;
using UnityEngine;

namespace RMC.DOTS.Samples.Games.TwinStickShooter3D.TwinStickShooter3D_Version02_DOTS
{
    public class EnemyMoveComponentAuthoring : MonoBehaviour
    {
        public float MoveSpeed = 10.0f;
        public float TurnSpeed = 1.5f;

        public class EnemyMoveComponentAuthoringBaker : Baker<EnemyMoveComponentAuthoring>
        {
            public override void Bake(EnemyMoveComponentAuthoring authoring)
            {
                Entity entity = GetEntity(TransformUsageFlags.Dynamic);

                AddComponent<EnemyMoveComponent>(entity, new EnemyMoveComponent(authoring.MoveSpeed, authoring.TurnSpeed));
            }
        }
    }
}