using RMC.DOTS.SystemGroups;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace RMC.DOTS.Systems.Tween
{
    public struct TweenScaleHasStartedTag : IComponentData {}
    public struct TweenScaleHasFinishedTag : IComponentData {}
    
    [UpdateInGroup(typeof(PauseablePresentationSystemGroup))]
    [BurstCompile]
    public partial struct TweenScaleSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<TweenScaleSystemAuthoring.TweenScaleSystemIsEnabledTag>();
            state.RequireForUpdate<BeginSimulationEntityCommandBufferSystem.Singleton>();
            state.RequireForUpdate<TweenScaleComponent>();
            state.RequireForUpdate<LocalTransform>();
        }
		
        public void OnUpdate(ref SystemState state)
        {
            var ecb = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>().
                CreateCommandBuffer(state.WorldUnmanaged);

            var deltaTime = SystemAPI.Time.DeltaTime;
      
            
            //TODO: Try to use "LitMotion" package (already imported) instead of this
            
            // Set to start scale
            foreach (var (localTransform, tweenScaleComponent, entity) in
                     SystemAPI.Query<RefRW<LocalTransform>, RefRO<TweenScaleComponent>>().
                         WithNone<TweenScaleHasStartedTag>().
                         WithEntityAccess())
            {

                localTransform.ValueRW.Scale = tweenScaleComponent.ValueRO.From;
                
                // Add tag so we only do this once
                ecb.AddComponent<TweenScaleHasStartedTag>(entity);
            }
            
            
            // Scale to end scale
            foreach (var (localTransform, teenScaleComponent, entity) in
                     SystemAPI.Query<RefRW<LocalTransform>, RefRW<TweenScaleComponent>>().
                         WithNone<TweenScaleHasFinishedTag>().
                         WithAll<TweenScaleHasStartedTag>().
                         WithEntityAccess())
            {
                teenScaleComponent.ValueRW._ElapsedTimeInSeconds += deltaTime;
                var percentage = teenScaleComponent.ValueRO._ElapsedTimeInSeconds / 
                                 teenScaleComponent.ValueRO.DurationInSeconds;
                
                localTransform.ValueRW.Scale = 
                    math.lerp(localTransform.ValueRW.Scale, 
                        teenScaleComponent.ValueRO.To, 
                        percentage);
                
                if (percentage >= 1f)
                {
                    // Add at most ONE thing
                    localTransform.ValueRW.Scale = teenScaleComponent.ValueRO.To;
                    ecb.AddComponent<TweenScaleHasFinishedTag>(entity);
                }
            }
        }
    }
}