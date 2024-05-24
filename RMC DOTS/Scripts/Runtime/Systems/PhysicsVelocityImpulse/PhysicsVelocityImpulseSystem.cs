using RMC.DOTS.Systems.Random;
using RMC.DOTS.Utilities;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Physics.Extensions;
using Unity.Physics.Systems;
using UnityEngine;

namespace RMC.DOTS.Systems.PhysicsVelocityImpulse
{
    /// <summary>
    /// Apply a linear impulse to an entity EXACTLY ONE TIME
    /// </summary>
    [UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
    [UpdateBefore(typeof(PhysicsSystemGroup))] 
    public partial struct PhysicsVelocityImpulseSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<PhysicsVelocityImpulseSystemAuthoring.PhysicsVelocityImpulseSystemIsEnabledTag>();
            state.RequireForUpdate<PhysicsVelocityImpulseComponent>();
            state.RequireForUpdate<EndInitializationEntityCommandBufferSystem.Singleton>();
            state.RequireForUpdate<RandomSystemAuthoring.RandomSystemIsEnabledTag>();
            state.RequireForUpdate<RandomComponent>();
        }

        public void OnUpdate(ref SystemState state)
        {
            var ecb = SystemAPI.
                GetSingleton<EndInitializationEntityCommandBufferSystem.Singleton>().
                CreateCommandBuffer(state.WorldUnmanaged);
            
            var randomComponentEntity = SystemAPI.GetSingletonEntity<RandomComponent>();
            var randomComponentAspect = SystemAPI.GetAspect<RandomComponentAspect>(randomComponentEntity);
            
            
            foreach (var (velocity, mass, physicsVelocityImpulseComponent, entity) in 
                     SystemAPI.Query<
                         RefRW<PhysicsVelocity>, 
                         RefRO<PhysicsMass>, 
                         RefRO<PhysicsVelocityImpulseComponent>>().WithEntityAccess())
            {
                
                var forceVector3 = randomComponentAspect.NextFloat3(
                    ConversionUtility.ToNumericsVector3(physicsVelocityImpulseComponent.ValueRO.MinForce),
                    ConversionUtility.ToNumericsVector3(physicsVelocityImpulseComponent.ValueRO.MaxForce),
                    physicsVelocityImpulseComponent.ValueRO.CanBeNegative);

                float3 mathematicsFloat3 = ConversionUtility.ToMathmaticsFloat3(forceVector3);

                velocity.ValueRW.ApplyLinearImpulse(in mass.ValueRO, mathematicsFloat3);
                ecb.RemoveComponent<PhysicsVelocityImpulseComponent>(entity);
            }
        }
    }
}