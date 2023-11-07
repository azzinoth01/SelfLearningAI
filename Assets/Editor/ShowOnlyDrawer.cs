//===================================================
//  Copyright @ Markus Dullnig 2023
//  Author：Markus Dullnig
//  Time：2023-11-07 20:33:49
//  GitUser: azzinoth01
//===================================================
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(ShowOnlyAttribute))]
public class ShowOnlyDrawer : PropertyDrawer {
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
        string value;

        switch (property.propertyType) {
            case SerializedPropertyType.Integer:
                value = property.intValue.ToString();
                break;
            case SerializedPropertyType.Boolean:
                value = property.boolValue.ToString();
                break;
            case SerializedPropertyType.Float:
                value = property.floatValue.ToString("0.00000");
                break;
            case SerializedPropertyType.String:
                value = property.stringValue;
                break;
            default:
                value = "(not supported)";
                break;
        }

        EditorGUI.LabelField(position, label.text, value);
    }
}
