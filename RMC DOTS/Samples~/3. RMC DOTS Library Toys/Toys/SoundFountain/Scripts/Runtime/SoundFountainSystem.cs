using RMC.Audio.Data.Types;
using RMC.DOTS.Systems.Audio;
using RMC.DOTS.Systems.PhysicsVelocityImpulse;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
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
    [UpdateAfter(typeof(PhysicsVelocityImpulseSystem))] 
    public partial struct SoundFountainSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<PhysicsVelocityImpulseSystemAuthoring.PhysicsVelocityImpulseSystemIsEnabledTag>();
            state.RequireForUpdate<PhysicsVelocityImpulseComponent>();
            state.RequireForUpdate<EndInitializationEntityCommandBufferSystem.Singleton>();
        }

        public void OnUpdate(ref SystemState state)
        {
            var ecb = SystemAPI.
                GetSingleton<EndInitializationEntityCommandBufferSystem.Singleton>().
                CreateCommandBuffer(state.WorldUnmanaged);
            
            
            foreach (var (physicsVelocity, physicsVelocityImpulseComponent, entity) in 
                     SystemAPI.Query<
                             RefRW<PhysicsVelocity>,
                         RefRO<PhysicsVelocityImpulseComponent>>().
                         WithEntityAccess())
            {
                //NOTE: A key concept is that this system runs AFTER the PhysicsVelocityImpulseSystem
                // So it can grab the following value which was JUST set.
                var pitch = 0.5f + math.abs(physicsVelocity.ValueRO.Linear.y) / 10;
                var volume = AudioConstants.VolumeDefault / 3;
                
                ecb.AddComponent<AudioComponent>(entity, 
                    new AudioComponent(
                        "SoundFountain01", 
                        volume,
                        pitch));
            }
        }
    }
}