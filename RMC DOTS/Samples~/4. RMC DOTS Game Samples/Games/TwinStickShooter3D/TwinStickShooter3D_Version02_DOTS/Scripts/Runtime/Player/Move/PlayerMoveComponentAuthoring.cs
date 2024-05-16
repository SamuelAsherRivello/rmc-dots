using Unity.Entities;
using UnityEngine;

namespace RMC.DOTS.Samples.Games.TwinStickShooter3D.TwinStickShooter3D_Version02_DOTS
{
    public class PlayerMoveComponentAuthoring : MonoBehaviour
    {
        public float Speed = 7.5f;
        
        public class PlayerMoveComponentAuthoringBaker : Baker<PlayerMoveComponentAuthoring>
        {
            public override void Bake(PlayerMoveComponentAuthoring authoring)
            {
                Entity entity = GetEntity(TransformUsageFlags.Dynamic);
                                
                AddComponent<PlayerMoveComponent>(entity, 
                    new PlayerMoveComponent { Value = authoring.Speed });
            }
        }
    }
}