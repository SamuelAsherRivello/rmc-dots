﻿using RMC.DOTS.SystemGroups;
using RMC.DOTS.Systems.Audio;
using Unity.Entities;

namespace RMC.DOTS.Samples.RollABall3D.RollABall3D_Version02_DOTS
{
    [UpdateInGroup(typeof(UnpauseablePresentationSystemGroup))]
    [UpdateBefore(typeof(PickupWasCollectedDestroySystem))] 
    public partial struct PickupWasCollectedAudioSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<EndInitializationEntityCommandBufferSystem.Singleton>();
            state.RequireForUpdate<PickupTag>();
            state.RequireForUpdate<PickupWasCollectedTag>();
        }

        public void OnUpdate(ref SystemState state)
        {
            var ecb = SystemAPI.
                GetSingleton<EndInitializationEntityCommandBufferSystem.Singleton>().
                CreateCommandBuffer(state.WorldUnmanaged);
            
            foreach (var (pickupTag, pickupWasCollectedTag, entity) 
                     in SystemAPI.Query<PickupTag, PickupWasCollectedTag>().
                         WithEntityAccess())
            {
                //Debug.Log("Play this sound: " + entity.Index + " fc: " + Time.frameCount);
                Entity audioEntity = ecb.CreateEntity();
                ecb.AddComponent<AudioComponent>(audioEntity,
                    new AudioComponent
                    (
                        "Pickup01"
                    ));
            }
        }
    }
}