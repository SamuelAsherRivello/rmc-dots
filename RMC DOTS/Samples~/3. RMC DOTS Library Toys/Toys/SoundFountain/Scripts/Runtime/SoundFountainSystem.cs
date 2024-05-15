using RMC.Audio.Data.Types;
using RMC.DOTS.Systems.Audio;
using RMC.DOTS.Systems.PhysicsTrigger;
using RMC.DOTS.Systems.PhysicsVelocityImpulse;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using UnityEngine;

namespace RMC.DOTS.Toys.Fountain
{
    /// <summary>
    /// Detect trigger, EXACTLY ONE TIME
    /// </summary>
    public struct PhysicsTriggerExecuteOnceTag : IComponentData {}
    
    /// <summary>
    /// When a new <see cref="Entity"/> droplet has a
    /// <see cref="PhysicsVelocityImpulseComponent"/> on it,
    /// play a sound by adding a <see cref="AudioComponent"/>
    /// with a dynamic pitch value.
    /// </summary>
    [UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
    [UpdateAfter(typeof(PhysicsVelocityImpulseSystem))] 
    public partial struct SoundFountainSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<PhysicsVelocityImpulseSystemAuthoring.PhysicsVelocityImpulseSystemIsEnabledTag>();
            state.RequireForUpdate<PhysicsTriggerSystemAuthoring.PhysicsTriggerSystemIsEnabledTag>();
            state.RequireForUpdate<EndInitializationEntityCommandBufferSystem.Singleton>();
            
            //These are OPTIONAL...
            //NOTE: Don't require PhysicsVelocityImpulseComponent
            //NOTE: Don't require PhysicsTriggerOutputComponent

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
            foreach (var (physicsVelocity, physicsTriggerOutputComponent, entity) in 
                     SystemAPI.Query<
                             RefRW<PhysicsVelocity>,
                             RefRO<PhysicsTriggerOutputComponent>>().
                         WithNone<PhysicsTriggerExecuteOnceTag>().
                         WithEntityAccess())
            {
                if (physicsTriggerOutputComponent.ValueRO.PhysicsTriggerType != PhysicsTriggerType.Enter ||
                    physicsTriggerOutputComponent.ValueRO.TimeFrameCountForLastCollision == Time.frameCount)
                {
                    var pitch2 = 0.5f + math.abs(physicsVelocity.ValueRO.Linear.y) / 10;
                    var volume2 = AudioConstants.VolumeDefault;
                    
                    ecb.AddComponent<AudioComponent>(entity, 
                        new AudioComponent(
                            "Bounce01", 
                            volume2,
                            pitch2));

                    //When the trigger rolls around on the floor eventually, it makes too many 'new' hits...
                    //So only one sound EVER per droplet will be heard now
                    ecb.AddComponent<PhysicsTriggerExecuteOnceTag>(entity);
                }
       
            }
        }
    }
}