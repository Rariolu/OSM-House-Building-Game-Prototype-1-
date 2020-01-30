#pragma warning disable IDE0017
#pragma warning disable IDE0018
#pragma warning disable IDE0028
#pragma warning disable IDE0039
#pragma warning disable IDE0044
#pragma warning disable IDE1005

#if PREFABPANEL

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime;
using UnityEngine;

public class PrefabPanel : NullableInstanceScriptSingleton<PrefabPanel>
{
    Dictionary<FLOORTYPE, List<PrefabIconMultipler>> separatedFloorPrefabIcons = new Dictionary<FLOORTYPE, List<PrefabIconMultipler>>();
    Dictionary<FLOORTYPE, Dictionary<Prefab, PrefabIconMultipler>> separateFloorPrefabIconDict = new Dictionary<FLOORTYPE, Dictionary<Prefab, PrefabIconMultipler>>();
    Dictionary<Prefab, PrefabCollection> prefabCollections = new Dictionary<Prefab, PrefabCollection>();
    public float a = 0.25f;
    public float y = 0.05f;
    FLOORTYPE currentFloorType = FLOORTYPE.GROUND_FLOOR;
    // Start is called before the first frame update
    void Awake()
    {
        SetInstance(this);

        bool defaultUINotRequired;
        ConstructionUtil util;
        if (defaultUINotRequired = ConstructionUtil.InstanceAvailable(out util))
        {
            PrefabCollection[] collections = util.Contract.prefabCollections;
            if (defaultUINotRequired = collections.Length > 0)
            {
                Dictionary<FLOORTYPE, float> xVals = new Dictionary<FLOORTYPE, float>()
                {
                    {FLOORTYPE.GROUND_FLOOR,0f},
                    {FLOORTYPE.FIRST_FLOOR,0f},
                    {FLOORTYPE.SECOND_FLOOR,0f}
                };
                Dictionary<FLOORTYPE, int> countVals = new Dictionary<FLOORTYPE, int>()
                {
                    {FLOORTYPE.GROUND_FLOOR,0},
                    {FLOORTYPE.FIRST_FLOOR,0},
                    {FLOORTYPE.SECOND_FLOOR,0}
                };
                
                for (int i = 0; i < collections.Length; i++)
                {
                    PrefabCollection prefabCollection = collections[i];
                    Prefab prefab = prefabCollection.prefab;
                    prefabCollections.Add(prefabCollection.prefab, prefabCollection);
                    PrefabIconMultipler icon = new PrefabIconMultipler(prefabCollection,countVals[prefab.floorType]++);
                    icon.Click += Icon_Click;
                    icon.SetParent(transform);
                    icon.SetPosition(new Vector2(xVals[prefab.floorType], y));

                    if (separatedFloorPrefabIcons.ContainsKey(prefab.floorType))
                    {
                        separatedFloorPrefabIcons[prefab.floorType].Add(icon);
                    }
                    else
                    {
                        separatedFloorPrefabIcons.Add(prefab.floorType, new List<PrefabIconMultipler>() { icon });
                    }

                    if (separateFloorPrefabIconDict.ContainsKey(prefab.floorType))
                    {
                        separateFloorPrefabIconDict[prefab.floorType].Add(prefab,icon);
                    }
                    else
                    {
                        Dictionary<Prefab, PrefabIconMultipler> floorIcons = new Dictionary<Prefab, PrefabIconMultipler>();
                        floorIcons.Add(prefab, icon);
                        separateFloorPrefabIconDict.Add(prefab.floorType, floorIcons);
                    }
                    xVals[prefab.floorType] += a;
                }
            }
        }
        SetCurrentFloor(currentFloorType);
        //This will only occur if there's no construction util OR there aren't any prefabs.
        if (!defaultUINotRequired)
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Add prefab to panel.
    /// </summary>
    /// <param name="prefab"></param>
    public void AddPrefab(Prefab prefab)
    {
        if (separateFloorPrefabIconDict.ContainsKey(prefab.floorType))
        {
            if (separateFloorPrefabIconDict[prefab.floorType].ContainsKey(prefab))
            {
                separateFloorPrefabIconDict[prefab.floorType][prefab].Count++;
            }
            else
            {
                int count = separatedFloorPrefabIcons.ContainsKey(prefab.floorType) ? separatedFloorPrefabIcons[prefab.floorType].Count : 0;
                float x = count * a;
                PrefabIconMultipler pim = new PrefabIconMultipler(prefabCollections[prefab], count);
                pim.Count = 1;
                pim.Click += Icon_Click;
                pim.SetParent(transform);
                pim.SetPosition(new Vector2(x, y));

                if (separatedFloorPrefabIcons.ContainsKey(prefab.floorType))
                {
                    separatedFloorPrefabIcons[prefab.floorType].Add(pim);
                }
                else
                {
                    separatedFloorPrefabIcons.Add(prefab.floorType, new List<PrefabIconMultipler>() { pim });
                }
                if (separateFloorPrefabIconDict.ContainsKey(prefab.floorType))
                {
                    separateFloorPrefabIconDict[prefab.floorType].Add(prefab, pim);
                }
                else
                {
                    separateFloorPrefabIconDict.Add(prefab.floorType, new Dictionary<Prefab, PrefabIconMultipler>() { { prefab, pim } });
                }
            }
        }
    }

    public void SetCurrentFloor(FLOORTYPE floor)
    {
        currentFloorType = floor;
        foreach(FLOORTYPE key in separatedFloorPrefabIcons.Keys)
        {
            foreach (PrefabIconMultipler icon in separatedFloorPrefabIcons[key])
            {
                icon.SetActive(key == floor);
            }
        }
    }

    void Icon_Click(PrefabIconMultipler icon)
    {
        buildSystem bs;
        if (buildSystem.InstanceAvailable(out bs))
        {
            Prefab prev;
            if (bs.PrefabAvailable(out prev))
            {
                AddPrefab(prev);
            }
            //if (!bs.PrefabSet)
            {
                Prefab prefab = icon.Prefab;
                bs.SetCurrentPrefab(prefab);

                PrefabView pv;
                if (PrefabView.InstanceAvailable(out pv))
                {
                    pv.SetPrefab(prefab);
                }
                icon.Count--;
                if (icon.Count < 1)
                {
                    RemoveIcon(icon.ID);
                }
            }
        }


    }

    void RemoveIcon(int id)
    {
        PrefabIconMultipler picon = separatedFloorPrefabIcons[currentFloorType][id];
        picon.Dispose();
        separateFloorPrefabIconDict[currentFloorType].Remove(picon.Prefab);
        separatedFloorPrefabIcons[currentFloorType].RemoveAt(id);
        float x = id * a;
        for (int i = id; i < separatedFloorPrefabIcons[currentFloorType].Count; i++)
        {
            PrefabIconMultipler icon = separatedFloorPrefabIcons[currentFloorType][i];
            icon.ID--;
            icon.SetPosition(new Vector2(x, y));
            x += a;
        }
    }
}

#endif