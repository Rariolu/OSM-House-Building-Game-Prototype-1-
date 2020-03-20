﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class EndConditionUtil
{
    public static EXIT_STATE Pass()
    {
        ConstructionUtil constructionUtil;
        if (!ConstructionUtil.InstanceAvailable(out constructionUtil))
        {
            Debug.LogWarning("There isn't a construction util so default fail.");
            return EXIT_STATE.LOSE;
        }
        Contract contract = constructionUtil.Contract;
        if (!PrefabsInCorrectPosition(contract))
        {
            Debug.LogWarning("The prefabs aren't in the correct position.");
            return EXIT_STATE.LOSE;
        }

        if (!FixingsInAllIntersections())
        {
            Debug.LogWarning("Not all the intersections have fixings.");
            return EXIT_STATE.LOSE;
        }

        return EXIT_STATE.WIN;
    }

    /// <summary>
    /// Returns true if the correct prefabs
    /// have been placed in the correct positions.
    /// </summary>
    /// <param name="contract"></param>
    /// <returns></returns>
    static bool PrefabsInCorrectPosition(Contract contract)
    {
        PrefabPlacedObject[] placedPrefabs = PrefabPlacedObject.Values;
        Dictionary<Prefab, List<Vector3>> prefabPosMap = contract.GetPrefabPositions();
        foreach (PrefabPlacedObject placedPrefab in placedPrefabs)
        {
            Prefab prefab = placedPrefab.Prefab;
            Vector3 placedPosition = placedPrefab.RoundedPosition;

            //Check if the prefab is in the contract
            //and check if the contract has that specific placed position.
            bool prefabInContract = prefabPosMap.ContainsKey(prefab);
            bool placedPositionInContract = prefabInContract && prefabPosMap[prefab].Contains(placedPosition);

            if (!placedPositionInContract)
            {
                Debug.LogWarningFormat("Prefab In contract: {0}; Position in contract: {1};", prefabInContract, placedPositionInContract);
                return false;
            }

            //Remove the position from the dictionary
            //and remove the prefab if there are no
            //more positions.
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

    static bool FixingsInAllIntersections()
    {
        Intersection[] intersections = Intersection.Values;
        foreach(Intersection intersection in intersections)
        {
            if (intersection.FixingSections.Count < 1)
            {
                return false;
            }
        }
        return true;
    }
}