using RMC.DOTS.SystemGroups;
using Unity.Burst;
using Unity.Entities;

namespace RMC.DOTS.Systems.Timer
{
    [UpdateInGroup(typeof(UnpauseablePresentationSystemGroup))]
    [RequireMatchingQueriesForUpdate]
    [BurstCompile]
    public partial struct TimerSystem : ISystem 
    {
        
        
        /// <summary>
        /// We set the TimeScale as virtual so that subclasses can have their own timescales if needed.
        /// A use case for this is for faster speeds or faster enemies in strategy games.
        /// </summary>
        // protected virtual float TimeScale
        // {
        //     get
        //     {
        //         return 1;
        //     }
        // }
        //
        // /// <summary>
        // /// This is the scaled time - applying the Timescale to the delta time.
        // /// </summary>
        // private float ScaledDeltaTime
        // {
        //     get { return SystemAPI.Time.DeltaTime * TimeScale; }
        // }

        // private EndSimulationEntityCommandBufferSystem _ecbSystem;
        //
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<EndInitializationEntityCommandBufferSystem.Singleton>();
            state.RequireForUpdate<TimerSystemAuthoring.TimerSystemIsEnabledTag>();
            state.RequireForUpdate<TimerComponent>();
        }
        
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var ecb = SystemAPI.
                GetSingleton<EndInitializationEntityCommandBufferSystem.Singleton>().
                CreateCommandBuffer(state.WorldUnmanaged);
            
            var deltaTime = SystemAPI.Time.DeltaTime;
            
            foreach (var (timerComponent, entity) in 
                     SystemAPI.Query<
                         RefRW<TimerComponent>>().WithEntityAccess())
            {
                
                bool wasDone = timerComponent.ValueRW.IsTimerDone;
                timerComponent.ValueRW.UpdateTimer(deltaTime);
                bool isDone = timerComponent.ValueRW.IsTimerDone;
                
                //Capture the FIRST frame when the timer is met and DestroyEntity
                if (wasDone && isDone)
                {
                   ecb.DestroyEntity(entity);
                }
            }
        }
    }
}
