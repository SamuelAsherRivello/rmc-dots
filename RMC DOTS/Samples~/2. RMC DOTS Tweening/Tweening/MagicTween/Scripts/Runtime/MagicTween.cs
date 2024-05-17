using UnityEngine;

namespace RMC.DOTS.Tweening.MagicTween
{
    
    /// <summary>
    /// Demos using 3rd party: https://github.com/AnnulusGames/MagicTween?tab=readme-ov-file#implementation-for-ecs
    /// </summary>
    public class MagicTween : MonoBehaviour
    {
        //  Initialization --------------------------------
        protected void Start()
        {
            Debug.Log("MagicTween Demo. Watch the spawning.");

#if !MAGICTWEEN_ENABLE_TRANSFORM_JOBS || !MAGICTWEEN_ENABLE_DEMO

            Debug.LogWarning("1. Add Magic Tween in package manager via https://github.com/AnnulusGames/MagicTween?tab=readme-ov-file#implementation-for-ecs.");

            Debug.LogWarning("2. Add scripting define for MAGICTWEEN_ENABLE_TRANSFORM_JOBS");

            Debug.LogWarning("3. Add scripting define for MAGICTWEEN_ENABLE_DEMO");
#endif
            
        }
    }
}