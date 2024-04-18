using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Physics;
using Unity.Physics.Systems;

namespace RMC.DOTS.Systems.PhysicsTrigger
{
    [UpdateInGroup(typeof(PhysicsSystemGroup))]
    [UpdateAfter(typeof(PhysicsSimulationGroup))]
    [UpdateBefore(typeof(ExportPhysicsWorld))]
    public partial struct PhysicsTriggerSystem : ISystem
    {
        [BurstCompile]
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
                PhysicsTriggerInput1TagLookup = SystemAPI.GetComponentLookup<PhysicsTriggerInput1Tag>(),
                PhysicsTriggerInput2TagLookup = SystemAPI.GetComponentLookup<PhysicsTriggerInput2Tag>(),
                PhysicsTriggerOutputTagLookup = SystemAPI.GetComponentLookup<PhysicsTriggerOutputTag>(),
                ECB = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged),
                TimeFrameCount = UnityEngine.Time.frameCount
            };

            state.Dependency = job.Schedule(simSingleton, state.Dependency);
        }
    }
    
    public struct PickupTriggerJob : ITriggerEventsJob
    {
        [ReadOnly] public ComponentLookup<PhysicsTriggerInput1Tag> PhysicsTriggerInput1TagLookup;
        [ReadOnly] public ComponentLookup<PhysicsTriggerInput2Tag> PhysicsTriggerInput2TagLookup;
        [ReadOnly] public ComponentLookup<PhysicsTriggerOutputTag> PhysicsTriggerOutputTagLookup;
        [ReadOnly] public int TimeFrameCount;
        
        public EntityCommandBuffer ECB;
        
        public void Execute(TriggerEvent triggerEvent)
        {
            var entityA = triggerEvent.EntityA;
            var entityB = triggerEvent.EntityB;
                
            bool isEntityAInput1 = PhysicsTriggerInput1TagLookup.HasComponent(entityA);
            bool isEntityAInput2 = PhysicsTriggerInput2TagLookup.HasComponent(entityA);
            
            bool isEntityBInput1 = PhysicsTriggerInput1TagLookup.HasComponent(entityB);
            bool isEntityBInput2 = PhysicsTriggerInput2TagLookup.HasComponent(entityB);


            //Detect a-b. Detect b-a. But do not detect a-a nor b-b
            if (isEntityAInput1 && isEntityBInput1)
            {
                return;
            }
            
            if (isEntityAInput2 && isEntityBInput2)
            {
                return;
            }
            
            if (!PhysicsTriggerOutputTagLookup.HasComponent(entityA))
            {
                AddComponentForEnter(entityA, entityB);
            }
           else
            {
                SetComponentForStay(entityA, entityB);;
            }
            
            if (!PhysicsTriggerOutputTagLookup.HasComponent(entityB))
            {
                AddComponentForEnter(entityB, entityA);
            }
            else
            {
                SetComponentForStay(entityB, entityA);
            }
        }

        private void AddComponentForEnter(Entity theEntity, Entity theOtherEntity)
        {
            // Handle enter 
            //Debug.Log($"PhysicsTriggerSystem ({theEntity.Index}) Set To Enter on TimeFrameCount: {TimeFrameCount}");
            ECB.AddComponent<PhysicsTriggerOutputTag>(theEntity,
                new PhysicsTriggerOutputTag
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
                ECB.SetComponent<PhysicsTriggerOutputTag>(theEntity,
                    new PhysicsTriggerOutputTag
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
                ECB.SetComponent<PhysicsTriggerOutputTag>(theEntity,
                    new PhysicsTriggerOutputTag
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