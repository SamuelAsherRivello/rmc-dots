#if MAGICTWEEN_ENABLE_TRANSFORM_JOBS && MAGICTWEEN_ENABLE_DEMO

using MagicTween;
using UnityEngine;

namespace RMC.DOTS.Tweening.MagicTween
{
    public struct MySampleTweenTranslator : ITweenTranslator<float, MySampleTweenComponent>
    {
        // Apply the value to the component
        public void Apply(ref MySampleTweenComponent component, in float value)
        {
            component.value = value;
            Debug.Log("Tween Custom Value To : " + component.value);
        }
        

        // Return the current value of the component
        public float GetValue(ref MySampleTweenComponent component)
        {
            return component.value;
        }
    }
}

#endif //#if MAGICTWEEN_ENABLE_TRANSFORM_JOBS && MAGICTWEEN_ENABLE_DEMO
