#if MAGICTWEEN_ENABLE_TRANSFORM_JOBS && MAGICTWEEN_ENABLE_DEMO

using MagicTween;
using MagicTween.Plugins;

namespace RMC.DOTS.Tweening.MagicTween
{
    public partial class MySampleTweenTranslationSystem : TweenTranslationSystemBase<float, NoOptions, FloatTweenPlugin,
        MySampleTweenComponent, MySampleTweenTranslator>
    {
    }
}

#endif //#if MAGICTWEEN_ENABLE_TRANSFORM_JOBS && MAGICTWEEN_ENABLE_DEMO
