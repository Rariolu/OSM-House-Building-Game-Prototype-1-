using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR

public class CreatingSnapLauncher : ScriptableObject
{
    [MenuItem("Tools/SnapPoints/CreateLaunchers")]
    static void DoIt()
    {
        SnapPointTrigger[] snapPointTriggers = (SnapPointTrigger[])FindObjectsOfType(typeof(SnapPointTrigger));
        foreach(SnapPointTrigger trigger in snapPointTriggers)
        {
            SnapPointLauncher launcher = trigger.gameObject.GetComponent<SnapPointLauncher>() ?? trigger.gameObject.AddComponent<SnapPointLauncher>();
            launcher.snapType = trigger.snapType;
            DestroyImmediate(trigger);
        }
        EditorUtility.DisplayDialog("Done", "Created snap point launchers.", "OK");
    }
}

#endif