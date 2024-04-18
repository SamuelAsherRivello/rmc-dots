using Unity.Entities;
using Unity.Physics;
using Unity.Physics.Extensions;
using Unity.Physics.Systems;
using UnityEngine;

namespace RMC.Playground3D.Pong2D_Version02_DOTS
{
    
    /// <summary>
    /// Apply a linear impulse to an entity EXACTLY ONE TIME
    /// </summary>
    [UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
    [UpdateBefore(typeof(PhysicsSystemGroup))]
    public partial struct ApplyLinearImpulseSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<ApplyLinearImpulseComponent>();
        }

        public void OnUpdate(ref SystemState state)
        {
            foreach (var (velocity, mass, applyLinearImpulseComponent) in 
                     SystemAPI.Query<RefRW<PhysicsVelocity>, RefRO<PhysicsMass>, RefRW<ApplyLinearImpulseComponent>>())
            {
                // Only move in the y
                if (!applyLinearImpulseComponent.ValueRW.WasApplied)
                {
                    velocity.ValueRW.ApplyLinearImpulse(in mass.ValueRO, applyLinearImpulseComponent.ValueRW.Value);
                    applyLinearImpulseComponent.ValueRW.WasApplied = true;
                }
            }
        }
    }
}