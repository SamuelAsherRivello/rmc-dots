using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace RMC.DOTS.Systems.PhysicsVelocityImpulse
{
    [CustomEditor(typeof(PhysicsVelocityImpulseComponentAuthoring))]
    public class PhysicsVelocityImpulseComponentAuthoringEditor : Editor
    {
        public VisualTreeAsset VisualTreeAsset;
        private VisualElement _visualElement;
        private PropertyField _isRandomForcePropertyField;

        public override VisualElement CreateInspectorGUI()
        {
            _visualElement = new VisualElement();
            VisualTreeAsset.CloneTree(_visualElement);

            // Listen
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