using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

#if UNITY_EDITOR

public class SetPrefabIconIndexes : ScriptableObject
{
    [MenuItem("Tools/PrefabIcons/SetIndexes")]
    static void SetPrefabIndexes()
    {
        //EditorUtility.DisplayDialog("MyTool", "Do It in C# !", "OK", "");
        PrefabIconScript[] icons = FindObjectsOfType<PrefabIconScript>();
        foreach(PrefabIconScript icon in icons)
        {
            string name = icon.gameObject.name;

            int openBracketIndex = name.LastIndexOf('(');
            int closeBracketIndex = name.LastIndexOf(')');
            if (openBracketIndex > -1 && closeBracketIndex > -1 && closeBracketIndex > openBracketIndex)
            {
                int startIndex = openBracketIndex + 1;
                int length = closeBracketIndex - startIndex;
                string numStr = name.Substring(startIndex, length);
                int n;
                if (int.TryParse(numStr, out n))
                {
                    icon.index = n;
                }
                else
                {
                    Logger.Log("Num str was {0}.", numStr);
                }
            }
        }
    }
    
    [MenuItem("Tools/PrefabIcons/SetCounterLabels")]
    static void SetCounterLabels()
    {
        PrefabIconScript[] icons = FindObjectsOfType<PrefabIconScript>();
        foreach (PrefabIconScript icon in icons)
        {
            Text label = icon.GetComponentInChildren<Text>();
            icon.lblCounter = label;
            if (label != null)
            {
                label.name = "lblPrefabIconCounter ({0})".FormatText(icon.index);
            }
        }
    }
}

#endif