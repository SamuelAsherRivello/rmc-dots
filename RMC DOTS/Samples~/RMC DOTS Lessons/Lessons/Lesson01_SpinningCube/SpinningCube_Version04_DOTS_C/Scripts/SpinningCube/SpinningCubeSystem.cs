using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace RMC.DOTS.Lessons.SpinningCube.SpinningCube_Version04_DOTS_C
{
    //  System  ------------------------------------
    [UpdateInGroup(typeof(PresentationSystemGroup))]
    public partial class SpinningCubeSystem : SystemBase
    {
        
        protected override void OnCreate()
        {
            RequireForUpdate<LocalTransform>();
            RequireForUpdate<SpinningCubeComponent>();
        }

        
        protected override void OnUpdate()
        {
            Entities.
                ForEach((ref LocalTransform localTransform, in SpinningCubeComponent spinningCubeComponent) =>
                {
                    var from = localTransform.Rotation;
                    var delta = quaternion.Euler(spinningCubeComponent.RotationDelta * SystemAPI.Time.DeltaTime);
                    localTransform.Rotation = math.mul(from, delta);
                })
                .Schedule();
        }
    }
}