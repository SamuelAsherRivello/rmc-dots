using RMC.DOTS.Systems.PhysicsVelocityImpulse;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace RMC.DOTS.Systems.Audio
{
    [CustomEditor(typeof(AudioComponentAuthoring))]
    public class AudioComponentAuthoringEditor : BaseCustomEditor
    {
        private VisualElement _visualElement;
        private PropertyField _isRandomForcePropertyField;

        public override VisualElement CreateInspectorGUI()
        {
            // Create new VisualElement
            _visualElement = new VisualElement();
            
            // Add default Script Field
            AddScriptField(ref _visualElement);
            
            // Clone serialized VisualElement and StyleSheet
            MyVisualTreeAsset.CloneTree(_visualElement);
            _visualElement.styleSheets.Add(MyStyleSheet);
            
            return _visualElement;
        }
    }
}