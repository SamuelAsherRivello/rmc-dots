using RMC.DOTS.SystemGroups;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

namespace RMC.DOTS.Systems.Culling
{
    [UpdateInGroup(typeof(UnpauseableSystemGroup), OrderFirst = true)]
    public partial class CullingSystem : SystemBase
    {
        // Query all sound request components
        private EntityQuery _entityQuery;

        // Create buffer to request deletion of component after playing sound
        private BeginPresentationEntityCommandBufferSystem _commandBufferSystem;

        protected override void OnCreate()
        {
            _commandBufferSystem = World.GetOrCreateSystemManaged<BeginPresentationEntityCommandBufferSystem>();
            RequireForUpdate<CullingComponent>();
            RequireForUpdate<LocalTransform>();
            RequireForUpdate<CullingSystemAuthoring.CullingSystemIsEnabledTag>();
        }

        protected override void OnUpdate()
        {
            //Do this in the update to capture any screen size changes (e.g. user drags game window)
            var cullingSystemConfigurationComponent = SystemAPI.GetSingleton<CullingSystemAuthoring.CullingSystemConfigurationComponent>();
            var screenBounds = Screen.safeArea;
            screenBounds.size *= cullingSystemConfigurationComponent.ScreenSizeMultiplier;
            
            //TODO: Better way to store this,... or store it in a dots component?
            Camera camera = Camera.main;
            
            
            // Update all components
            foreach (var (localTransform, cullingComponent) in 
                     SystemAPI.Query<RefRO<LocalTransform>, RefRW<CullingComponent>>())
            {
                Vector3 myScreenPosition = camera.WorldToScreenPoint(localTransform.ValueRO.Position);

                bool isMyCenterOffscreen = !screenBounds.Contains(myScreenPosition);
                
                if (!cullingComponent.ValueRO.IsOffscreen.HasValue || 
                    cullingComponent.ValueRO.IsOffscreen.Value != isMyCenterOffscreen)
                {
                    cullingComponent.ValueRW.IsOffscreen = isMyCenterOffscreen;
                }
            }
        }
    }
}