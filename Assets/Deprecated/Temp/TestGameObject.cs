using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

public class TestGameObject : MonoBehaviour
{
    public string meshName;
    public string materialArrName;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }
    //internal static void ExtractMaterialsFromAsset(Object[] targets, string destinationPath)
    //{
    //    HashSet<string> assetsToReload = new HashSet<string>();
    //    foreach (var t in targets)
    //    {
    //        var importer = t as AssetImporter;

    //        var materials = AssetDatabase.LoadAllAssetsAtPath(importer.assetPath).Where(x => x.GetType() == typeof(Material)).ToArray();

    //        foreach (var material in materials)
    //        {
    //            var newAssetPath = FileUtil.CombinePaths(destinationPath, material.name) + kMaterialExtension;
    //            newAssetPath = AssetDatabase.GenerateUniqueAssetPath(newAssetPath);

    //            var error = AssetDatabase.ExtractAsset(material, newAssetPath);
    //            if (String.IsNullOrEmpty(error))
    //            {
    //                assetsToReload.Add(importer.assetPath);
    //            }
    //        }
    //    }

    //    foreach (var path in assetsToReload)
    //    {
    //        AssetDatabase.WriteImportSettingsIfDirty(path);
    //        AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);
    //    }
    //}
}