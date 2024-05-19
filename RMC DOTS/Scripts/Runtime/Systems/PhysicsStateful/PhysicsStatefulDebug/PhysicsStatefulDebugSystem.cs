using RMC.DOTS.Systems.FrameCount;
using Unity.Collections;
using Unity.Entities;
using Unity.Physics.Systems;
using UnityEngine;

namespace Unity.Physics.PhysicsStateful
{
    [UpdateInGroup(typeof(PhysicsSystemGroup))]
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
                            DebugStatefulTriggerEvent(ref state, entity, i, collisionEvent);
                            break;
                        case StatefulEventState.Stay:
                            DebugStatefulTriggerEvent(ref state, entity, i, collisionEvent);
                            break;
                        case StatefulEventState.Exit:
                            DebugStatefulTriggerEvent(ref state, entity, i, collisionEvent);
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
                            DebugStatefulCollisionEvent(ref state, entity, i, collisionEvent);
                            break;
                        case StatefulEventState.Stay:
                            DebugStatefulCollisionEvent(ref state, entity, i, collisionEvent);
                            break;
                        case StatefulEventState.Exit:
                            DebugStatefulCollisionEvent(ref state, entity, i, collisionEvent);
                            break;
                    }
                }
            }
            
        }

        private void DebugStatefulTriggerEvent(ref SystemState state, Entity entity, int i, 
            StatefulTriggerEvent collisionEvent)
        {
            Debug.Log($"({collisionEvent.GetType().Name}) E: {entity}, i : {i}, f : {Time.frameCount}, " +
                      $"A : {state.EntityManager.GetName(collisionEvent.EntityA)}, " +
                      $"B : {state.EntityManager.GetName(collisionEvent.EntityB)}, " +
                      $"S : {collisionEvent.State}");
        }

        private void DebugStatefulCollisionEvent(ref SystemState state, Entity entity, int i,
            StatefulCollisionEvent collisionEvent)
        {
            Debug.Log($"({collisionEvent.GetType().Name}) E: {entity}, i : {i}, f : {Time.frameCount}, " +
                      $"A : {state.EntityManager.GetName(collisionEvent.EntityA)}, " +
                      $"B : {state.EntityManager.GetName(collisionEvent.EntityB)}, " +
                      $"S : {collisionEvent.State}");
        }
    }
}
