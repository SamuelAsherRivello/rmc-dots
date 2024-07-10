using RMC.DOTS.Systems.StateMachine;
using Unity.Entities;
using UnityEngine;

namespace RMC.DOTS.Demos.StateMachine.Light
{
    /// <summary>
    /// See <see cref="StateMachineSystemBase"/>
    ///
    /// There are multiple demos
    ///
    /// * <see cref="StateMachine01_Light"/> - More logic OUTSIDE the StateMachine itself
    /// * <see cref="StateMachine02_Full"/>  - More logic INSIDE the StateMachine itself
    /// 
    /// </summary>
    public class StateMachine01_Light : MonoBehaviour
    {
        //  Initialization --------------------------------
        protected void Start()
        {
            Debug.Log("StateMachine01_Light Demo. Watch the console.");
            
            //TODO: Not sure why, but...
            Debug.LogWarning("NOTE: Before playing, Open SubScene (via checkbox) in Scene Hierarchy Window.");
            
			SetupStateMachines();
		}

		//  Methods ----------------------------------------
		private void SetupStateMachines()
		{
			// Get StateMachine
			MyMovementStateMachineSystem stateMachine =
				World.DefaultGameObjectInjectionWorld.CreateSystemManaged<MyMovementStateMachineSystem>();

			// Set Initial State
			stateMachine.RequestStateChangeForAllEntities<MyMovementTranslationState>();
		}
	}
}