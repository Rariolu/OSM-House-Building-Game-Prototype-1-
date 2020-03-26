using UnityEngine;
using UnityEditor;
using System;
using System.Collections;


[CustomPropertyDrawer(typeof(PrefabResource))]
public class PrefabLoaderPropertyDrawer : PropertyDrawer
{
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUI.GetPropertyHeight(property, label);
    }
    public override void OnGUI(Rect rect, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(rect, label, property);

        try
        {
            PREFABTYPE prefabType = (PREFABTYPE)property.FindPropertyRelative("type").enumValueIndex;
            EditorGUI.PropertyField(rect, property, new GUIContent(prefabType.ToString()), true);
        }
        catch(Exception err)
        {
            Logger.Log("PrefabResource drawer fail: {0};", err.Message);
            EditorGUI.PropertyField(rect, property, label, true);
        }

        EditorGUI.EndProperty();
    }
}
