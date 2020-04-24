using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

#if UNITY_EDITOR

public class PrefabPositionStoring : ScriptableObject
{
    //const string xmlFile = "Assets\\Contracts\\Semi-Detached House_SEMI_DETACHED_HOUSE_0.xml";
    [MenuItem("Tools/Prefabs/SavePrefabsC1")]
    static void SavePrefabPositionsC1()
    {
        string xmlFile = "Assets\\Contracts\\{0}.xml".FormatText("Semi-Detached House_SEMI_DETACHED_HOUSE_0");
        SavePrefabPositions(xmlFile);
    }

    [MenuItem("Tools/Prefabs/SavePrefabsC2")]
    static void SavePrefabPositionsC2()
    {
        string xmlFile = "Assets\\Contracts\\{0}\\.xml".FormatText("Detached House_DETACHED_HOUSE_0");
        SavePrefabPositions(xmlFile);
    }

    static void SavePrefabPositions(string xmlFile)
    {
        Contract contract;
        if (XMLUtil.LoadContract(xmlFile, out contract))
        {
            if (EditorUtility.DisplayDialog("Save", "Save the prefab positions in the scene?", "OK", "Cancel"))
            {
                PrefabPlacementScript[] placedPrefabs = FindObjectsOfType<PrefabPlacementScript>();
                Dictionary<Prefab, List<Vector3>> prefabPositions = new Dictionary<Prefab, List<Vector3>>();
                foreach (PrefabPlacementScript placedPrefab in placedPrefabs)
                {
                    Prefab prefab = placedPrefab.Prefab;
                    Vector3 pos = placedPrefab.parentPrefabInstance.InitialiserPosition;
                    if (!prefabPositions.ContainsKey(prefab))
                    {
                        prefabPositions.Add(prefab, new List<Vector3>() { pos });
                    }
                    else
                    {
                        prefabPositions[prefab].Add(pos);
                    }
                }
                for (int i = 0; i < contract.prefabCollections.Length; i++)
                {
                    Prefab colPrefab = contract.prefabCollections[i].prefab;
                    if (prefabPositions.ContainsKey(colPrefab))
                    {
                        List<Vector3> originalPositionList = contract.prefabCollections[i].positionsTakenWithinContract.ToList();
                        List<Vector3> newPositionList = prefabPositions[colPrefab];
                        contract.prefabCollections[i].positionsTakenWithinContract = Util.MergeLists(originalPositionList, newPositionList).ToArray();
                        //contract.prefabCollections[i].positionsTakenWithinContract = prefabPositions[colPrefab].ToArray();
                    }
                    //else
                    //{
                    //    contract.prefabCollections[i].positionsTakenWithinContract = new Vector3[0];
                    //}
                }
                XMLUtil.SaveContract(xmlFile, contract);
                EditorUtility.DisplayDialog("Positions Saved", string.Format("Positions saved in \"{0}\".", xmlFile), "K");
            }
        }
        else
        {
            Logger.Log("XML file not parsed.");
        }
    }

    [MenuItem("Tools/Prefabs/ResetC1Prefabs")]
    static void ResetPrefabPositionsC1()
    {
        string xmlFile = "Assets\\Contracts\\{0}.xml".FormatText("Semi-Detached House_SEMI_DETACHED_HOUSE_0");
        ResetPrefabPositions(xmlFile);
    }

    [MenuItem("Tools/Prefabs/ResetC2Prefabs")]
    static void ResetPrefabPositionsC2()
    {
        string xmlFile = "Assets\\Contracts\\{0}\\.xml".FormatText("Detached House_DETACHED_HOUSE_0");
        ResetPrefabPositions(xmlFile);
    }

    static void ResetPrefabPositions(string xmlFile)
    {
        if (EditorUtility.DisplayDialog("Reset Positions", "Reset positions of {0}.".FormatText(xmlFile), "K", "cancel"))
        {
            Contract contract;
            if (XMLUtil.LoadContract(xmlFile, out contract))
            {
                for (int i = 0; i < contract.prefabCollections.Length; i++)
                {
                    contract.prefabCollections[i].positionsTakenWithinContract = new Vector3[0];
                }
                XMLUtil.SaveContract(xmlFile, contract);
            }
           
        }
    }
}

#endif