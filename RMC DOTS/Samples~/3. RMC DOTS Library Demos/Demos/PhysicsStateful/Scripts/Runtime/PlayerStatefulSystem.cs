using Unity.Collections;
using Unity.Entities;

//TODO: FixPhysics - do without query - test result
namespace Unity.Physics.PhysicsStateful
{
    [UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
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
                            PhysicsStatefulDebugSystem.LogEvent(ref state, entity, bufferIndex, collisionEvent);
                            break;
                        case StatefulEventState.Stay:
                            // Comment in/out to see the difference
                            //PhysicsStatefulDebugSystem.LogEvent(ref state, entity, bufferIndex, collisionEvent);
                            break;
                        case StatefulEventState.Exit:
                            // Comment in/out to see the difference
                            //PhysicsStatefulDebugSystem.LogEvent(ref state, entity, bufferIndex, collisionEvent);
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
                            //PhysicsStatefulDebugSystem.LogEvent(ref state, entity, bufferIndex, collisionEvent);
                            break;
                        case StatefulEventState.Stay:
                            // Comment in/out to see the difference
                            //PhysicsStatefulDebugSystem.LogEvent(ref state, entity, bufferIndex, collisionEvent);
                            break;
                        case StatefulEventState.Exit:
                            // Comment in/out to see the difference
                            //PhysicsStatefulDebugSystem.LogEvent(ref state, entity, bufferIndex, collisionEvent);
                            break;
                    }
                }
            }
            
        }
       
    }
}
