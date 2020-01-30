#pragma warning disable IDE0017
#pragma warning disable IDE0018
#pragma warning disable IDE0039
#pragma warning disable IDE0044
#pragma warning disable IDE1005
#pragma warning disable IDE1006

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A class used to store the currently selected prefab and allow it to be
/// instantiated on request.
/// </summary>
public class buildSystem : NullableInstanceScriptSingleton<buildSystem>
{

    /// <summary>
    /// The currently selected prefab.
    /// </summary>
    Prefab prefabToConstruct;

    /// <summary>
    /// The snap type of the current prefab
    /// (used to make sure that the correct
    /// snap points are used).
    /// </summary>
    public SNAP_POINT_TYPE PrefabSnapType
    {
        get
        {
            return prefabToConstruct.snapType;
        }
    }
    bool prefabAssigned = false;
    /// <summary>
    /// Represents whether or not a prefab has been selected.
    /// </summary>
    public bool PrefabSet
    {
        get
        {
            return prefabAssigned;
        }
    }
    private void Awake()
    {
        SetInstance(this);
    }
    /// <summary>
    /// Returns true if a prefab has been selected
    /// and outputs given prefab (as well as forgetting
    /// about it so that a new one has to be set).
    /// </summary>
    /// <param name="prefab"></param>
    /// <returns></returns>
    public bool PrefabAvailable(out Prefab prefab)
    {
        prefab = prefabToConstruct;
        if (prefabAssigned)
        {
            prefabAssigned = false;
            return true;
        }
        return false;
    }
    /// <summary>
    /// Set the currently selected prefab.
    /// </summary>
    /// <param name="prefab"></param>
    public void SetCurrentPrefab(Prefab prefab)
    {
        prefabToConstruct = prefab;
        prefabAssigned = true;
        SnapPoint.ShowSnapPoints(prefab.snapType);
    }
}