using Unity.Entities;
using UnityEngine;

namespace RMC.DOTS.Systems.PhysicsTrigger
{
    public struct PhysicsTriggerComponent : IComponentData
    {
        public LayerMask MemberOfLayerMask;
        public LayerMask CollidesWithLayerMask;
    }
}