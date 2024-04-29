using UnityEngine;

namespace RMC.DOTS.Demos.Input
{
    public class PhysicsTrigger : MonoBehaviour
    {
        //  Initialization --------------------------------
        protected void Start()
        {
            Debug.Log("PhysicsTrigger Demo. Watch the console.");

            //Make sure the project has layers set properly
            var playerName = "Player";
            var playerIndexCurrent = LayerMask.NameToLayer(playerName);
            var playerIndexRequired = 6;

            if (playerIndexCurrent != playerIndexRequired)
            {
                Debug.Log($"LayerMask failed. Must set Layer {playerIndexRequired} to be '{playerName}'.");
            }
            
            var goalName = "Goal";
            var goalIndexCurrent = LayerMask.NameToLayer(goalName);
            var goalIndexRequired = 9;

            if (goalIndexCurrent != goalIndexRequired)
            {
                Debug.Log($"LayerMask failed. Must set Layer {goalIndexRequired} to be '{goalName}'.");
            }
        }
    }
}