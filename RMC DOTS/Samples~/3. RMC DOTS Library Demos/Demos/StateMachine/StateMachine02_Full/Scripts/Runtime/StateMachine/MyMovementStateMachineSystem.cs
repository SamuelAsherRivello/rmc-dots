using RMC.DOTS.Systems.StateMachine;
using System;

namespace RMC.DOTS.Demos.StateMachine.Full
{
    
    ///////////////////////////////////////////////////
    // StateMachine Comment #02: Note that the 
    // StateMachineSystem<T> relates to ONE component
    ///////////////////////////////////////////////////
    partial class MyMovementStateMachineSystem : StateMachineSystem<MyMovementDataComponent>
    {

        protected override void OnCreate()
        {
            base.OnCreate();
            
            // Any RequireForUpdate calls...
            
            
            // Any RegisterState calls...
            
            ///////////////////////////////////////////////////
            // StateMachine Comment #03: Register all states
            ///////////////////////////////////////////////////
            RegisterState<MyMovementTranslationState>();
            RegisterState<MyMovementRotationState>();
        }

	}
}