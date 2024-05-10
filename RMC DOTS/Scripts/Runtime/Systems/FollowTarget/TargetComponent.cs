using Unity.Entities;
using UnityEngine;

namespace RMC.DOTS.Systems.FollowTarget
{
    public struct TargetComponent : IComponentData
    {
        public LayerMask MemberOfLayerMask;
    }
}