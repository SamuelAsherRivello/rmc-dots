using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Physics.Systems;
using Unity.Transforms;

namespace RMC.DOTS.Samples.Pong2D.Pong2D_Version02_DOTS
{
    [UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
    [UpdateBefore(typeof(PhysicsSystemGroup))]
    public partial struct PaddleCPUMoveSystem : ISystem
    {
        private ComponentLookup<LocalTransform> _localTransformLookup;
        
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<ProjectileTag>();
            state.RequireForUpdate<PaddleCPUTag>();
            _localTransformLookup = state.GetComponentLookup<LocalTransform>();
        }

        public void OnUpdate(ref SystemState state)
        {
            float deltaTime = SystemAPI.Time.DeltaTime;
            _localTransformLookup.Update(ref state);
            float closestDistance = float.MaxValue;
            
            // Query for all projectiles
            var projectileTagEntities = state.EntityManager.CreateEntityQuery(
                ComponentType.ReadOnly<LocalTransform>(),
                ComponentType.ReadOnly<ProjectileTag>()).ToEntityArray(Allocator.Temp);

            // Loop through all cpuPaddles
            foreach (var (velocity, cpuLocalTransform, paddleMoveComponent) in 
                     SystemAPI.Query<RefRW<PhysicsVelocity>, LocalTransform, PaddleMoveComponent>().WithAll<PaddleCPUTag>())
            {
                // Find closests projectile
                LocalTransform closestLocalTransform = new LocalTransform();
                foreach (var projectileTagEntity in projectileTagEntities)
                {
                    var newDistance = math.distance(_localTransformLookup[projectileTagEntity].Position, cpuLocalTransform.Position);
                    if (newDistance < closestDistance )
                    {
                        closestLocalTransform = _localTransformLookup[projectileTagEntity];
                    }
                }
                
                // Move towards closest projectile
                var deltaY = closestLocalTransform.Position.y - cpuLocalTransform.Position.y;
                float currentMoveInput = deltaY * paddleMoveComponent.Value * deltaTime;
                velocity.ValueRW.Linear.y = velocity.ValueRW.Linear.x + currentMoveInput;
            }
        }
    }
}