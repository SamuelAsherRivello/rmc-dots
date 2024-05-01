using UnityEngine;

namespace RMC.DOTS.Lessons.SpinningCube
{
    /// <summary>
    /// Here are various versions of spinning a cube
    ///
    /// SpinningCube_Version01_GO   - MonoBehavior,     Update
    /// SpinningCube_Version02_DOTS - ISystem,          foreach,            With Burst
    /// SpinningCube_Version03_DOTS - ISystem,          IJobEntity,         With Burst
    /// SpinningCube_Version04_DOTS - SystemBase,       Entities.ForEach,   Without Burst 
    ///
    /// </summary>
    public class VersionComparisonComments {} //Dummy class just for the comment above
}


namespace RMC.DOTS.Lessons.SpinningCube.SpinningCube_Version01_GO
{
    /// <summary>
    /// See <see cref="VersionComparisonComments"/>
    /// </summary>
    public class SpinningCube_Version01_GO : MonoBehaviour
    {
        //  Initialization --------------------------------
        protected void Start()
        {
            Debug.Log("SpinningCube_Version01_GO Demo. Watch the spinning cube.");
        }
    }
}