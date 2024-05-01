using Unity.Burst;
using Unity.Entities;
using UnityEngine;

namespace RMC.DOTS.Lessons.SpinningCube.Lesson02_ExecuteOnce
{
    //  System  ------------------------------------
    [BurstCompile]
    [UpdateInGroup(typeof(PresentationSystemGroup))]
    [UpdateAfter(typeof(DemoSystem01))]
    [UpdateBefore(typeof(ExecuteOnceTagSystem))]
    public partial struct DemoSystem02 : ISystem 
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
                Debug.Log("This runs exactly one time - as well!");
            }
        }
    }
}