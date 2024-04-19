using RMC.DOTS.Systems.PhysicsTrigger;
using Unity.Entities;
using UnityEngine;

namespace RMC.DOTS.Samples.RollABall3D.RollABall3D_Version02_DOTS
{
    public class PlayerMoveAuthoring : MonoBehaviour
    {
        public float Speed = 7.5f;
        
        public class PlayerMoveAuthoringBaker : Baker<PlayerMoveAuthoring>
        {
            public override void Bake(PlayerMoveAuthoring authoring)
            {
                Entity entity = GetEntity(TransformUsageFlags.Dynamic);
                                
                AddComponent<PlayerMoveComponent>(entity, 
                    new PlayerMoveComponent { Value = authoring.Speed });
            }
        }
    }
}