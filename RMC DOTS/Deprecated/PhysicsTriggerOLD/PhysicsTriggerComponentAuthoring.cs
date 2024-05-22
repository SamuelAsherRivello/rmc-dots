using System;
using Unity.Entities;
using UnityEngine;

namespace RMC.DOTS.Systems.PhysicsTrigger
{

    [Obsolete("Remove this and use the RMC DOTS 'PhysicsStateful' feature instead")]
    public class PhysicsTriggerComponentAuthoring : MonoBehaviour
    {
        public LayerMask MemberOfLayerMask;
        public LayerMask CollidesWithLayerMask;

        public class PhysicsTriggerComponentAuthoringBaker : Baker<PhysicsTriggerComponentAuthoring>
        {
            public override void Bake(PhysicsTriggerComponentAuthoring authoring)
            {
                Entity entity = GetEntity(TransformUsageFlags.Dynamic);

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
