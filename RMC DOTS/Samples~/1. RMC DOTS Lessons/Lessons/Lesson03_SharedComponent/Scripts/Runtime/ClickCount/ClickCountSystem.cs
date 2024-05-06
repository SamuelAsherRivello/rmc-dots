using Unity.Burst;
using Unity.Entities;
using Unity.Rendering;
using UnityEngine;

namespace RMC.DOTS.Lessons.SharedComponent
{
    //  System  ------------------------------------
    [BurstCompile]
    [UpdateInGroup(typeof(PresentationSystemGroup), OrderLast = true)]
    public partial class ClickCountSystem : SystemBase
    {
        private EndSimulationEntityCommandBufferSystem _ecbSystem;
        
        
        [BurstCompile]
        protected override void OnCreate()
        {
            RequireForUpdate<EndInitializationEntityCommandBufferSystem.Singleton>();
            RequireForUpdate<WorldRenderBounds>();
            _ecbSystem = World.GetExistingSystemManaged<EndSimulationEntityCommandBufferSystem>();
        }
        
        
        
        [BurstCompile]
        protected override void OnUpdate()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                var ecb = _ecbSystem.CreateCommandBuffer();

                foreach (var (worldRenderBounds, entity)
                         in SystemAPI.Query<RefRO<WorldRenderBounds>>().WithEntityAccess().WithAll<ClickCountSharedComponent>())
                {
                    var center = worldRenderBounds.ValueRO.Value.Center;
                    var extents = worldRenderBounds.ValueRO.Value.Extents;

                    Bounds newBounds = new Bounds(center, extents);

                    bool haveHit = newBounds.IntersectRay(ray, out float distance);

                    if (haveHit)
                    {
                        // Get ONE value from the local instance
                        ClickCountSharedComponent clickCountComponent = EntityManager.GetSharedComponent<ClickCountSharedComponent>(entity);
          
                        // NOTE: **Manually** set the value for ALL entities.
                        //TODO: I'm surprised I have to do this myself. What is the point of it being 'shared' if I have to do this?
                        foreach (var (oldComponent, entity2)
                                 in SystemAPI.Query<ClickCountSharedComponent>().
                                     WithEntityAccess().
                                     WithSharedComponentFilter(clickCountComponent))
                        {
                            ecb.SetSharedComponentManaged<ClickCountSharedComponent>(entity2, new ClickCountSharedComponent
                            {
                                Value = clickCountComponent.Value + 1
                            });
                        }
                        
                        //Log
                        Debug.Log($"SetSharedComponentForAllEntities() to Value = {clickCountComponent.Value + 1}");
                     
                    }
                }
            }
        }
    }
}