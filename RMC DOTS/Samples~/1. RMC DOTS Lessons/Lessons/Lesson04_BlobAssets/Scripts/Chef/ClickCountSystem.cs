using Unity.Burst;
using Unity.Entities;
using UnityEngine;

namespace RMC.DOTS.Lessons.BlobAssets
{
    //  System  ------------------------------------
    [BurstCompile]
    [UpdateInGroup(typeof(PresentationSystemGroup), OrderLast = true)]
    public partial struct ChefSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<EndInitializationEntityCommandBufferSystem.Singleton>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state) 
        {
            var ecb = SystemAPI.
                GetSingleton<EndInitializationEntityCommandBufferSystem.Singleton>().
                CreateCommandBuffer(state.WorldUnmanaged);
            
            
            // Run one time
            foreach (var (chefComponent, entity) in SystemAPI.Query<RefRW<ChefComponent>>().
                         WithAll<ChefCookOnceTag>().
                         WithEntityAccess())
            {
                
                var recipeDataRef = chefComponent.ValueRO.RecipeDataRef.Value;
                
                // Show the amount
                Debug.Log($"1. Chef has flour={chefComponent.ValueRO.FlourInKilogramsRemaining}, " +
                          $"water={chefComponent.ValueRO.WaterInLitersRemaining}.");
                
                // Does chef have enough ingredients?
                if (chefComponent.ValueRO.FlourInKilogramsRemaining > recipeDataRef.FlourInKilogramsRequired ||
                    chefComponent.ValueRO.WaterInLitersRemaining > recipeDataRef.WaterInLitersRequired)
                {
                    // Consume ingredients
                    chefComponent.ValueRW.FlourInKilogramsRemaining = recipeDataRef.FlourInKilogramsRequired;
                    chefComponent.ValueRW.WaterInLitersRemaining = recipeDataRef.WaterInLitersRequired;
                    
                    Debug.Log("Chef component had enough ingredients, required in recipe from blob asset. Food is now cooked!");
                }
                else
                {
                    Debug.Log("Chef component had NOT enough ingredients.");
                }
                
                // Show the amount
                Debug.Log($"2. Chef has flour={chefComponent.ValueRO.FlourInKilogramsRemaining}, " +
                          $"water={chefComponent.ValueRO.WaterInLitersRemaining}.");
                
                
                //Trigger to not run again
                ecb.RemoveComponent<ChefCookOnceTag>(entity);
            }
            
        }
    }
}