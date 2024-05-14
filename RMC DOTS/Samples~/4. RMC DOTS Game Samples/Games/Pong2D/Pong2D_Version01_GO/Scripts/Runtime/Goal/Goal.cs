using UnityEngine;

namespace RMC.DOTS.Samples.Pong2D.Pong2D_Version01_GO
{
    //  Namespace Properties ------------------------------

    //  Class Attributes ----------------------------------
    
    /// <summary>
    /// </summary>
    public class Goal : MonoBehaviour
    {
        //  Events ----------------------------------------

        //  Properties ------------------------------------
        public PlayerType PlayerType { get { return _playerType; } }
        
        //  Fields ----------------------------------------
        [SerializeField] 
        private PlayerType _playerType;
        
        //  Unity Methods  --------------------------------
        protected void Start()
        {
         
        }
        
        //  Methods ---------------------------------------
        public virtual void Move()
        {
            
        }
        
        //  Event Handlers --------------------------------
    }
}