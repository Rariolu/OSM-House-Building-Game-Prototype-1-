using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Linq;

#if UNITY_EDITOR

[CustomPropertyDrawer(typeof(Contract))]
public class ContractPropertyDrawer : AlternateNamePropertyDrawer
{
    protected override string GetName(SerializedProperty property)
    {
        try
        {
            string name = property.FindPropertyRelative("name").stringValue;
            return name;
        }
        catch
        {
            return "[error]";
        }
    }
}
//public class ContractPropertyDrawer : PropertyDrawer
//{
//    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
//    {
//        return EditorGUI.GetPropertyHeight(property, label);
//    }
//    public override void OnGUI(Rect rect, SerializedProperty property, GUIContent label)
//    {
//        EditorGUI.BeginProperty(rect, label, property);
//        try
//        {
//            string name = property.FindPropertyRelative("name").stringValue;
//            FINISHED_CONSTRUCTION finCon = (FINISHED_CONSTRUCTION)property.FindPropertyRelative("finishedConstruction").enumValueIndex;
//            string contractName = name;//string.Format("{0}_{1}", name, finCon);
//            EditorGUI.PropertyField(rect, property, new GUIContent(contractName), true);
//        }
//        catch(Exception err)
//        {
//            Logger.Log("Contract drawer fail: {0};", err.Message);
//            EditorGUI.PropertyField(rect, property, label, true);
//        }
//        EditorGUI.EndProperty();
//    }
//}

#endif