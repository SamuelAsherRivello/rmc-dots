using Unity.Entities;
using Unity.Physics.Systems;
using UnityEngine;

namespace RMC.DOTS.Systems.PhysicsTrigger
{
    [UpdateInGroup(typeof(PhysicsSystemGroup))]
    [UpdateAfter(typeof(PhysicsSimulationGroup))]
    [UpdateBefore(typeof(ExportPhysicsWorld))]
    public partial class PhysicsTriggerSystemCleanup : SystemBase
    {
        private EndSimulationEntityCommandBufferSystem _ecbSystem;

        protected override void OnCreate()
        {
            // Get or create the EndSimulationEntityCommandBufferSystem instance
            _ecbSystem = World.GetExistingSystemManaged<EndSimulationEntityCommandBufferSystem>();
        }

        protected override void OnUpdate()
        {
            // Create a command buffer from the ECB system, which is specifically designed to be executed later
            var commandBuffer = _ecbSystem.CreateCommandBuffer();
            var physicsTriggerOutputTagLookup = GetComponentLookup<PhysicsTriggerOutputTag>();
            int timeFrameCount = UnityEngine.Time.frameCount;

            int framesToWait = 10; //TODO: why not lower it to '1'? Theoretically that's good, but it triggers too fast
            
            Entities.WithAll<PhysicsTriggerOutputTag>().ForEach((Entity entity) =>
            {
                var physicsTriggerOutputTag = physicsTriggerOutputTagLookup.GetRefRO(entity);
                if (physicsTriggerOutputTag.ValueRO.PhysicsTriggerType == PhysicsTriggerType.Stay && 
                    physicsTriggerOutputTag.ValueRO.TimeFrameCountForLastCollision < timeFrameCount - framesToWait)
                {
                    //Debug.Log($"PhysicsTriggerSystem ({entity.Index}) Set To Exit on TimeFrameCount: {timeFrameCount} and last was {physicsTriggerOutputTag.ValueRO.TimeFrameCountForLastCollision}");
                    commandBuffer.SetComponent<PhysicsTriggerOutputTag>(entity, new PhysicsTriggerOutputTag
                    {
                        TheEntity = physicsTriggerOutputTag.ValueRO.TheEntity,
                        TheOtherEntity = physicsTriggerOutputTag.ValueRO.TheOtherEntity,
                        PhysicsTriggerType = PhysicsTriggerType.Exit,
                        TimeFrameCountForLastCollision = physicsTriggerOutputTag.ValueRO.TimeFrameCountForLastCollision
                    });
                    commandBuffer.RemoveComponent<PhysicsTriggerOutputTag>(entity);
                }

            }).Schedule(); // Schedule this job instead of running it immediately

            // Inform the ECB system that this command buffer will be producing jobs that it needs to wait for
            _ecbSystem.AddJobHandleForProducer(Dependency);
        }
    }
}