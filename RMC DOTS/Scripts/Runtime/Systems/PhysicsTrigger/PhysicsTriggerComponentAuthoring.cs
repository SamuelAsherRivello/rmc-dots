using Unity.Entities;
using UnityEngine;

namespace RMC.DOTS.Systems.PhysicsTrigger
{
    /// <summary>
    /// This authoring component allows us to define the speed and direction a pickup will rotate. This script is added
    /// to a GameObject in the RollABallSubScene and the Baker will convert it to an entity at runtime.
    /// </summary>
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
