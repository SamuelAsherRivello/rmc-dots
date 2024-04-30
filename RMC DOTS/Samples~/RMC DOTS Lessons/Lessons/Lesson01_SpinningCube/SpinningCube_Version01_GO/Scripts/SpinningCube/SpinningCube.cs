using UnityEngine;

namespace RMC.DOTS.Lessons.SpinningCube.SpinningCube_Version01_GO
{
    //  Entity  -------------------------------------------
    public class SpinningCube : MonoBehaviour
    {
        //  Component  ------------------------------------
        [SerializeField] 
        public Vector3 _rotationDelta = new Vector3(1, 1, 1);

        [SerializeField] 
        public GameObject _cube;

        //  System ----------------------------------------
        protected void Update()
        {
            _cube.transform.Rotate(_rotationDelta * Time.deltaTime);
        }
    }
}