using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace RMC.DOTS.Samples.Templates.DOTSGameTemplate
{
    public class RotateAuthoring : MonoBehaviour
    {
        public float Speed;
        public float3 Direction;

        public class RotateAuthoringBaker : Baker<RotateAuthoring>
        {
            public override void Bake(RotateAuthoring authoring)
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