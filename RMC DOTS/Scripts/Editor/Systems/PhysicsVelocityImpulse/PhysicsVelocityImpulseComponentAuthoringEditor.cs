using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace RMC.DOTS.Systems.PhysicsVelocityImpulse
{
    [CustomEditor(typeof(PhysicsVelocityImpulseComponentAuthoring))]
    public class PhysicsVelocityImpulseComponentAuthoringEditor : BaseCustomEditor
    {
        private VisualElement _visualElement;
        private PropertyField _isRandomForcePropertyField;

        private const bool IsInDevelopment = false;

        public override VisualElement CreateInspectorGUI()
        {
            // Create new VisualElement
            _visualElement = new VisualElement();
         
            if (IsInDevelopment)
            {
#pragma warning disable CS0162
                // Debug-only: Keep commented out
                AddDefaultInspector(ref _visualElement);
                AddVerticalGap(ref _visualElement);
#pragma warning restore CS0162
            }
            
            // Add default Script Field
            AddScriptField(ref _visualElement);
            
            // Clone serialized VisualElement and StyleSheet
            MyVisualTreeAsset.CloneTree(_visualElement);
            _visualElement.styleSheets.Add(MyStyleSheet);
            
            // Observer Events
            _isRandomForcePropertyField = _visualElement.Q<PropertyField>("IsRandomForcePropertyField");
            _isRandomForcePropertyField.RegisterCallback<ClickEvent>(IsRandomForcePropertyField_OnClick);
            IsRandomForcePropertyField_OnClick(null);
            
            return _visualElement;
        }



        private void IsRandomForcePropertyField_OnClick(ClickEvent evt)
        {
            var isRandomForceProperty = _isRandomForcePropertyField.bindingPath;
            var isRandomForcePropertySerializedProperty = serializedObject.FindProperty(isRandomForceProperty);

            var nonRandomVisualElement = _visualElement.Q<VisualElement>("NonRandomVisualElement");
            var randomVisualElement = _visualElement.Q<VisualElement>("RandomVisualElement");

            if (isRandomForcePropertySerializedProperty.boolValue)
            {
                nonRandomVisualElement.style.display = DisplayStyle.None;
                randomVisualElement.style.display = DisplayStyle.Flex;
            }
            else
            {
                nonRandomVisualElement.style.display = DisplayStyle.Flex;
                randomVisualElement.style.display = DisplayStyle.None;
            }
        }
    }
}