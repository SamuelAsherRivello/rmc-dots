using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace RMC.DOTS.Samples.RollABall3D.RollABall3D_Version02_DOTS
{
    public class PickupRotationAuthoring : MonoBehaviour
    {
        public float Speed;
        public float3 Direction;

        public class PickupRotationBaker : Baker<PickupRotationAuthoring>
        {
            public override void Bake(PickupRotationAuthoring authoring)
            {
                Entity entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new PickupRotationComponent
                {
                    Direction = new float3
                    {
                        x = math.radians(authoring.Direction.x),
                        y = math.radians(authoring.Direction.y),
                        z = math.radians(authoring.Direction.z)
                    }, 
                    Speed = authoring.Speed
                });
            }
        }
    }
}