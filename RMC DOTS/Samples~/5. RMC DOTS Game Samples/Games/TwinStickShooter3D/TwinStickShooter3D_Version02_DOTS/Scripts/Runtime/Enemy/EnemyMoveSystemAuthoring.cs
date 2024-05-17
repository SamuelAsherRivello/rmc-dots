using Unity.Entities;
using UnityEngine;

namespace RMC.DOTS.Samples.Games.TwinStickShooter3D.TwinStickShooter3D_Version02_DOTS
{
    public class EnemyMoveSystemAuthoring : MonoBehaviour
    {
        [SerializeField] 
        public bool IsSystemEnabled = true;

        public struct EnemyMoveSystemIsEnabledTag : IComponentData { }

        public class EnemyMoveSystemAuthoringBaker : Baker<EnemyMoveSystemAuthoring>
        {
            public override void Bake(EnemyMoveSystemAuthoring authoring)
            {
                if (authoring.IsSystemEnabled)
                {
                    Entity entity = GetEntity(TransformUsageFlags.None);
                    AddComponent<EnemyMoveSystemIsEnabledTag>(entity);
                }
            }
        }
    }
}