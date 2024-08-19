using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(ConditionalFieldAttribute))]
public class ConditionalFieldPropertyDrawer : PropertyDrawer
{
    private const float Spacing = 6f;
    private readonly Color highlightColor = new Color(1f, 1f, 1f, 0.2f);

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        ConditionalFieldAttribute conditionalField = (ConditionalFieldAttribute)attribute;
        SerializedProperty conditionProperty = property.serializedObject.FindProperty(conditionalField.ConditionFieldName);

        if (conditionProperty == null)
        {
            return EditorGUI.GetPropertyHeight(property, label, true);
        }

        bool shouldDisplay = false;
        switch (conditionProperty.propertyType)
        {
            case SerializedPropertyType.Boolean:
                shouldDisplay = conditionProperty.boolValue.Equals(conditionalField.ExpectedValue);
                break;
            case SerializedPropertyType.Enum:
                shouldDisplay = conditionProperty.enumValueIndex.Equals((int)conditionalField.ExpectedValue);
                break;
            default:
                return EditorGUI.GetPropertyHeight(property, label, true);
        }

        return shouldDisplay ? EditorGUI.GetPropertyHeight(property, label, true) + Spacing * 2 : 0;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        ConditionalFieldAttribute conditionalField = (ConditionalFieldAttribute)attribute;
        SerializedProperty conditionProperty = property.serializedObject.FindProperty(conditionalField.ConditionFieldName);

        if (conditionProperty == null)
        {
            EditorGUI.LabelField(position, label.text, "Condition property not found.");
            return;
        }

        bool shouldDisplay = false;
        switch (conditionProperty.propertyType)
        {
            case SerializedPropertyType.Boolean:
                shouldDisplay = conditionProperty.boolValue.Equals(conditionalField.ExpectedValue);
                break;
            case SerializedPropertyType.Enum:
                shouldDisplay = conditionProperty.enumValueIndex.Equals((int)conditionalField.ExpectedValue);
                break;
            default:
                EditorGUI.LabelField(position, label.text, "Unsupported condition property type.");
                return;
        }

        if (shouldDisplay)
        {
            Rect adjustedPosition = new Rect(position.x, position.y + Spacing, position.width, position.height - Spacing);
            EditorGUI.DrawRect(adjustedPosition, highlightColor);
            EditorGUI.PropertyField(adjustedPosition, property, label, true);
        }
    }
}
