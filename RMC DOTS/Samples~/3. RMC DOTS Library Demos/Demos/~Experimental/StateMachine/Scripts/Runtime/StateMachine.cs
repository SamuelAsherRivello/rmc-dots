using RMC.DOTS.Systems.StateMachine;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

namespace RMC.DOTS.Demos.StateMachine
{
    /// <summary>
    /// See <see cref="StateMachineSystemBase"/>
    /// </summary>
    public class StateMachine : MonoBehaviour
    {
        //  Initialization --------------------------------
        protected void Start()
        {
            Debug.Log("StateMachine Demo. Watch the console.");

            MyMovementStateMachineSystem myMovementStateMachineSystem = 
                World.DefaultGameObjectInjectionWorld.CreateSystemManaged<MyMovementStateMachineSystem>();
                
            // TODO
            // 1. remove the need for the Test01 component. Find another way to set the **FIRST** state
            // 2. Rethink the need for "RotationComponent". Does it make sense, or just move that DATA into the related state?
            EntityQuery query = new EntityQueryBuilder(Allocator.Temp)
                .WithAllRW<MyMovementEntityTag>()
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