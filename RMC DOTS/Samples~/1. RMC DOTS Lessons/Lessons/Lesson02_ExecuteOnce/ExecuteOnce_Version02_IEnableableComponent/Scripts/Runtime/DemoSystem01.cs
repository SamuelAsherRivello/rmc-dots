using Unity.Burst;
using Unity.Entities;
using UnityEngine;

namespace RMC.DOTS.Lessons.ExecuteOnce.ExecuteOnce_Version02_IEnableableComponent
{
    //  System  ------------------------------------
    [BurstCompile]
    [UpdateInGroup(typeof(PresentationSystemGroup))]    
    [UpdateBefore(typeof(ExecuteOnceTagSystem))]
    public partial struct DemoSystem01 : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<ExecuteOnceEnableableTag>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state) 
        {
            foreach (var executeOnceTag
                     in SystemAPI.Query<RefRO<ExecuteOnceEnableableTag>>())
            {
                Debug.Log("This runs exactly one time.");
            }
        }
    }
}