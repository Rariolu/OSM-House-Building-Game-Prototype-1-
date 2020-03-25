using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using UnityObj = UnityEngine.Object;

#if UNITY_EDITOR

[CustomPropertyDrawer(typeof(PrefabCollection))]
public class PrefabPropertyDrawer : PropertyDrawer
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
            SerializedProperty prefabProp = property.FindPropertyRelative("prefab");
            SerializedProperty prefabTypeProp = prefabProp.FindPropertyRelative("type");

            int enumVal = prefabTypeProp.enumValueIndex;
            PREFABTYPE prefabType = (PREFABTYPE)prefabTypeProp.enumValueIndex;

            EditorGUI.PropertyField(rect, property, new GUIContent(prefabType.ToString()), true);
        }
        catch(Exception err)
        {
            Logger.Log("Contract drawer fail: {0};", err.Message);
            EditorGUI.PropertyField(rect, property, label, true);
        }

        EditorGUI.EndProperty();
    }
}

#endif