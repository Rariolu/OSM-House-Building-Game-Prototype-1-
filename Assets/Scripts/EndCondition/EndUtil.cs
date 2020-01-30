using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class EndUtil
{
    /// <summary>
    /// Determines whether or not the contract has been fulfilled.
    /// </summary>
    /// <param name="contract">The original contract definition</param>
    /// <param name="takenPositions">The positions that have been taken by prefabs</param>
    /// <param name="placedPrefabs">The prefabs that have been placed on the scene</param>
    /// <returns></returns>
    public static bool ContractFullfilled(Contract contract, Dictionary<SNAP_POINT_TYPE, List<Vector3>> takenPositions, Stack<PrefabPlacedObject> placedPrefabs)
    {
        List<Vector3> positionsTaken = contract.positionsTaken.ToList();
        int count = 0;
        foreach(SNAP_POINT_TYPE key in takenPositions.Keys)
        {
            List<Vector3> tps = takenPositions[key];
            foreach(Vector3 v3 in tps)
            {
                //Check if the contracts set of positions contains the placed position
                if (!positionsTaken.Contains(v3))
                {
                    Debug.Log("Line 25");
                    return false;
                }
            }
            
            count += tps.Count;
        }

        //Check that the amount of taken positions matches the quantity in the contract.
        if (count != positionsTaken.Count)
        {
            Debug.Log("Line 36");
            return false;
        }

        Dictionary<Prefab, Vector3[]> contractPrefabLocations = new Dictionary<Prefab, Vector3[]>();
        foreach(PrefabCollection pc in contract.prefabCollections)
        {
            contractPrefabLocations.Add(pc.prefab, pc.positionsTakenWithinContract);
        }

        Dictionary<Prefab,List<Vector3>> placedPrefabLocations = new Dictionary<Prefab,List<Vector3>>();
        while(placedPrefabs.Count > 0)
        {
            PrefabPlacedObject ppo = placedPrefabs.Pop();
            Prefab prefab = ppo.Prefab;
            Vector3 position = ppo.RoundedPosition;
            if (placedPrefabLocations.ContainsKey(prefab))
            {
                placedPrefabLocations[prefab].Add(position);
            }
            else
            {
                placedPrefabLocations.Add(prefab, new List<Vector3>() { position });
            }
        }

        foreach(Prefab key in contractPrefabLocations.Keys)
        {
            //Check if the placed prefabs contain the selected one from the contract.
            if (!placedPrefabLocations.ContainsKey(key))
            {
                Debug.Log("Line 67");
                return false;
            }
            List<Vector3> prefabPositions = contractPrefabLocations[key].ToList();
            List<Vector3> placedPositions = placedPrefabLocations[key];
            //Check that the amount of unique positions for this prefab in equivalent to
            //what is used in the scene
            if (prefabPositions.Count != placedPositions.Count)
            {
                Debug.Log("Line 76");
                return false;
            }
            for (int i = 0; i < prefabPositions.Count; i++)
            {
                //Check that the placed positions for this prefab
                //contains the same 
                if (!placedPositions.Contains(prefabPositions[i]))
                {
                    Debug.Log("Line 85");
                    return false;
                }
            }
        }
        return true;
    }
}