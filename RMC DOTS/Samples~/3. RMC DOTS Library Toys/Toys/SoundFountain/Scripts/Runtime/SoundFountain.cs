using RMC.Core.Utilities;
using UnityEngine;

namespace RMC.DOTS.Toys.Fountain
{
    public class SoundFountain : MonoBehaviour
    {
        //  Initialization --------------------------------
        protected void Start()
        {
            Debug.Log("SoundFountain Demo. Watch the spawning.");
            
            // The Unity Project Must Have These Layers
            // LayerMaskUtility Shows Errors If Anything Is Missing
            LayerMaskUtility.AssertLayerMask("Floor", 15);
            LayerMaskUtility.AssertLayerMask("Droplet", 16);
        }
    }
}