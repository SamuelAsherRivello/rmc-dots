using RMC.Core.Utilities;
using UnityEngine;

namespace RMC.DOTS.Demos.Input
{
    public class PhysicsTrigger : MonoBehaviour
    {
        //  Initialization --------------------------------
        protected void Start()
        {
            Debug.Log("PhysicsTrigger Demo. Watch the console.");
            
            // The Unity Project Must Have These Layers
            // LayerMaskUtility Shows Errors If Anything Is Missing
            LayerMaskUtility.AssertLayerMask("Player", 6);
            LayerMaskUtility.AssertLayerMask("Goal", 9);
        }
    }
}