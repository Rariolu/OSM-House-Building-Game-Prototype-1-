using UnityEngine;
using UnityEditor;
using System;
using System.Collections;

#if UNITY_EDITOR

[CustomPropertyDrawer(typeof(PrefabResource))]
public class PrefabLoaderPropertyDrawer : AlternateNamePropertyDrawer
{
    protected override string GetName(SerializedProperty property)
    {
        PREFABTYPE prefabType = (PREFABTYPE)property.FindPropertyRelative("type").enumValueIndex;
        return prefabType.ToString();
    }
}

#endif