using RMC.DOTS.Systems.FrameCount;
using Unity.Collections;
using Unity.Entities;
using Unity.Physics.Stateful;
using Unity.Transforms;
using UnityEngine;

namespace Events
{
    [UpdateBefore(typeof(TransformSystemGroup))]
    public partial struct BasicDetectionSystem : ISystem
    {
        private EntityQuery _statefulTriggerEventEntityQuery;
        private EntityQuery _statefulCollisionEventEntityQuery;
        
        public void OnCreate(ref SystemState state)
        {
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
                            DebugStatefulTriggerEvent(state, entity, i, collisionEvent);
                            break;
                        case StatefulEventState.Stay:
                            break;
                        case StatefulEventState.Exit:
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
                            DebugStatefulCollisionEvent(state, entity, i, collisionEvent);
                            break;
                        case StatefulEventState.Stay:
                            break;
                        case StatefulEventState.Exit:
                            break;
                    }
                }
            }
            
        }

        private void DebugStatefulTriggerEvent(SystemState state, Entity entity, int i, 
            StatefulTriggerEvent collisionEvent)
        {
            Debug.Log($"({collisionEvent.GetType().Name}) E: {entity}, i : {i}, f : {FrameCountSystem.FrameCount}, " +
                      $"A : {state.EntityManager.GetName(collisionEvent.EntityA)}, " +
                      $"B : {state.EntityManager.GetName(collisionEvent.EntityB)}, " +
                      $"S : {collisionEvent.State}");
        }

        private void DebugStatefulCollisionEvent(SystemState state, Entity entity, int i,
            StatefulCollisionEvent collisionEvent)
        {
            Debug.Log($"({collisionEvent.GetType().Name}) E: {entity}, i : {i}, f : {FrameCountSystem.FrameCount}, " +
                      $"A : {state.EntityManager.GetName(collisionEvent.EntityA)}, " +
                      $"B : {state.EntityManager.GetName(collisionEvent.EntityB)}, " +
                      $"S : {collisionEvent.State}");
        }
    }
}
