using RMC.Audio.Data.Types;
using RMC.DOTS.Systems.Audio;
using RMC.DOTS.Systems.PhysicsVelocityImpulse;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Physics.PhysicsStateful;
using UnityEngine;

namespace RMC.DOTS.Toys.Fountain
{
    /// <summary>
    /// When a new <see cref="Entity"/> droplet has a
    /// <see cref="PhysicsVelocityImpulseComponent"/> on it,
    /// play a sound by adding a <see cref="AudioComponent"/>
    /// with a dynamic pitch value.
    /// </summary>
    [UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
    [UpdateAfter(typeof(StatefulTriggerEventSystem))] 
    public partial struct SoundFountainSystem : ISystem
    {
        private EntityQuery _statefulEntityQuery;
        
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<SoundFountainSystemAuthoring.SoundFountainSystemIsEnabledTag>();
            state.RequireForUpdate<PhysicsStatefulSystemAuthoring.CollisionSystemIsEnabledIsEnabledTag>();
            state.RequireForUpdate<PhysicsVelocityImpulseSystemAuthoring.PhysicsVelocityImpulseSystemIsEnabledTag>();
            state.RequireForUpdate<EndInitializationEntityCommandBufferSystem.Singleton>();
            
            _statefulEntityQuery = state.GetEntityQuery(
                typeof(StatefulCollisionEvent), typeof(PhysicsVelocity));
        }

        public void OnUpdate(ref SystemState state)
        {
            var ecb = SystemAPI.
                GetSingleton<EndInitializationEntityCommandBufferSystem.Singleton>().
                CreateCommandBuffer(state.WorldUnmanaged);
            
            
            // 1) Detect when something spawns, and play a sound
            foreach (var (physicsVelocity, physicsVelocityImpulseComponent, entity) in 
                     SystemAPI.Query<
                             RefRW<PhysicsVelocity>,
                         RefRO<PhysicsVelocityImpulseComponent>>().
                         WithEntityAccess())
            {
                //NOTE: A key concept is that this system runs AFTER the PhysicsVelocityImpulseSystem
                // So it can grab the following value which was JUST set.
                var pitch1 = 0.5f + math.abs(physicsVelocity.ValueRO.Linear.y) / 10;
                var volume1 = 0.1f;
                
                ecb.AddComponent<AudioComponent>(entity, 
                    new AudioComponent(
                        "Spawn01", 
                        volume1,
                        pitch1));
            }

            
            
            // 2) Detect when something bounces, and play a sound
            foreach (var (physicsVelocity, statefulEventBuffers, entity) in 
                     SystemAPI.Query<RefRO<PhysicsVelocity>, DynamicBuffer<StatefulCollisionEvent>>().
                         WithEntityAccess())
            {
                
                for (int bufferIndex = 0; bufferIndex < statefulEventBuffers.Length; bufferIndex++)
                {
                    var statefulEvent = statefulEventBuffers[bufferIndex];
                    if (statefulEvent.State == StatefulEventState.Enter)
                    {
                        //DO SOMETHING
                        var pitch2 = 0.5f + math.abs(physicsVelocity.ValueRO.Linear.y) / 10;
                        var volume2 = AudioConstants.VolumeDefault;
                            
                        ecb.AddComponent<AudioComponent>(entity, 
                            new AudioComponent(
                                "Bounce01", 
                                volume2,
                                pitch2));
                        break;
                    }
                }
            }
        }
    }
}