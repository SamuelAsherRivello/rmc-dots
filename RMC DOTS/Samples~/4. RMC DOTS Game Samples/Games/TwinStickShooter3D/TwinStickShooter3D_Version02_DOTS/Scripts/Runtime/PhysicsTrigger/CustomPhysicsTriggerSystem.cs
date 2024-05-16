using RMC.DOTS.SystemGroups;
using RMC.DOTS.Systems.GameState;
using RMC.DOTS.Systems.PhysicsTrigger;
using Unity.Burst;
using Unity.Entities;
using UnityEngine;

namespace RMC.DOTS.Samples.Games.TwinStickShooter3D.TwinStickShooter3D_Version02_DOTS
{
    
    /// <summary>
    ///
    /// 1. This 'listens to' <see cref="PhysicsTriggerSystem"/>
    /// and sets a tag on each entity that was hit.
    ///
    /// 2. Separately the <see cref="WasHitSystem"/> will 'listen' for these tags
    /// 
    /// </summary>
    [UpdateInGroup(typeof(UnpauseableSystemGroup))]
    public partial struct CustomPhysicsTriggerSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<PhysicsTriggerSystemAuthoring.PhysicsTriggerSystemIsEnabledTag>();
            state.RequireForUpdate<BeginPresentationEntityCommandBufferSystem.Singleton>();
            state.RequireForUpdate<GameStateComponent>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var ecb = SystemAPI.
                GetSingleton<BeginPresentationEntityCommandBufferSystem.Singleton>().
                CreateCommandBuffer(state.WorldUnmanaged);
            
            
            //////////////////////////////////////
            //GEM - REMOVE
            foreach (var (physicsTriggerOutputComponent, entity) 
                     in SystemAPI.Query<RefRO<PhysicsTriggerOutputComponent>>().
                         WithEntityAccess().WithAll<GemTag, GemWasHitThisFrameTag>())
            {
                ecb.RemoveComponent<GemWasHitThisFrameTag>(entity);
            }

            
            //GEM - ADD
            foreach (var (physicsTriggerOutputComponent, entity) 
                     in SystemAPI.Query<RefRO<PhysicsTriggerOutputComponent>>().
                         WithEntityAccess().WithAll<GemTag>().WithNone<GemWasHitThisFrameTag>())
            {
                if (physicsTriggerOutputComponent.ValueRO.PhysicsTriggerType == PhysicsTriggerType.Enter &&
                    physicsTriggerOutputComponent.ValueRO.TimeFrameCountForLastCollision == Time.frameCount)
                {
                    ecb.AddComponent<GemWasHitThisFrameTag>(entity);
                }
            }


            //////////////////////////////////////
            //BULLET - REMOVE
            foreach (var (physicsTriggerOutputComponent, entity) 
                     in SystemAPI.Query<RefRO<PhysicsTriggerOutputComponent>>().
                         WithEntityAccess().WithAll<BulletTag, BulletWasHitThisFrameTag>())
            {
                ecb.RemoveComponent<BulletWasHitThisFrameTag>(entity);
            }

            
            //BULLET - ADD
            foreach (var (physicsTriggerOutputComponent, entity) 
                     in SystemAPI.Query<RefRO<PhysicsTriggerOutputComponent>>().
                         WithEntityAccess().WithAll<BulletTag>().WithNone<BulletWasHitThisFrameTag>())
            {
                if (physicsTriggerOutputComponent.ValueRO.PhysicsTriggerType == PhysicsTriggerType.Enter &&
                    physicsTriggerOutputComponent.ValueRO.TimeFrameCountForLastCollision == Time.frameCount)
                {
                    ecb.AddComponent<BulletWasHitThisFrameTag>(entity);
                }
            }

            //////////////////////////////////////
            //BULLET - REMOVE
            foreach (var (physicsTriggerOutputComponent, entity) 
                     in SystemAPI.Query<RefRO<PhysicsTriggerOutputComponent>>().
                         WithEntityAccess().WithAll<EnemyTag, EnemyWasHitThisFrameTag>())
            {
                ecb.RemoveComponent<EnemyWasHitThisFrameTag>(entity);
            }

            
            //BULLET - ADD
			foreach (var (physicsTriggerOutputComponent, entity)
					 in SystemAPI.Query<RefRO<PhysicsTriggerOutputComponent>>().
						 WithEntityAccess().WithAll<EnemyTag>().WithNone<EnemyWasHitThisFrameTag>())
			{
                if (physicsTriggerOutputComponent.ValueRO.PhysicsTriggerType == PhysicsTriggerType.Enter &&
                    physicsTriggerOutputComponent.ValueRO.TimeFrameCountForLastCollision == Time.frameCount)
                {
                    ecb.AddComponent<EnemyWasHitThisFrameTag>(entity);
                }
            }
		}
    }
}