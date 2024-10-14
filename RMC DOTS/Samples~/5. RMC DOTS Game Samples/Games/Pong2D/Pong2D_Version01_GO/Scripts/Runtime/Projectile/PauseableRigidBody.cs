using UnityEngine;

namespace RMC.DOTS.Samples.Pong2D.Pong2D_Version01_GO
{
    //  Namespace Properties ------------------------------

    //  Class Attributes ----------------------------------
    
    /// <summary>
    /// </summary>
    public class PauseableRigidBody : MonoBehaviour
    {
        //  Events ----------------------------------------

        //  Properties ------------------------------------
        public Rigidbody Rigidbody { get { return _rigidBody;}}
        
        public bool IsPaused
        {
            get { return _isPaused; }
            set
            {
                _isPaused = value;
                if (_isPaused)
                {
                    _pausedVelocity = _rigidBody.linearVelocity;
                    _pausedAngularVelocity = _rigidBody.angularVelocity;
                    
                    //Do last
                    _rigidBody.isKinematic = _isPaused;
                }
                else
                {
                    //Do first
                    _rigidBody.isKinematic = _isPaused;
                    
                    _rigidBody.linearVelocity = _pausedVelocity;
                    _rigidBody.angularVelocity = _pausedAngularVelocity;
                }
            }
        }
        
        
        //  Fields ----------------------------------------
        [SerializeField] 
        private Rigidbody _rigidBody;

        private bool _isPaused = false;
        private Vector3 _pausedVelocity;
        private Vector3 _pausedAngularVelocity;
        
        
        //  Unity Methods  --------------------------------
        
        //  Methods ---------------------------------------
        
        //  Event Handlers --------------------------------
    }
}