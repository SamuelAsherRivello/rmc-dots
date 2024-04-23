using RMC.DOTS.Systems.Random;
using Unity.Entities;
using UnityEngine;

namespace RMC.DOTS.Demos.Random.ConsoleLogRandom
{
    public partial struct ConsoleLogRandomSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<ConsoleLogRandomSystemAuthoring.ConsoleLogRandomSystemIsEnabledTag>();
            state.RequireForUpdate<RandomComponentAuthoring.RandomSystemIsEnabledTag>();
            state.RequireForUpdate<RandomComponent>();
        }

        public void OnUpdate(ref SystemState state)
        {
            var randomComponentEntity = SystemAPI.GetSingletonEntity<RandomComponent>();
            var randomComponentAspect = SystemAPI.GetAspect<RandomComponentAspect>(randomComponentEntity);
            Debug.Log("aspect.NextFloat(): " + randomComponentAspect.NextFloat(0, 10));
        }
    }
}
