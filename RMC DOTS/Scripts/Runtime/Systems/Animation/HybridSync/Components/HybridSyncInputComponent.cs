using Unity.Collections;
using Unity.Entities;
using UnityEngine;

namespace RMC.DOTS.Demos.HybridSync
{
    public struct HybridSyncInputComponent: IComponentData
    {
        public Vector3 Position;
        public Quaternion Rotation;
        public float Blend;
        public FixedString128Bytes Trigger;
    }
}