using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

namespace RMC.DOTS.Demos.HybridAnimation
{
    [UpdateInGroup(typeof(PresentationSystemGroup), OrderFirst = true)]
    public partial struct HybridAnimationSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<HybridAnimationSystemAuthoring.HybridAnimationSystemIsEnabledTag>();
        }
        
        public void OnUpdate(ref SystemState state)
        {
            var ecb = new EntityCommandBuffer(Allocator.Temp);
            
            foreach (var (playerGameObjectPrefab, entity) in 
                     SystemAPI.Query<HybridAnimationPrefabComponent>().WithNone<HybridAnimationAnimatorComponent>().WithEntityAccess())
            {
                var newCompanionGameObject = Object.Instantiate(playerGameObjectPrefab.Value);
                var newAnimatorReference = new HybridAnimationAnimatorComponent
                {
                    Value = newCompanionGameObject.GetComponent<Animator>()
                };
                ecb.AddComponent(entity, newAnimatorReference);
            }
            

            
            foreach (var (animatorReference, entity) in 
                     SystemAPI.Query<HybridAnimationAnimatorComponent>().WithNone<HybridAnimationPrefabComponent, LocalTransform>()
                         .WithEntityAccess())
            {
                Object.Destroy(animatorReference.Value.gameObject);
                ecb.RemoveComponent<HybridAnimationAnimatorComponent>(entity);
            }
            
            ecb.Playback(state.EntityManager);
            ecb.Dispose();
        }
    }
}