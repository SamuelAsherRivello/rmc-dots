using UnityEngine;
using UnityEngine.Events;

namespace RMC.DOTS.Samples.Pong2D.Pong2D_Version01_GO
{
    //  Namespace Properties ------------------------------

    //  Class Attributes ----------------------------------
    public class ProjectileUnityEvent : UnityEvent<Projectile, Goal> { }
    
    /// <summary>
    /// </summary>
    public class Projectile : MonoBehaviour
    {
        //  Events ----------------------------------------
        [HideInInspector]
        public ProjectileUnityEvent OnGoalHit = new ProjectileUnityEvent();

        //  Properties ------------------------------------
        public Rigidbody Rigidbody { get { return _rigidBody;}}
        
        //  Fields ----------------------------------------
        [SerializeField] 
        private Rigidbody _rigidBody;

        [SerializeField] 
        private float _speed = 10;
        
        //  Unity Methods  --------------------------------
        
        //  Methods ---------------------------------------
        public void AddForce(Vector3 force)
        {
            _rigidBody.AddForce(force * _speed, ForceMode.Impulse);
        }
        
        //  Event Handlers --------------------------------
        private void OnTriggerEnter(Collider other)
        {
            Goal goal = other.GetComponent<Goal>();
            if (goal != null)
            {
                OnGoalHit.Invoke(this, goal);
            }
        }
    }
}