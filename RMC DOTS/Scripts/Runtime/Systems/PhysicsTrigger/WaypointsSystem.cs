using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Physics;
using Unity.Physics.Systems;
using UnityEngine;

namespace RMC.DOTS.Systems.PhysicsTrigger
{
    [BurstCompile]
    [UpdateInGroup(typeof(PhysicsSystemGroup))]
    [UpdateAfter(typeof(PhysicsSimulationGroup))]
    [UpdateBefore(typeof(ExportPhysicsWorld))]
    public partial struct WaypointsSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<PhysicsTriggerSystemAuthoring.PhysicsTriggerSystemIsEnabledTag>();
            state.RequireForUpdate<SimulationSingleton>();
            state.RequireForUpdate<EndSimulationEntityCommandBufferSystem.Singleton>();
        }
        
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var simSingleton = SystemAPI.GetSingleton<SimulationSingleton>();
            var ecbSingleton = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>();

            var job = new PickupTriggerJob
            {
                PhysicsTriggerComponentLookup = SystemAPI.GetComponentLookup<PhysicsTriggerComponent>(),
                PhysicsTriggerOutputTagLookup = SystemAPI.GetComponentLookup<PhysicsTriggerOutputComponent>(),
                ECB = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged),
                TimeFrameCount = UnityEngine.Time.frameCount
            };

            state.Dependency = job.Schedule(simSingleton, state.Dependency);
        }
    }
     
    [BurstCompile]
    public struct PickupTriggerJob : ITriggerEventsJob
    {
        [ReadOnly] public ComponentLookup<PhysicsTriggerComponent> PhysicsTriggerComponentLookup;
        [ReadOnly] public ComponentLookup<PhysicsTriggerOutputComponent> PhysicsTriggerOutputTagLookup;
        [ReadOnly] public int TimeFrameCount;
        public EntityCommandBuffer ECB; 
        
        [BurstCompile]
        public void Execute(TriggerEvent triggerEvent)
        {
            var entityA = triggerEvent.EntityA;
            var entityB = triggerEvent.EntityB;
                
            PhysicsTriggerComponentLookup.TryGetComponent(entityA, out PhysicsTriggerComponent physicsTriggerComponentA);
            PhysicsTriggerComponentLookup.TryGetComponent(entityB, out PhysicsTriggerComponent physicsTriggerComponentB);

            /*
             *
             *
             * This system works, but has bugs.
             *
             * I would expect if the latermasks are set to collide with each other, that the system would
             * run TWICE. Once for a hitting b and once for b hitting a.
             *
             * However, when I play with the masks to have ONLY a hit b, it is unpredictable.
             *
             *
             *
             * 
             */
            bool isCollidingAWithB = (physicsTriggerComponentA.CollidesWithLayerMask &
                                      physicsTriggerComponentB.MemberOfLayerMask) != 0;
            
            bool isCollidingBWithA = (physicsTriggerComponentB.CollidesWithLayerMask &
                                       physicsTriggerComponentA.MemberOfLayerMask) != 0;

            if (isCollidingAWithB)
            {
                if (!PhysicsTriggerOutputTagLookup.HasComponent(entityA))
                {
                    AddComponentForEnter(entityA, entityB);
                }
                else
                {
                    SetComponentForStay(entityA, entityB);;
                }
            }
            if (isCollidingAWithB)
            {
                if (!PhysicsTriggerOutputTagLookup.HasComponent(entityB))
                {
                    AddComponentForEnter(entityB, entityA);
                }
                else
                {
                    SetComponentForStay(entityB, entityA);
                }
            }
        }

        private void AddComponentForEnter(Entity theEntity, Entity theOtherEntity)
        { 
            // Handle enter 
            //Debug.Log($"PhysicsTriggerSystem theEntity=({theEntity.Index}) hitby theOtherEntity=({theOtherEntity.Index})");
            ECB.AddComponent<PhysicsTriggerOutputComponent>(theEntity,
                new PhysicsTriggerOutputComponent
                {
                    TheEntity = theEntity, 
                    TheOtherEntity = theOtherEntity, 
                    PhysicsTriggerType = PhysicsTriggerType.Enter, 
                    TimeFrameCountForLastCollision = TimeFrameCount
                });
        }
        
        private void SetComponentForStay(Entity theEntity, Entity theOtherEntity)
        {
            // Update to Stay if already in collision
            if (PhysicsTriggerOutputTagLookup.GetRefRO(theEntity).ValueRO.PhysicsTriggerType ==
                PhysicsTriggerType.Enter)
            {
                //Debug.Log($"PhysicsTriggerSystem ({theEntity.Index}) Set1 To Stay on TimeFrameCount: {TimeFrameCount}");
                ECB.SetComponent<PhysicsTriggerOutputComponent>(theEntity,
                    new PhysicsTriggerOutputComponent
                    {
                        TheEntity = theEntity,
                        TheOtherEntity = theOtherEntity,
                        PhysicsTriggerType = PhysicsTriggerType.Stay,
                        TimeFrameCountForLastCollision = TimeFrameCount
                    }); 
                
            }
            else if ( PhysicsTriggerOutputTagLookup.GetRefRO(theEntity).ValueRO.PhysicsTriggerType == PhysicsTriggerType.Stay)
            {
                //Debug.Log($"PhysicsTriggerSystem ({theEntity.Index}) Set2 To Stay on TimeFrameCount: {TimeFrameCount}");
                ECB.SetComponent<PhysicsTriggerOutputComponent>(theEntity,
                    new PhysicsTriggerOutputComponent
                    {
                        TheEntity = theEntity,
                        TheOtherEntity = theOtherEntity,
                        PhysicsTriggerType = PhysicsTriggerType.Stay,
                        TimeFrameCountForLastCollision = TimeFrameCount
                    });
            }
        }
    }
}