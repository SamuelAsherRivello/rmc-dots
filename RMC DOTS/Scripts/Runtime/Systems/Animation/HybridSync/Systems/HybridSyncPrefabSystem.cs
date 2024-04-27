using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

namespace RMC.DOTS.Demos.HybridSync
{
    [UpdateInGroup(typeof(PresentationSystemGroup), OrderFirst = true)]
    public partial struct HybridSyncPrefabSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<HybridSyncSystemAuthoring.HybridSyncSystemIsEnabledTag>();
            state.RequireForUpdate<HybridSyncPrefabComponent>();
        }
        
        public void OnUpdate(ref SystemState state)
        {
            var ecb = new EntityCommandBuffer(Allocator.Temp);
            
            
            //Add HybridSyncAnimatorReferenceComponent
            foreach (var (playerGameObjectPrefab, entity) in 
                     SystemAPI.Query<HybridSyncPrefabComponent>().
                         WithNone<HybridSyncAnimatorReferenceComponent>().
                         WithEntityAccess())
            {
                var newCompanionGameObject =
                    Object.Instantiate(playerGameObjectPrefab.Prefab, 
                        playerGameObjectPrefab.Transform.position, 
                        playerGameObjectPrefab.Transform.rotation);
                
                var newAnimatorReference = new HybridSyncAnimatorReferenceComponent
                {
                    Value = newCompanionGameObject.GetComponent<Animator>()
                };
                ecb.AddComponent(entity, newAnimatorReference);
            } 
            
            //Remove HybridSyncAnimatorReferenceComponent
            foreach (var (animatorReference, entity) in 
                     SystemAPI.Query<HybridSyncAnimatorReferenceComponent>().
                         WithNone<HybridSyncPrefabComponent, LocalTransform>().
                         WithEntityAccess())
            {
                Object.Destroy(animatorReference.Value.gameObject);
                ecb.RemoveComponent<HybridSyncAnimatorReferenceComponent>(entity);
            }
            
            ecb.Playback(state.EntityManager);
            ecb.Dispose();
        }
    }
}