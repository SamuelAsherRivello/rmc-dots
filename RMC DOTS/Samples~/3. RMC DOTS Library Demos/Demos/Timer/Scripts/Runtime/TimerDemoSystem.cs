using Unity.Entities;
using UnityEngine;

namespace RMC.DOTS.Systems.Timer
{
    public partial struct TimerDemoSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<TimerDemoSystemAuthoring.TimerDemoSystemIsEnabledTag>();
            state.RequireForUpdate<TimerComponent>();
            
            // 1) START TIMER
            //Typically this is done within the OnUpdate of a production system
            
            // Half-second
            var entity1 = state.EntityManager.CreateEntity();
            state.EntityManager.AddComponentData(entity1, new TimerComponent(0.5f));
            
            // A bit longer...
            var entity2 = state.EntityManager.CreateEntity();
            state.EntityManager.AddComponentData(entity2, new TimerComponent(1.5f));
        }

        public void OnUpdate(ref SystemState state)
        {
            foreach (var timerComponent in 
                     SystemAPI.Query<
                         RefRO<TimerComponent>>())
            {
                // 2) CHECK TIMER
                if (timerComponent.ValueRO.IsTimerDone)
                {
                    Debug.Log($"TimerDemoSystem: Done after {timerComponent.ValueRO.TargetDurationInSeconds} seconds.");
                }
            }
        }
    }
}