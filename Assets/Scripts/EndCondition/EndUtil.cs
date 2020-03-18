using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class EndUtil
{
    public static bool Pass()
    {
        ConstructionUtil constructionUtil;
        if (!ConstructionUtil.InstanceAvailable(out constructionUtil))
        {
            //There isn't a construction util so default
            //fail.
            return false;
        }
        Contract contract = constructionUtil.Contract;
        if (!PrefabsInCorrectPosition(contract))
        {
            //The prefabs aren't in the correct position.
            return false;
        }

        return true;
    }
    static bool PrefabsInCorrectPosition(Contract contract)
    {
        PrefabPlacedObject[] placedPrefabs = PrefabPlacedObject.Values;
        Dictionary<Prefab, List<Vector3>> prefabPosMap = contract.GetPrefabPositions();
        foreach (PrefabPlacedObject placedPrefab in placedPrefabs)
        {
            Prefab prefab = placedPrefab.Prefab;
            Vector3 placedPosition = placedPrefab.RoundedPosition;
            bool prefabInContract = prefabPosMap.ContainsKey(prefab);
            bool placedPositionInContract = prefabInContract && prefabPosMap[prefab].Contains(placedPosition);
            if (!placedPositionInContract)
            {
                Debug.LogWarningFormat("Prefab In contract: {0}; Position in contract: {1};", prefabInContract, placedPositionInContract);
                return false;
            }
            prefabPosMap[prefab].RemoveAll(p => p == placedPosition);
            if (prefabPosMap[prefab].Count < 1)
            {
                prefabPosMap.Remove(prefab);
            }
        }

        if (prefabPosMap.Count > 0)
        {
            Debug.LogWarning("Pos map still got some shit");
            return false;
        }

        return true;
    }
}