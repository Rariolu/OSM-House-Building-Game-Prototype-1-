#pragma warning disable IDE0017
#pragma warning disable IDE0018
#pragma warning disable IDE0044
#pragma warning disable IDE1005

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A script attached to a GameObject in the main game scene which manages its
/// operation and resources.
/// </summary>
public class InGameSceneScript : NullableInstanceScriptSingleton<InGameSceneScript>
{
    int availableFixtures;
    /// <summary>
    /// The quantity of remaining fixtures.
    /// </summary>
    public int AvailableFixtures
    {
        get
        {
            return availableFixtures;
        }
        set
        {
            availableFixtures = value;
        }
    }
    
    Contract currentContract;
    const int minBound = -10;
    const int maxBound = 10;
    Stack<PrefabPlacedObject> placedPrefabs = new Stack<PrefabPlacedObject>();
    const float spaceInterval = 5f;
    Dictionary<SNAP_POINT_TYPE, List<Vector3>> takenPositions = new Dictionary<SNAP_POINT_TYPE, List<Vector3>>();

    Dictionary<FLOORTYPE, Dictionary<Vector3, int>> intersectionMapping = new Dictionary<FLOORTYPE, Dictionary<Vector3, int>>();
    Dictionary<Vector3, Intersection> intersections = new Dictionary<Vector3, Intersection>();
    public string xmlBackupFile = "Assets\\Contracts\\Semi-Detached House_SEMI_DETACHED_HOUSE_0.xml";
    public bool AddIntersection(FLOORTYPE floor, Vector3 position, out Intersection intersection)
    {
        if (!intersectionMapping.ContainsKey(floor))
        {
            intersectionMapping.Add(floor, new Dictionary<Vector3, int>());
        }
        if (!intersectionMapping[floor].ContainsKey(position))
        {
            intersectionMapping[floor].Add(position, 0);
        }
        intersectionMapping[floor][position]++;
        if (intersectionMapping[floor][position] == 2)
        {
            intersection = new Intersection();
            Floor floorInst;
            
            intersection.SetPosition(position);
            if (Floor.InstanceExists(floor, out floorInst))
            {
                intersection.SetParent(floorInst.transform);
            }
            
            intersections.Add(position, intersection);
            return true;
        }
        intersection = intersections.ContainsKey(position) ? intersections[position] : null;
        return intersectionMapping[floor][position] > 1;
    }

    /// <summary>
    /// Add a placed object to the stack and store its position
    /// to be checked later.
    /// </summary>
    /// <param name="ppo"></param>
    public void AddPlacement(PrefabPlacedObject ppo)
    {
        placedPrefabs.Push(ppo);
        if (takenPositions.ContainsKey(ppo.Prefab.snapType))
        {
            takenPositions[ppo.Prefab.snapType].Add(ppo.RoundedPosition);
        }
        else
        {
            takenPositions.Add(ppo.Prefab.snapType, new List<Vector3>() { ppo.RoundedPosition });
        }
    }

    void Awake()
    {
        SetInstance(this);
        Util.PreventCollisions(LAYER.DEFAULT, LAYER.IntersectionLayer);
        ConstructionUtil util;
        if (ConstructionUtil.InstanceAvailable(out util))
        {
            currentContract = util.Contract;
            availableFixtures = currentContract.fixtures;
        }
#if UNITY_EDITOR
        else
        {
            Contract contract;
            Debug.LogFormat("Attempting to load \"{0}\".", xmlBackupFile);
            if (XMLUtil.LoadContract(xmlBackupFile, out contract))
            {
                Debug.Log("Loaded xml file.");
                ConstructionUtil.SetContract(contract);
            }
        }
#endif
    }

    public bool PositionTaken(Vector3 v3, SNAP_POINT_TYPE snapType)
    {
        if (!takenPositions.ContainsKey(snapType))
        {
            return false;
        }
        return takenPositions[snapType].Contains(v3);
    }

    public void RemovePrefabIntersectionPoint(FLOORTYPE floor, Vector3 position)
    {
        if (intersectionMapping.ContainsKey(floor))
        {
            if (intersectionMapping[floor].ContainsKey(position))
            {
                intersectionMapping[floor][position]--;
                if (intersectionMapping[floor][position] < 2)
                {
                    if (intersections.ContainsKey(position))
                    {
                        intersections[position].Destroy();
                        intersections.Remove(position);
                    }
                }
            }
        }
    }

    void Start()
    {
        Floor groundFloor;
        if (Floor.InstanceExists(FLOORTYPE.GROUND_FLOOR, out groundFloor))
        {
            groundFloor.Focus();
        }
        //Open UI scene over this one.
        Util.LoadGameUI();
    }

    public void Undo()
    {
        if (placedPrefabs.Count > 0)
        {
            PrefabPlacedObject ppo = placedPrefabs.Pop();
            ppo.SnapPointTrigger.Snapped = false;
            Prefab prefab = ppo.Prefab;
            takenPositions[ppo.Prefab.snapType].Remove(ppo.RoundedPosition);

            PrefabSelectionButton selectionButton;
            if (PrefabSelectionButton.InstanceExists(prefab.compart,out selectionButton))
            {
                selectionButton.AddPrefab(prefab);
            }

            ppo.Destroy();
        }
    }
}