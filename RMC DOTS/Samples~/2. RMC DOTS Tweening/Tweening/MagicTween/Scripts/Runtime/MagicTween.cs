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
            Debug.LogWarning("Add scripting define for MAGICTWEEN_ENABLE_TRANSFORM_JOBS and MAGICTWEEN_ENABLE_DEMO to enable this demo.");
#endif
            
        }
    }
}