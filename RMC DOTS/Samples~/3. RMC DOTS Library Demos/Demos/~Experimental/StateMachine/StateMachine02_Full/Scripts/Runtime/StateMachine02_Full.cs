using RMC.DOTS.Systems.StateMachine;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

namespace RMC.DOTS.Demos.StateMachine.Full
{
    /// <summary>
    /// See <see cref="StateMachineSystemBase"/>
    ///
    /// There are multiple demos
    ///
    /// * <see cref="StateMachine01_Light"/> - More logic outside the StateMachine itself
    /// * <see cref="StateMachine02_Full"/>  - More logic inside the StateMachine itself
    /// 
    /// </summary>
    public class StateMachine02_Full : MonoBehaviour
    {
        //  Initialization --------------------------------
        protected void Start()
        {
            Debug.Log("StateMachine02_Full Demo. Watch the console.");

            MyMovementStateMachineSystem myMovementStateMachineSystem = 
                World.DefaultGameObjectInjectionWorld.CreateSystemManaged<MyMovementStateMachineSystem>();
                
            // TODO
            // 1. remove the need for the Test01 component. Find another way to set the **FIRST** state
            // 2. Rethink the need for "RotationComponent". Does it make sense, or just move that DATA into the related state?
            EntityQuery query = new EntityQueryBuilder(Allocator.Temp)
                .WithAllRW<MyMovementDataComponent>()
                .Build(World.DefaultGameObjectInjectionWorld.EntityManager);

            using (query)
            {
                var entities = query.ToEntityArray(Allocator.Temp);
                foreach (Entity entity in entities)
                {
                    myMovementStateMachineSystem.
                        RequestStateChange<MyMovementTranslationState>(entity);
                }
            }

        }
    }
}