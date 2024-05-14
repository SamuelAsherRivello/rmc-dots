using UnityEngine;

namespace RMC.DOTS.Samples.Pong2D.Pong2D_Version01_GO
{
    //  Namespace Properties ------------------------------

    //  Class Attributes ----------------------------------
    
    /// <summary>
    /// </summary>
    public class PaddleBase : MonoBehaviour
    {
        //  Events ----------------------------------------

        //  Properties ------------------------------------
        public Rigidbody Rigidbody { get { return _rigidBody;}}
        
        //  Fields ----------------------------------------
        [SerializeField] 
        private Rigidbody _rigidBody;

        [SerializeField] 
        private float _speed = 500;
        
        //  Unity Methods  --------------------------------
        protected virtual void Start()
        {
         
        }
        
        //  Methods ---------------------------------------
        public void Move(Vector3 movement)
        {
            _rigidBody.AddForce (movement * _speed, ForceMode.Acceleration);
        }
        
        //  Event Handlers --------------------------------
    }
}