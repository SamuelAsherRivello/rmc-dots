using RMC.DOTS.Systems.StateMachine;
using System;

namespace RMC.DOTS.Demos.StateMachine.Full
{
    partial class MyMovementStateMachineSystem : StateMachineSystem<MyMovementDataComponent>
    {
        protected override void OnCreate()
        {
            base.OnCreate();
            
            //Requires
            
            //Registers
            RegisterState<MyMovementTranslationState>();
            RegisterState<MyMovementRotationState>();
        }

	}
}