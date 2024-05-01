using RMC.DOTS.SystemGroups;
using RMC.DOTS.Systems.Random;
using Unity.Burst;
using Unity.Entities;
using UnityEngine;

namespace RMC.DOTS.Demos.Random.ConsoleLogRandom
{
    [BurstCompile]
    [UpdateInGroup(typeof(PauseableSystemGroup))]
    public partial struct ConsoleLogRandomSystem : ISystem
    {
        private int TempCounter;
        
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<ConsoleLogRandomSystemAuthoring.ConsoleLogRandomSystemIsEnabledTag>();
            state.RequireForUpdate<RandomSystemAuthoring.RandomSystemIsEnabledTag>();
            state.RequireForUpdate<RandomComponent>();

            TempCounter = 0;
        }

        //No burst sice I'm using an implicit toString in my Debug.Log
        public void OnUpdate(ref SystemState state)
        {
            var randomComponentEntity = SystemAPI.GetSingletonEntity<RandomComponent>();
            var randomComponentAspect = SystemAPI.GetAspect<RandomComponentAspect>(randomComponentEntity);
            
            //Limit the console output for this demo
            if (++TempCounter <= 5)
            {
                Debug.Log("RandomComponentAspect DemoValue = " + randomComponentAspect.NextFloat(0, 10));
            }
        }
    }
}
