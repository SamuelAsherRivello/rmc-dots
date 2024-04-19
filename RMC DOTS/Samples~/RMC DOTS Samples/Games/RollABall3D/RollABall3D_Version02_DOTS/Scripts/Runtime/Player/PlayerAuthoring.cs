﻿using RMC.DOTS.Systems.PhysicsTrigger;
using Unity.Entities;
using UnityEngine;

namespace RMC.DOTS.Samples.RollABall3D.RollABall3D_Version02_DOTS
{
    public class PlayerAuthoring : MonoBehaviour
    {
        public LayerMask MemberOfLayerMask;
        public LayerMask CollidesWithLayerMask;
        
        public class PlayerAuthoringBaker : Baker<PlayerAuthoring>
        {
            public override void Bake(PlayerAuthoring authoring)
            {
                Entity entity = GetEntity(TransformUsageFlags.Dynamic);
                                
                AddComponent<PlayerTag>(entity);
                
                AddComponent<PhysicsTriggerComponent>(entity,
                    new PhysicsTriggerComponent
                    {
                        MemberOfLayerMask = authoring.MemberOfLayerMask,
                        CollidesWithLayerMask = authoring.CollidesWithLayerMask
                    });

            }
        }
    }
}