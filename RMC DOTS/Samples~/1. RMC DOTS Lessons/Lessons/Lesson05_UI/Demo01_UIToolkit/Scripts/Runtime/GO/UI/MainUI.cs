using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

namespace RMC.DOTS.Lessons.UI.UIToolkit
{
    //  Namespace Properties ------------------------------

    //  Class Attributes ----------------------------------

    /// <summary>
    /// The View handles user interface and user input
    /// </summary>
    public class MainUI: MonoBehaviour
    {
        //  Events ----------------------------------------
        public readonly UnityEvent OnIncrementScore = new UnityEvent();

        //  Properties ------------------------------------
        public Label ScoreLabel { get { return _uiDocument?.rootVisualElement?.Q<Label>("ScoreLabel");} }
        public Label StatusLabel { get { return _uiDocument?.rootVisualElement?.Q<Label>("StatusLabel");} }
        public Button IncrementScoreButton { get { return _uiDocument?.rootVisualElement?.Q<Button>("IncrementScoreButton");} }

        //  Fields ----------------------------------------
        [SerializeField] 
        private UIDocument _uiDocument;
        

        //  Unity Methods  -------------------------------
        protected void Start()
        {
            IncrementScoreButton.clicked += IncrementScoreButtonOnClicked;
        }

        //  Methods ---------------------------------------
        
        
        //  Event Handlers --------------------------------
        private void IncrementScoreButtonOnClicked()
        {
            OnIncrementScore.Invoke();
        }
    }
}