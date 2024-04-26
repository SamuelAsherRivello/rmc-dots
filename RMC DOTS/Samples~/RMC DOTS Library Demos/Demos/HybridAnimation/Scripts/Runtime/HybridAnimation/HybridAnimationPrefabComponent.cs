using Unity.Entities;
using UnityEngine;

namespace RMC.DOTS.Demos.HybridAnimation
{
    public class HybridAnimationPrefabComponent : IComponentData
    {
        public GameObject Value;
    }

    public class HybridAnimationAnimatorComponent : ICleanupComponentData
    {
        public Animator Value;
    }
}