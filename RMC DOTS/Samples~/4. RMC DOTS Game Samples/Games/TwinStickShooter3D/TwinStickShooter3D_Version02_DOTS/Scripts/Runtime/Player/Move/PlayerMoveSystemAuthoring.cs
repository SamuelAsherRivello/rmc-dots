using Unity.Entities;
using UnityEngine;

namespace RMC.DOTS.Samples.Games.TwinStickShooter3D.TwinStickShooter3D_Version02_DOTS
{
    public class PlayerMoveSystemAuthoring : MonoBehaviour
    {
        [SerializeField] 
        public bool IsSystemEnabled = true;
        
        public struct PlayerMoveSystemIsEnabledTag : IComponentData {}
        
        public class PlayerMoveSystemAuthoringBaker : Baker<PlayerMoveSystemAuthoring>
        {
            public override void Bake(PlayerMoveSystemAuthoring authoring)
            {
                if (authoring.IsSystemEnabled)
                {
                    Entity entity = GetEntity(TransformUsageFlags.Dynamic);
                    AddComponent<PlayerMoveSystemIsEnabledTag>(entity);
                }
            }
        }
    }
}