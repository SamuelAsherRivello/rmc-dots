using RMC.DOTS.SystemGroups;
using RMC.DOTS.Systems.Input;
using Unity.Entities;
using Unity.Transforms;

namespace RMC.DOTS.Demos.HybridSync
{
    [UpdateInGroup(typeof(PauseablePresentationSystemGroup))]
    public partial struct HybridSyncInputSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<HybridSyncSystemAuthoring.HybridSyncSystemIsEnabledTag>();
            state.RequireForUpdate<InputComponent>();
        }  

        
        
        public void OnUpdate(ref SystemState state)
        {
            // Loop through all players. Move each
            foreach (var (hybridSyncInputComponent, hybridSyncAnimatorComponent) in
                     SystemAPI.Query<
                             RefRO<HybridSyncInputComponent>, HybridSyncAnimatorReferenceComponent>().
                         WithAll<LocalTransform>())
            {
                
                // Keyframes
                hybridSyncAnimatorComponent.Value.SetFloat("Blend", hybridSyncInputComponent.ValueRO.Blend);
            
                // Trigger
                if (hybridSyncInputComponent.ValueRO.Trigger.Value.Length > 0)
                {
                    hybridSyncAnimatorComponent.Value.SetTrigger(hybridSyncInputComponent.ValueRO.Trigger.Value);
                }
                
                // Move
                hybridSyncAnimatorComponent.Value.transform.position = hybridSyncInputComponent.ValueRO.Position;
                
                // Face
                hybridSyncAnimatorComponent.Value.transform.rotation = hybridSyncInputComponent.ValueRO.Rotation;
            }
        }
    }
}