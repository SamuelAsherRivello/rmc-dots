using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace RMC.DOTS.Samples.Games.TwinStickShooter3D.TwinStickShooter3D_Version02_DOTS
{
    public class RotateComponentAuthoring : MonoBehaviour
    {
        public float Speed;
        public float3 Direction;

        public class RotateComponentAuthoringBaker : Baker<RotateComponentAuthoring>
        {
            public override void Bake(RotateComponentAuthoring authoring)
            {
                Entity entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new RotateComponent
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
