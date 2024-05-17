using Unity.Entities;
using UnityEngine;

namespace RMC.DOTS.Demos.SpawnGrid
{
    public class GridElementComponentAuthoring : MonoBehaviour
    {
        public float Amplitude = 10;
        public float Speed = 10;
        
        public class SpawnGridComponentAuthoringBaker : Baker<GridElementComponentAuthoring>
        {
            public override void Bake(GridElementComponentAuthoring authoring)
            {
                Entity entity = GetEntity(TransformUsageFlags.Dynamic);

                AddComponent<GridElementMoveComponent>(entity, new GridElementMoveComponent
                {
                    Amplitude = authoring.Amplitude,
                    Speed = authoring.Speed
                });

            }
        }
    }
}
