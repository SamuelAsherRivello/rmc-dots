using Unity.Entities;
using UnityEngine;

namespace RMC.DOTS.Systems.FollowTarget
{
    public struct FollowerComponent : IComponentData
    {
        public LayerMask TargetsLayerMask;
        public float LinearSpeed;
        public float AngularSpeed;
        public float Radius;
    }
}