using UnityEngine;
using System.Collections;
using UnityEditor;
using FH.DevTool;

namespace FH.Core.Architecture.ParametersTable
{
    [CustomPropertyDrawer(typeof(BoolParameterGetter))]
    [CustomPropertyDrawer(typeof(FloatParameterGetter))]
    [CustomPropertyDrawer(typeof(IntParameterGetter))]
    [CustomPropertyDrawer(typeof(StringParameterGetter))]
    public class ParameterGetterInspector : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            float maxWidth = position.width;

            float propertyLabelWidth = maxWidth * 0.4f;
            float useKeyPropertyWidth = maxWidth * 0.2f;
            float keyPropertyWidth = maxWidth * 0.2f;
            float defaulValuePropertyWidth = maxWidth * 0.2f;
            float valuePropertyWidth = maxWidth * 0.4f;

            VerticalGUILayout rectGenerator = new VerticalGUILayout(position);

            SerializedProperty keyProperty = property.FindPropertyRelative("key");
            SerializedProperty useKeyProperty = property.FindPropertyRelative("useKey");
            SerializedProperty valueProperty = property.FindPropertyRelative("value");
            SerializedProperty defaultValueProperty = property.FindPropertyRelative("defaultValue");

            EditorGUI.BeginProperty(position, label, property);
            EditorGUI.BeginChangeCheck();

            EditorGUI.LabelField(rectGenerator.GetNextRect(propertyLabelWidth), property.name);

            int useKey = useKeyProperty.boolValue ? 1 : 0;
            useKey = EditorGUI.Popup(rectGenerator.GetNextRect(useKeyPropertyWidth), useKey, new string[] { "Use value", "Use key" });
            useKeyProperty.boolValue = useKey == 1;

            if (useKey == 1)
            {
                EditorGUI.PropertyField(rectGenerator.GetNextRect(keyPropertyWidth), keyProperty, GUIContent.none);
                InspectorHelper.DrawFixedWidthLabelPropertyField(rectGenerator.GetNextRect(defaulValuePropertyWidth), defaultValueProperty, 15, "D");
            }
            else
            {
                InspectorHelper.DrawFixedWidthLabelPropertyField(rectGenerator.GetNextRect(valuePropertyWidth), valueProperty, 50, "Value");
            }

            if (EditorGUI.EndChangeCheck())
            {
                //
            }
            EditorGUI.EndProperty();
        }
    }

}