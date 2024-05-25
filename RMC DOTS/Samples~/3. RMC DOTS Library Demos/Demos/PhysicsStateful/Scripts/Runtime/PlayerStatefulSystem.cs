using Unity.Entities;

namespace Unity.Physics.PhysicsStateful
{
    [UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
    [UpdateAfter(typeof(StatefulTriggerEventSystem))] 
    public partial struct PlayerStatefulSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<PlayerStatefulSystemAuthoring.PlayerStatefulSystemAuthoringIsEnabledTag>();
        }
        

        public void OnUpdate(ref SystemState state)
        {
            
            //TRIGGER
            foreach (var (statefulEventBuffers, entity) in 
                     SystemAPI.Query<DynamicBuffer<StatefulTriggerEvent>>().
                         WithEntityAccess())
            {

                for (int bufferIndex = 0; bufferIndex < statefulEventBuffers.Length; bufferIndex++)
                {
                    var statefulEvent = statefulEventBuffers[bufferIndex];

                    switch (statefulEvent.State) 
                    {
                        case StatefulEventState.Enter:
                            // Comment in/out to see the difference
                            PhysicsStatefulDebugSystem.LogEvent(ref state, entity, bufferIndex, statefulEvent);
                            break;
                        case StatefulEventState.Stay:
                            // Comment in/out to see the difference
                            //PhysicsStatefulDebugSystem.LogEvent(ref state, entity, bufferIndex, statefulEvent);
                            break;
                        case StatefulEventState.Exit:
                            // Comment in/out to see the difference
                            //PhysicsStatefulDebugSystem.LogEvent(ref state, entity, bufferIndex, statefulEvent);
                            break;
                    }
                }
            }
            
            
            //COLLISION
            foreach (var (statefulEventBuffers, entity) in 
                     SystemAPI.Query<DynamicBuffer<StatefulCollisionEvent>>().
                         WithEntityAccess())
            {

                for (int bufferIndex = 0; bufferIndex < statefulEventBuffers.Length; bufferIndex++)
                {
                    var statefulEvent = statefulEventBuffers[bufferIndex];

                    switch (statefulEvent.State) 
                    {
                        case StatefulEventState.Enter:
                            // Comment in/out to see the difference
                            //PhysicsStatefulDebugSystem.LogEvent(ref state, entity, bufferIndex, statefulEvent);
                            break;
                        case StatefulEventState.Stay:
                            // Comment in/out to see the difference
                            //PhysicsStatefulDebugSystem.LogEvent(ref state, entity, bufferIndex, statefulEvent);
                            break;
                        case StatefulEventState.Exit:
                            // Comment in/out to see the difference
                            // PhysicsStatefulDebugSystem.LogEvent(ref state, entity, bufferIndex, statefulEvent);
                            break;
                    }
                }
            }
        }
    }
}
