using UnityEngine.Events;
using UnityEngine.UIElements;

namespace RMC.DOTS.Samples.Games.TwinStickShooter3D.TwinStickShooter3D_Version02_DOTS
{
    //  Namespace Properties ------------------------------
    public class ConfirmUnityEvent : UnityEvent {}
    public class CancelUnityEvent : UnityEvent {}
    
    //  Class Attributes ----------------------------------

    /// <summary>
    /// Show an "Are You Sure?" Dialog Window
    /// </summary>
    public class DialogVisualElement
    {
        //  Events ----------------------------------------
        public readonly CancelUnityEvent OnCancel = new CancelUnityEvent();
        public readonly ConfirmUnityEvent OnConfirm = new ConfirmUnityEvent();
    
        //  Properties ------------------------------------
        public bool IsVisible
        {
            get
            {
                return _dialogVisualElement.style.display == new StyleEnum<DisplayStyle>(DisplayStyle.Flex);
            }
            set
            {
                if (value)
                {
                    _dialogVisualElement.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.Flex);
                }
                else
                {
                    _dialogVisualElement.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.None);
                }
            }
        }
        
        public Label BodyLabel { get { return _bodyLabel;} }
        
        //  Fields ----------------------------------------
        
        // Passed in
        private VisualElement _dialogVisualElement;
        
        // Queried
        private Label _bodyLabel;
        private Button _cancelButton;
        private Button _confirmButton;
        
        
        //  Methods ---------------------------------------
        public DialogVisualElement(VisualElement dialogVisualElement)
        {
            _dialogVisualElement = dialogVisualElement;
            _bodyLabel = dialogVisualElement.Q<Label>("BodyLabel");
            _cancelButton = dialogVisualElement.Q<Button>("CancelButton");
            _confirmButton = dialogVisualElement.Q<Button>("ConfirmButton");
            _cancelButton.clicked += CancelButton_OnClicked;
            _confirmButton.clicked += ConfirmButton_OnClicked;
        }
        
        
        //  Event Handlers --------------------------------
        private void CancelButton_OnClicked()
        {
            OnCancel.Invoke();
        }
        
        private void ConfirmButton_OnClicked()
        {
           OnConfirm.Invoke();
        }
    }
}