using Unity.Entities;
using UnityEngine;

namespace RMC.DOTS.Lessons.SpinningCube.SpinningCube_Version02_DOTS
{
    public class SpinningCubeComponentAuthoring : MonoBehaviour
    {
        public Vector3 RotationDelta;

        public class SpinningCubeComponentAuthoringBaker : Baker<SpinningCubeComponentAuthoring>
        {
            public override void Bake(SpinningCubeComponentAuthoring authoring)
            {
                
                //  Entity  ------------------------------------
                var entity = GetEntity(TransformUsageFlags.Dynamic);

                
                AddComponent(entity, new SpinningCubeComponent
                {
                    RotationDelta = authoring.RotationDelta
                });
            }
        }
    }
}