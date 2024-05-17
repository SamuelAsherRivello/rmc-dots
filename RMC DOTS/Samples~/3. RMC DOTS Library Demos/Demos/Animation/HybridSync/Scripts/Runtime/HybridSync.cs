using Unity.Scenes;
using UnityEngine;

namespace RMC.DOTS.Demos.HybridSync
{
    public class HybridSync : MonoBehaviour
    {
        [SerializeField]
        private SubScene _subScene;
        
        //  Initialization --------------------------------
        protected void Start()
        {
            Debug.Log("HybridSync Demo. Use arrow keys to move & Spacebar/Enter to change animations.");
            
            //TODO: Not sure why, but...
            Debug.LogWarning("NOTE: Before playing, Open SubScene (via checkbox) in Scene Hierarchy Window.");
        }
    }
}