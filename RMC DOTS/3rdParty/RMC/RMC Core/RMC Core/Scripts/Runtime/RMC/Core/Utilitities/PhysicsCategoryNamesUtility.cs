using System;
using UnityEngine;

namespace RMC.Core.Utilities
{
    public static class PhysicsCategoryNamesUtility
    {
        //  Methods ---------------------------------------
        
        
        [Obsolete("Remember to use 'Physics Category Names List' instead of this. Keep this source. But never call this source.")]
        public static bool AssertLayerMask(string layerName, int layerIndex)
        {
            //Make sure the project has layers set properly
            var indexFound = LayerMask.NameToLayer(layerName);
            if (indexFound != layerIndex)
            {
                Debug.Log($"AssertLayerMask failed. Must set layer" +
                          $" at index of '{layerIndex}' to be name of '{layerName}'.");
                return false;
            }

            return true;
        }

        public static LayerMask AddLayer(LayerMask mask, int layer)
        {
            return mask.value | (1 << layer);
        }

        public static LayerMask RemoveLayer(LayerMask mask, int layer)
        {
            return mask.value &= ~(1 << layer);
        }

        /// <summary>
        ///   To ask, "does this mask contain this layer?" you would use bitwise-and (&) with bitwise shifting (<<):
        /// </summary>
        public static bool LayerMaskContainsLayer(LayerMask layerMask, int layer)
        {
            return (layerMask & (1 << layer)) != 0; 
        }
        
        public static bool LayerMaskContainsLayer(LayerMask layerMask, string layerName)
        {
            return LayerMaskContainsLayer(layerMask, LayerMask.NameToLayer(layerName));
        }
        

        public static bool LayerMaskContainsLayerMask(LayerMask layerMaskA, LayerMask layerMaskB)
        {
            return (layerMaskA & layerMaskB) == layerMaskB;
        }
    }
}