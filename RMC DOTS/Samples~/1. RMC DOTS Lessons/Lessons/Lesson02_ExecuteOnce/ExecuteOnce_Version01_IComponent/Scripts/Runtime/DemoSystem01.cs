using Unity.Burst;
using Unity.Entities;
using UnityEngine;

namespace RMC.DOTS.Lessons.ExecuteOnce.ExecuteOnce_01_IComponent
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
            state.RequireForUpdate<ExecuteOnceTag>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state) 
        {
            foreach (var executeOnceTag
                     in SystemAPI.Query<RefRO<ExecuteOnceTag>>())
            {
                Debug.Log("This runs exactly one time.");
            }
        }
    }
}