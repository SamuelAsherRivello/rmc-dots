﻿using RMC.DOTS.Systems.Player;
using Unity.Entities;
using Unity.Physics.PhysicsStateful;
using UnityEngine;

namespace RMC.DOTS.Samples.Templates.DOTSGameTemplate
{
    [UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
    [UpdateAfter(typeof(StatefulTriggerEventSystem))] 
    public partial struct GoalWasReachedSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<GoalWasReachedSystemAuthoring.GoalWasReachedSystemIsEnabledTag>();
            state.RequireForUpdate<BeginPresentationEntityCommandBufferSystem.Singleton>();
            state.RequireForUpdate<PlayerTag>();
        }

        public void OnUpdate(ref SystemState state)
        {
            var ecb = SystemAPI.
                GetSingleton<BeginPresentationEntityCommandBufferSystem.Singleton>().
                CreateCommandBuffer(state.WorldUnmanaged);
            
            foreach (var (statefulTriggerEventBuffers, entity) in 
                     SystemAPI.Query<DynamicBuffer<StatefulTriggerEvent>>().
                         WithAll<PlayerTag>().
                         WithNone<GoalWasReachedExecuteOnceTag>().
                         WithEntityAccess())
            {

                
                for (int bufferIndex = 0; bufferIndex < statefulTriggerEventBuffers.Length; bufferIndex++)
                {
                    var collisionEvent = statefulTriggerEventBuffers[bufferIndex];
                    if (collisionEvent.State == StatefulEventState.Enter)
                    {
                        ecb.AddComponent<GoalWasReachedExecuteOnceTag>(entity);
                        break;
                    }
                }
            }
        }
    }
}