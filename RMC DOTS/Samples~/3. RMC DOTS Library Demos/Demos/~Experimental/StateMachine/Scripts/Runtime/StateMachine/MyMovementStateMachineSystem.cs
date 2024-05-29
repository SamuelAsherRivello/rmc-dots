using RMC.DOTS.Systems.StateMachine;
using Unity.Entities;

namespace RMC.DOTS.Demos.StateMachine
{
    struct MyMovementStateSystemTag : IComponentData{}

    partial class MyMovementStateMachineSystem : StateMachineSystem<MyMovementStateSystemTag>
    {
        protected override void OnCreate()
        {
            base.OnCreate();

            RegisterState<MyMovementTranslationState>();
            RegisterState<MyMovementRotationState>();
        }
    }
}