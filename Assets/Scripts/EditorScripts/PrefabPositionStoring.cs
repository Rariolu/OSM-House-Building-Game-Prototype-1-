using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

#if UNITY_EDITOR

public class PrefabPositionStoring : ScriptableObject
{
    const string xmlFile = "Assets\\Contracts\\Semi-Detached House_SEMI_DETACHED_HOUSE_0.xml";
    [MenuItem("Tools/Prefabs/SavePrefabs")]
    static void SavePrefabPositions()
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
                    Vector3 pos = placedPrefab.parentPrefabInstance.RoundedPosition;
                    if (!prefabPositions.ContainsKey(prefab))
                    {
                        prefabPositions.Add(prefab, new List<Vector3>() { pos });
                    }
                    else
                    {
                        prefabPositions[prefab].Add(pos);
                    }
                }
                for(int i = 0; i < contract.prefabCollections.Length; i++)
                {
                    Prefab colPrefab = contract.prefabCollections[i].prefab;
                    if (prefabPositions.ContainsKey(colPrefab))
                    {
                        contract.prefabCollections[i].positionsTakenWithinContract = prefabPositions[colPrefab].ToArray();
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
}

#endif