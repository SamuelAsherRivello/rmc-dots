using RMC.DOTS.Samples.Pong2D.Shared.UI;
using UnityEngine;

namespace RMC.DOTS.Samples.Pong2D.Shared
{
    //  Namespace Properties ------------------------------

    //  Class Attributes ----------------------------------

    /// <summary>
    /// Common world elements
    /// </summary>
    public class Common: MonoBehaviour
    {
        //  Events ----------------------------------------

        
        //  Properties ------------------------------------
        public MainUI MainUI { get { return _mainUI; }}

        
        //  Fields ----------------------------------------
        [SerializeField]
        private MainUI _mainUI;
        
        
        //  Unity Methods  -------------------------------
        
        
        //  Methods ---------------------------------------
        
        
        //  Event Handlers --------------------------------
    }
}