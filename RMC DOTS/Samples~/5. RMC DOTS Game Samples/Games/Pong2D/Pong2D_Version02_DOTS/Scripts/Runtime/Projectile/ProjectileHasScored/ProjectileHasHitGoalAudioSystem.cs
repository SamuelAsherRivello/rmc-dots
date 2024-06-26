﻿using RMC.DOTS.Systems.Audio;
using Unity.Entities;
using Unity.Physics.Systems;

namespace RMC.DOTS.Samples.Pong2D.Pong2D_Version02_DOTS
{
    [UpdateInGroup(typeof(PhysicsSystemGroup))]
    [UpdateAfter(typeof(PhysicsSimulationGroup))]
    [UpdateBefore(typeof(ExportPhysicsWorld))]
    [RequireMatchingQueriesForUpdate]
    public partial struct ProjectileHasHitGoalAudioSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<ProjectileHasHitGoalSystemAuthoring.ProjectileHasHitGoalSystemIsEnabled>();
            state.RequireForUpdate<EndInitializationEntityCommandBufferSystem.Singleton>();
        }

        public void OnUpdate(ref SystemState state)
        {
            var ecb = SystemAPI.
                GetSingleton<EndInitializationEntityCommandBufferSystem.Singleton>().
                CreateCommandBuffer(state.WorldUnmanaged);
            
            foreach (var (projectileTag, projectileHasScoredTag, entity) in SystemAPI.Query<ProjectileTag, ProjectileHasHitGoalComponent>().WithEntityAccess())
            {
                //Debug.Log("Play this sound: " + entity.Index + " fc: " + Time.frameCount);
                Entity audioEntity = ecb.CreateEntity();
                ecb.AddComponent<AudioComponent>(audioEntity, new AudioComponent
                (
                    "Goal01"
                ));
            }
        }
    }
}