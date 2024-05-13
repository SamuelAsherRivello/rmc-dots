using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace RMC.DOTS.Systems.PhysicsVelocityImpulse
{
    public class BaseCustomEditor : Editor
    {
        // Single-click the MonoBehavior and set this through the Inspector
        public VisualTreeAsset MyVisualTreeAsset;
        public StyleSheet MyStyleSheet;
        
        /// <summary>
        /// Optional: Recommended to mimic the typical unity 'Script' field at the top
        /// </summary>
        protected void AddScriptField(ref VisualElement visualElement)
        {
            // Add the Script property field at the top
            var scriptProperty = serializedObject.FindProperty("m_Script");
            PropertyField scriptField = new PropertyField(scriptProperty);
            scriptField.SetEnabled(false); 
            visualElement.Add(scriptField);
        }
        
        /// <summary>
        /// Optional: This is great for debugging to compare your WIP
        /// UI Toolkit editor with the original Unity Inspector
        /// </summary>
        protected void AddDefaultInspector(ref VisualElement visualElement)
        {
            InspectorElement.FillDefaultInspector( visualElement , serializedObject , this );
        }
        
        /// <summary>
        /// Optional: This is great for debugging 
        /// </summary>
        protected void AddVerticalGap(ref VisualElement visualElement)
        {
            var verticalGap = new VisualElement();
            verticalGap.style.height = new StyleLength(new Length(20, LengthUnit.Pixel));
            verticalGap.style.width = new StyleLength(new Length(100, LengthUnit.Percent));
            visualElement.Add(verticalGap);
        }
        
    }
}