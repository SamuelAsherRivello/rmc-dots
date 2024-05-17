#if MAGICTWEEN_ENABLE_TRANSFORM_JOBS && MAGICTWEEN_ENABLE_DEMO

using Unity.Entities;

namespace RMC.DOTS.Tweening.MagicTween
{
    public struct MySampleTweenComponent : IComponentData
    {
        public float value;
    }
}

#endif //#if MAGICTWEEN_ENABLE_TRANSFORM_JOBS && MAGICTWEEN_ENABLE_DEMO
