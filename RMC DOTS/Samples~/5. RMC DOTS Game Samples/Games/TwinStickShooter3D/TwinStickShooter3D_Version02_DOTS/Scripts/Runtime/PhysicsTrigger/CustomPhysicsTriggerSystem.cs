using RMC.DOTS.Systems.GameState;
using RMC.DOTS.Systems.Player;
using Unity.Burst;
using Unity.Entities;
using Unity.Physics.PhysicsStateful;
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
    [UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
    [UpdateAfter(typeof(StatefulTriggerEventSystem))] 
    [BurstCompile]
    public partial struct CustomPhysicsTriggerSystem : ISystem
    {
	    private ComponentLookup<EnemyTag> _enemyTagLookup;
	    private ComponentLookup<PlayerTag> _playerTagLookup;
	    private ComponentLookup<WallTag> _wallTagLookup;
	    
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<CustomPhysicsTriggerSystemAuthoring.CustomPhysicsTriggerSystemIsEnabledTag>();
            state.RequireForUpdate<BeginPresentationEntityCommandBufferSystem.Singleton>();
            state.RequireForUpdate<GameStateComponent>();
            state.RequireForUpdate<EnemyTag>();
            state.RequireForUpdate<BulletTag>();
            state.RequireForUpdate<WallTag>();

            
            //
            _enemyTagLookup = state.GetComponentLookup<EnemyTag>();
            _wallTagLookup = state.GetComponentLookup<WallTag>();
            _playerTagLookup = state.GetComponentLookup<PlayerTag>();
        }
        
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
	        _enemyTagLookup.Update(ref state);
	        _wallTagLookup.Update(ref state);
	        _playerTagLookup.Update(ref state);
	        
	        var ecb = SystemAPI.
		        GetSingleton<BeginPresentationEntityCommandBufferSystem.Singleton>().
		        CreateCommandBuffer(state.WorldUnmanaged);
            
	        
	        // Bullet TRIGGERS with...
	        foreach (var (statefulEventBuffers, bulletEntity) in 
	                 SystemAPI.Query<DynamicBuffer<StatefulTriggerEvent>>().
		                 WithAll<BulletTag>().
		                 WithEntityAccess())
	        {
                
		        for (int bufferIndex = 0; bufferIndex < statefulEventBuffers.Length; bufferIndex++)
		        {
			        var statefulEvent = statefulEventBuffers[bufferIndex];
			        if (statefulEvent.State == StatefulEventState.Enter)
			        {
				        var otherEntity = statefulEvent.GetOtherEntity(bulletEntity);
				        
				        // Bullet TRIGGERS with... Enemy
				        if (_enemyTagLookup.HasComponent(otherEntity))
				        {
					        ecb.AddComponent<EnemyWasHitThisFrameTag>(otherEntity);
					        ecb.AddComponent<BulletWasHitThisFrameTag>(bulletEntity);
				        }
				        
				        // Bullet TRIGGERS with... Wall
				        if (_wallTagLookup.HasComponent(otherEntity))
				        {
					        ecb.AddComponent<BulletWasHitThisFrameTag>(bulletEntity);
				        }
				        break;
			        }
		        }
	        }
	        
	        // Gem TRIGGERS with...
	        foreach (var (statefulEventBuffers, gemEntity) in 
	                 SystemAPI.Query<DynamicBuffer<StatefulTriggerEvent>>().
		                 WithAll<GemTag>().
		                 WithEntityAccess())
	        {
                
		        for (int bufferIndex = 0; bufferIndex < statefulEventBuffers.Length; bufferIndex++)
		        {
			        var statefulEvent = statefulEventBuffers[bufferIndex];
			        if (statefulEvent.State == StatefulEventState.Enter)
			        {
				        var otherEntity = statefulEvent.GetOtherEntity(gemEntity);
				        
				        // Bullet COLLIDES with... Player
				        if (_playerTagLookup.HasComponent(otherEntity))
				        {
					        ecb.AddComponent<GemWasHitThisFrameTag>(gemEntity);
				        }
				        break;
			        }
		        }
	        }
        }
    }
}