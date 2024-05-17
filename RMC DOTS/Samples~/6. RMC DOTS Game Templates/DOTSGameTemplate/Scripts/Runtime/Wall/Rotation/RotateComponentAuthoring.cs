using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace RMC.DOTS.Samples.Templates.DOTSGameTemplate
{
    public class RotateComponentAuthoring : MonoBehaviour
    {
        public float Speed;
        public float3 Direction;

        public class RotateComponentAuthoringBaker : Baker<RotateComponentAuthoring>
        {
            public override void Bake(RotateComponentAuthoring componentAuthoring)
            {
                Entity entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new RotateComponent
                {
                    Direction = new float3
                    {
                        x = math.radians(componentAuthoring.Direction.x),
                        y = math.radians(componentAuthoring.Direction.y),
                        z = math.radians(componentAuthoring.Direction.z)
                    }, 
                    Speed = componentAuthoring.Speed
                });
            }
        }
    }
}
