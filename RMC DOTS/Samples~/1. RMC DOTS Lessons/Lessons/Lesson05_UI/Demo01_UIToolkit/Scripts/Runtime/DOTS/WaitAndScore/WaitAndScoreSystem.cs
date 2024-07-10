using Unity.Entities;
using UnityEngine;

namespace RMC.DOTS.Lessons.UI.UIToolkit
{
    [RequireMatchingQueriesForUpdate]
    public partial struct WaitAndScoreSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            //Only run update for certain conditions
            state.RequireForUpdate<SimpleScoreComponent>();
            state.RequireForUpdate<SimpleScoreSystemAuthoring.SimpleScoreSystemIsEnabled>();
            state.RequireForUpdate<EndInitializationEntityCommandBufferSystem.Singleton>();
            
            // Wait and score
            var entity = state.EntityManager.CreateEntity();
            state.EntityManager.AddComponentData(entity, new WaitAndScoreComponent
            {
                WaitForSeconds = 1,
                ScoreDelta = 3
            });
        }
        
        public void OnUpdate(ref SystemState state)
        {
            var ecb = SystemAPI.
                GetSingleton<EndInitializationEntityCommandBufferSystem.Singleton>().
                CreateCommandBuffer(state.WorldUnmanaged);
            
            foreach (var (waitAndScoreComponent, entity) in
                     SystemAPI.Query<RefRW<WaitAndScoreComponent>>().WithEntityAccess())

            {
                //wait and score
                waitAndScoreComponent.ValueRW.WaitForSeconds -= Time.deltaTime;
                
                if (waitAndScoreComponent.ValueRW.WaitForSeconds <= 0)
                {
                    // Update the score
                    var simpleScoreComponent = SystemAPI.GetSingleton<SimpleScoreComponent>();
                    simpleScoreComponent.Score += waitAndScoreComponent.ValueRW.ScoreDelta;
                    SystemAPI.SetSingleton(simpleScoreComponent);
                    
                    // And stop
                    Debug.Log("WaitAndScoreSystem just rewarded points after 1 second. Now it will stop.");
                    ecb.DestroyEntity(entity);
                }
            }
        }
    }
}