using Unity.Collections;
using Unity.Entities;
using UnityEngine;

namespace Unity.Physics.PhysicsStateful
{
    [UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
    [UpdateAfter(typeof(StatefulTriggerEventSystem))] 
    public partial struct PhysicsStatefulDebugSystem : ISystem
    {
        private EntityQuery _statefulTriggerEventEntityQuery;
        private EntityQuery _statefulCollisionEventEntityQuery;
        
        public void OnCreate(ref SystemState state)
        {
              
            // SamR
            state.RequireForUpdate<PhysicsStatefulSystemAuthoring.DebugSystemIsEnabledIsEnabledTag>();
            
            // Existing
            _statefulTriggerEventEntityQuery = state.GetEntityQuery(typeof(StatefulTriggerEvent));
            _statefulCollisionEventEntityQuery = state.GetEntityQuery(typeof(StatefulCollisionEvent));
        }
        

        public void OnUpdate(ref SystemState state)
        {
            
            foreach (var entity in _statefulTriggerEventEntityQuery.ToEntityArray(Allocator.Temp))
            {
                var buffer = state.EntityManager.GetBuffer<StatefulTriggerEvent>(entity);
                
                for (int i = 0; i < buffer.Length; i++) 
                {
                    var collisionEvent = buffer[i]; 

                    switch (collisionEvent.State) 
                    {
                        case StatefulEventState.Enter:
                            PhysicsStatefulDebugSystem.LogEvent(ref state, entity, i, collisionEvent);
                            break;
                        case StatefulEventState.Stay:
                            PhysicsStatefulDebugSystem.LogEvent(ref state, entity, i, collisionEvent);
                            break;
                        case StatefulEventState.Exit:
                            PhysicsStatefulDebugSystem.LogEvent(ref state, entity, i, collisionEvent);
                            break;
                    }
                }
            }

            
            foreach (var entity in _statefulCollisionEventEntityQuery.ToEntityArray(Allocator.Temp))
            {
                var buffer = state.EntityManager.GetBuffer<StatefulCollisionEvent>(entity);
                
                for (int i = 0; i < buffer.Length; i++)
                {
                    var collisionEvent = buffer[i];

                    switch (collisionEvent.State) 
                    {  
                        case StatefulEventState.Enter:
                            PhysicsStatefulDebugSystem.LogEvent(ref state, entity, i, collisionEvent);
                            break;
                        case StatefulEventState.Stay:
                            PhysicsStatefulDebugSystem.LogEvent(ref state, entity, i, collisionEvent);
                            break;
                        case StatefulEventState.Exit:
                            PhysicsStatefulDebugSystem.LogEvent(ref state, entity, i, collisionEvent);
                            break;
                    }
                }
            }
            
        }

        public static void LogEvent(ref SystemState state, Entity entity, int bufferIndex, 
            StatefulTriggerEvent collisionEvent)
        {
            LogEvent(
                collisionEvent.GetType().Name, 
                entity, 
                bufferIndex, 
                Time.frameCount, 
                state.EntityManager.GetName(collisionEvent.EntityA), 
                state.EntityManager.GetName(collisionEvent.EntityB), 
                collisionEvent.State.ToString()
            );
        }

        public static void LogEvent(ref SystemState state, Entity entity, int bufferIndex,
            StatefulCollisionEvent collisionEvent)
        {
            LogEvent(
                collisionEvent.GetType().Name, 
                entity, 
                bufferIndex, 
                Time.frameCount, 
                state.EntityManager.GetName(collisionEvent.EntityA), 
                state.EntityManager.GetName(collisionEvent.EntityB), 
                collisionEvent.State.ToString()
            );
        }
        
        private static void LogEvent(string eventName, Entity entity, 
            int bufferIndex, int frameCount, string entityAName, string entityBName, string stateName)
        {
            Debug.Log($"{eventName}() = {stateName} On " +
                      $"'{entityAName}' with '{entityBName}'." +
                      $"  More...\n\n [BufferIndex = {bufferIndex}, Entity = {entity}, FrameCount = {frameCount}]" +
                      $"\n\n"
            );
        }
    }
}
