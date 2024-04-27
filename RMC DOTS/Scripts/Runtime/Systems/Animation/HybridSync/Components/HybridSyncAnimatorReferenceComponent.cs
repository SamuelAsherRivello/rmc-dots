using Unity.Entities;
using UnityEngine;

namespace RMC.DOTS.Demos.HybridSync
{
    public class HybridSyncAnimatorReferenceComponent : ICleanupComponentData
    {
        public Animator Value;
    }
}