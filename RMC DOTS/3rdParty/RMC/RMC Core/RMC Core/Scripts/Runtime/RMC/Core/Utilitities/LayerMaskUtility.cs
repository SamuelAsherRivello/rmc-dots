using UnityEngine;

namespace RMC.Core.Utilities
{
    public static class LayerMaskUtility
    {
        //  Methods ---------------------------------------
        public static void AssertLayerMask(string layerName, int layerIndex)
        {
            //Make sure the project has layers set properly
            var indexFound = LayerMask.NameToLayer(layerName);
            if (indexFound != layerIndex)
            {
                Debug.Log($"AssertLayerMask failed. Must set layer at index of '{layerIndex}' to be name of '{layerName}'.");
            }
        }
    }
}