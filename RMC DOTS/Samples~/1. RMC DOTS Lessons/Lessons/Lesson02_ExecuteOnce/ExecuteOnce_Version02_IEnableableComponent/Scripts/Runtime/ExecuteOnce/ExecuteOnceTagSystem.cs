using Unity.Burst;
using Unity.Entities;
using UnityEngine;

namespace RMC.DOTS.Lessons.ExecuteOnce.ExecuteOnce_Version02_IEnableableComponent
{
    //  System  ------------------------------------
    [BurstCompile]
    [UpdateInGroup(typeof(PresentationSystemGroup), OrderLast = true)]
    public partial struct ExecuteOnceTagSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<EndInitializationEntityCommandBufferSystem.Singleton>();
            state.RequireForUpdate<ExecuteOnceEnableableTag>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state) 
        {
            var ecb = SystemAPI.
                GetSingleton<EndInitializationEntityCommandBufferSystem.Singleton>().
                CreateCommandBuffer(state.WorldUnmanaged);
            
            foreach (var (executeOnceTag, entity)
                     in SystemAPI.Query<RefRO<ExecuteOnceEnableableTag>>().
                         WithEntityAccess().
                         WithOptions(EntityQueryOptions.IgnoreComponentEnabledState))
            {
                
                // Remove here
                ecb.SetComponentEnabled<ExecuteOnceEnableableTag>(entity, false);
            }
        }
    }
}