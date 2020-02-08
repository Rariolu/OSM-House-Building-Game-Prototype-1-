using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void CounterChanged(Prefab prefab, int count);

/// <summary>
/// Singleton used to keep a count of the amount of each prefab that there is available.
/// </summary>
public class PrefabCounter : NullableInstanceClassSingleton<PrefabCounter>
{
    Dictionary<Prefab, int> availablePrefabCount = new Dictionary<Prefab, int>();
    public CounterChanged CounterChanged;
    bool prefabSelected = false;
    Prefab selectedPrefab;

    private PrefabCounter()
    {
        
    }

    public static void CreatePrefabCounter()
    {
        SetInstance(new PrefabCounter());
    }


    public int GetCount(Prefab prefab)
    {
        if (availablePrefabCount.ContainsKey(prefab))
        {
            return availablePrefabCount[prefab];
        }
        return 0;
    }

    public void IncrementCount(Prefab prefab)
    {
        int count = GetCount(prefab);
        SetCount(prefab, count + 1);
    }

    /// <summary>
    /// Reduces the count of a given prefab by 1.
    /// If the count reaches 0 and the given
    /// prefab is the selected one, it is unselected.
    /// </summary>
    /// <param name="prefab"></param>
    public void DecrementCount(Prefab prefab)
    {
        int count = GetCount(prefab);
        SetCount(prefab, count - 1);
        if (count-1 <= 0)
        {
            if (prefab == selectedPrefab)
            {
                prefabSelected = false;
            }
        }
    }

    public void SetCount(Prefab prefab, int count)
    {
        if (availablePrefabCount.ContainsKey(prefab))
        {
            availablePrefabCount[prefab] = count;
        }
        else
        {
            availablePrefabCount.Add(prefab, count);
        }
        if (CounterChanged != null)
        {
            CounterChanged(prefab, count);
        }
    }
    
    public bool PrefabAvailable(out Prefab prefab)
    {
        prefab = selectedPrefab;
        return prefabSelected;
    }


    public void SelectPrefab(Prefab prefab)
    {
        Prefab prev;
        if (PrefabAvailable(out prev))
        {
            SnapPoint.HideSnapPoints(prev.snapType);
        }
        selectedPrefab = prefab;
        prefabSelected = true;
        SnapPoint.ShowSnapPoints(prefab.snapType);
        PrefabView prefabView;
        if (PrefabView.InstanceAvailable(out prefabView))
        {
            prefabView.SetPrefab(prefab);
        }
    }
}