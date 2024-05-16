using System;
using RMC.DOTS.SystemGroups;
using RMC.DOTS.Systems.Audio;
using RMC.DOTS.Systems.DestroyEntity;
using RMC.DOTS.Systems.GameState;
using RMC.DOTS.Systems.PhysicsTrigger;
using RMC.DOTS.Systems.Scoring;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace RMC.DOTS.Samples.Games.TwinStickShooter3D.TwinStickShooter3D_Version02_DOTS
{
	/// <summary>
	/// This processes the tags previously created by the
	/// <see cref="CustomPhysicsTriggerSystem"/>
	/// </summary>
    [UpdateInGroup(typeof(UnpauseableSystemGroup))]
    public partial class WasHitSystem : SystemBase //CHanged from ISystem, just to have Action<>
    {
	    //  Events ----------------------------------------
	    public Action<Type, bool> OnWasHit;
	    
	    
	    //  Fields ----------------------------------------
	    private ComponentLookup<DestroyEntityComponent> _destroyEntityComponentLookup;
	    private ComponentLookup<GemWasDestroyed> _gemWasCollectedTagLookup;
	    
	    
	    //  Unity Methods  --------------------------------
		protected override void OnCreate()
        {
            RequireForUpdate<PhysicsTriggerSystemAuthoring.PhysicsTriggerSystemIsEnabledTag>();
            RequireForUpdate<EndInitializationEntityCommandBufferSystem.Singleton>();
            RequireForUpdate<ScoringComponent>();
            RequireForUpdate<GameStateComponent>();
            
            _destroyEntityComponentLookup = GetComponentLookup<DestroyEntityComponent>();
            _gemWasCollectedTagLookup = GetComponentLookup<GemWasDestroyed>();
        }
		
        protected override void OnUpdate()
        {
	        var ecb = SystemAPI.
		        GetSingleton<EndInitializationEntityCommandBufferSystem.Singleton>().
		        CreateCommandBuffer(World.Unmanaged);
	        
	        ScoringComponent scoringComponent = SystemAPI.GetSingleton<ScoringComponent>();

	        _destroyEntityComponentLookup.Update(this);
			_gemWasCollectedTagLookup.Update(this);
			
			////////////////////////////////
			// GEM
			////////////////////////////////
			foreach (var (gemTag, gemWasHitTag, entity)
			         in SystemAPI.Query<GemTag, GemWasHitThisFrameTag>().
				         WithNone<GemWasDestroyed>().
				         WithEntityAccess())
			{
				scoringComponent.ScoreComponent01.ScoreCurrent += 1;
					
				var audioEntity = ecb.CreateEntity();
				ecb.AddComponent<AudioComponent>(audioEntity, new AudioComponent
				(
					"Pickup01"
				));
				
				// Destroy the Gem
				DestroyEntitySystem.DestroyEntity(ref ecb, _destroyEntityComponentLookup, entity);
				OnWasHit?.Invoke(typeof(GemTag), true);
				
				//HACK: Why do I need this to properly limit this local scope to run once?
				ecb.AddComponent<GemWasDestroyed>(entity);
					
			}

			
			////////////////////////////////
			// ENEMY
			////////////////////////////////
			foreach (var (healthAspect, enemyTag, enemyWasHitTag, localTransform, gemDropComponent, entity)
				in SystemAPI.Query<HealthAspect, EnemyTag, EnemyWasHitThisFrameTag, LocalTransform, GemSpawnComponent>().
					WithNone<EnemyWasDestroyed>().
				WithEntityAccess())
			{
                const float damageDealtPerBullet = 1.0f;
                healthAspect.DealDamage(damageDealtPerBullet);
                if (!healthAspect.IsDead)
                    continue;

                // Instantiate the entity
                var gemEntity = ecb.Instantiate(gemDropComponent.GemPrefab);
				
				// Move entity to initial position
				ecb.SetComponent<LocalTransform>(gemEntity, new LocalTransform
				{
					Position = localTransform.Position + -localTransform.Forward() * 1.5f, //'in front' of the eyes
					Rotation = quaternion.identity,
					Scale = 1
				});
				
				// Destroy the enemy
				DestroyEntitySystem.DestroyEntity(ref ecb, _destroyEntityComponentLookup, entity);
				OnWasHit?.Invoke(typeof(EnemyTag), true);
				
				//HACK: Why do I need this to properly limit this local scope to run once?
				ecb.AddComponent<EnemyWasDestroyed>(entity);
			}
			
			
			////////////////////////////////
			// BULLET
			////////////////////////////////
			foreach (var (bulletTag, bulletWasHitTag, entity) 
			         in SystemAPI.Query<BulletTag, BulletWasHitThisFrameTag>().
				         WithNone<BulletWasDestroyed>().
				         WithEntityAccess())
			{

				var audioEntity = ecb.CreateEntity();
				ecb.AddComponent<AudioComponent>(audioEntity, new AudioComponent
				(
					"Click02"
				));
				
				// HANDLE: Bullet
				DestroyEntitySystem.DestroyEntity(ref ecb, _destroyEntityComponentLookup, entity);
				OnWasHit?.Invoke(typeof(BulletTag), true);
				
				//HACK: Why do I need this to properly limit this local scope to run once?
				ecb.AddComponent<BulletWasDestroyed>(entity);

			}
			
			SystemAPI.SetSingleton<ScoringComponent>(scoringComponent);
		}

        
        //  Methods ---------------------------------------


        //  Event Handlers --------------------------------
        
    }
}