using UnityEngine;

namespace RMC.DOTS.Demos.Culling
{
    public class Culling : MonoBehaviour
    {
        //  Initialization --------------------------------
        protected void Start()
        {
            Debug.Log("Culling Demo. Watch the Unity Console and Unity Scene View.");
            Debug.Log("NOTE: Depending on your Unity preferences, you may STILL see any destroyed entities in the Unity Scene View.");
        }
    }
}