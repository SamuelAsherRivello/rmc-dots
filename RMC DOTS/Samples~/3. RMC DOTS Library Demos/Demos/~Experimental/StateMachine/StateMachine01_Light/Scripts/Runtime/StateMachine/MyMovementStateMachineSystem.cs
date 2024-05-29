using RMC.DOTS.Systems.StateMachine;

namespace RMC.DOTS.Demos.StateMachine.Light
{
    partial class MyMovementStateMachineSystem : StateMachineSystem<MyMovementStateSystemTag>
    {
        protected override void OnCreate()
        {
            base.OnCreate();
            
            //Requires
            RequireForUpdate<RotationComponent>();
            RequireForUpdate<TranslationComponent>();
            
            //Registers
            RegisterState<MyMovementTranslationState>();
            RegisterState<MyMovementRotationState>();
        }
    }
}