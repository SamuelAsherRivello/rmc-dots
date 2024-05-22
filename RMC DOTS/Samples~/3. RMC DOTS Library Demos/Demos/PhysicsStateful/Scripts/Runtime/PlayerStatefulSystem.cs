using Unity.Collections;
using Unity.Entities;
using Unity.Physics.Systems;
using UnityEngine;

namespace Unity.Physics.PhysicsStateful
{
    [UpdateInGroup(typeof(PhysicsSystemGroup))]
    [UpdateAfter(typeof(StatefulTriggerEventSystem))]
    public partial struct PlayerStatefulSystem : ISystem
    {
        private EntityQuery _statefulTriggerEventEntityQuery;
        private EntityQuery _statefulCollisionEventEntityQuery;
        
        public void OnCreate(ref SystemState state)
        {
              
            // SamR
            state.RequireForUpdate<PlayerStatefulSystemAuthoring.PlayerStatefulSystemAuthoringIsEnabledTag>();
            
            // Existing
            _statefulTriggerEventEntityQuery = state.GetEntityQuery(typeof(StatefulTriggerEvent));
            _statefulCollisionEventEntityQuery = state.GetEntityQuery(typeof(StatefulCollisionEvent));
        }
        

        public void OnUpdate(ref SystemState state)
        {
            
            foreach (var entity in _statefulTriggerEventEntityQuery.ToEntityArray(Allocator.Temp))
            {
                var buffer = state.EntityManager.GetBuffer<StatefulTriggerEvent>(entity);
                
                for (int bufferIndex = 0; bufferIndex < buffer.Length; bufferIndex++) 
                {
                    var collisionEvent = buffer[bufferIndex]; 

                    switch (collisionEvent.State) 
                    {
                        case StatefulEventState.Enter:
                            // Comment in/out to see the difference
                            LogStatefulTriggerEvent(ref state, entity, bufferIndex, collisionEvent);
                            break;
                        case StatefulEventState.Stay:
                            // Comment in/out to see the difference
                            //LogTrigger(ref state, entity, i, collisionEvent);
                            break;
                        case StatefulEventState.Exit:
                            // Comment in/out to see the difference
                            LogStatefulTriggerEvent(ref state, entity, bufferIndex, collisionEvent);
                            break;
                    }
                }
            }

            foreach (var entity in _statefulCollisionEventEntityQuery.ToEntityArray(Allocator.Temp))
            {
                var buffer = state.EntityManager.GetBuffer<StatefulCollisionEvent>(entity);
                
                for (int bufferIndex = 0; bufferIndex < buffer.Length; bufferIndex++)
                {
                    var collisionEvent = buffer[bufferIndex];

                    switch (collisionEvent.State) 
                    {  
                        case StatefulEventState.Enter:
                            // Comment in/out to see the difference
                            //LogStatefulCollisionEvent(ref state, entity, i, collisionEvent);
                            break;
                        case StatefulEventState.Stay:
                            // Comment in/out to see the difference
                            //LogStatefulCollisionEvent(ref state, entity, i, collisionEvent);
                            break;
                        case StatefulEventState.Exit:
                            // Comment in/out to see the difference
                            //LogStatefulCollisionEvent(ref state, entity, i, collisionEvent);
                            break;
                    }
                }
            }
            
        }
        private void LogStatefulTriggerEvent(ref SystemState state, Entity entity, int bufferIndex, 
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

        private void LogStatefulCollisionEvent(ref SystemState state, Entity entity, int bufferIndex,
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
        
        private void LogEvent(string eventName, Entity entity, 
            int bufferIndex, int frameCount, string entityAName, string entityBName, string stateName)
        {
            Debug.Log($"{eventName}.{stateName}" +
                      $"\t'{entityAName}' with '{entityBName}'" +
                      $"\t More Info...\n\n [e : {entity}, i : {bufferIndex}, f : {frameCount}]" +
                      $"\n\n"
                      );
        }
    }
}
