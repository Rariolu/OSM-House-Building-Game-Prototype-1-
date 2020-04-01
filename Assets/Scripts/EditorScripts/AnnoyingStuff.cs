using UnityEngine;
using UnityEditor;
using System.IO;

#if UNITY_EDITOR

public class AnnoyingStuff : ScriptableObject
{
    [MenuItem("Tools/Annoying Stuff/Delete Post-Processing folder")]
    static void DoIt()
    {
        const string dir = "Library\\PackageCache\\com.unity.postprocessing@2.0.3-preview";
        string fullPath = Path.GetFullPath(dir);
        if (Directory.Exists(dir))
        {
            
            if (EditorUtility.DisplayDialog("Post-Processing", "Delete annoying post-processing folder. "+fullPath, "Yes", "No"))
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(fullPath);
                directoryInfo.Delete(true);
                Logger.Log("\"{0}\" successfully deleted.",fullPath);
            }
        }
        else
        {
            Logger.Log(string.Format("\"{0}\" doesn't exist...", fullPath), LogType.Warning);
        }
    }
}

#endif