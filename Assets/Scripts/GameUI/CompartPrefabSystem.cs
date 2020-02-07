using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompartPrefabSystem : NullableInstanceScriptSingleton<CompartPrefabSystem>
{
    void Awake()
    {
        SetInstance(this);
    }
    Dictionary<PREFAB_COMPART, Dictionary<FLOORTYPE, List<Prefab>>> compartedPrefabs = new Dictionary<PREFAB_COMPART, Dictionary<FLOORTYPE, List<Prefab>>>();
    void AddPrefabToDict(Prefab prefab)
    {
        if (compartedPrefabs.ContainsKey(prefab.compart))
        {
            if (compartedPrefabs[prefab.compart].ContainsKey(prefab.floorType))
            {
                compartedPrefabs[prefab.compart][prefab.floorType].Add(prefab);
            }
            else
            {
                compartedPrefabs[prefab.compart].Add(prefab.floorType, new List<Prefab>() { prefab });
            }
        }
        else
        {
            Dictionary<FLOORTYPE, List<Prefab>> newFloorDict = new Dictionary<FLOORTYPE, List<Prefab>>();
            newFloorDict.Add(prefab.floorType, new List<Prefab>() { prefab });
            compartedPrefabs.Add(prefab.compart, newFloorDict);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        ConstructionUtil util;
        PrefabCounter counter;
        if (ConstructionUtil.InstanceAvailable(out util) && PrefabCounter.InstanceAvailable(out counter))
        {
            PrefabCollection[] collections = util.Contract.prefabCollections;
            foreach (PrefabCollection collection in collections)
            {
                counter.SetCount(collection.prefab, collection.quantity);
                AddPrefabToDict(collection.prefab);
            }
            //Dictionary<PREFAB_COMPART, List<PrefabCollection>> prefabCollectionsComparted = new Dictionary<PREFAB_COMPART, List<PrefabCollection>>();
            //foreach(PrefabCollection collection in collections)
            //{

            //    PREFAB_COMPART pc = collection.prefab.compart;
            //    if (prefabCollectionsComparted.ContainsKey(pc))
            //    {
            //        prefabCollectionsComparted[pc].Add(collection);
            //    }
            //    else
            //    {
            //        prefabCollectionsComparted.Add(pc, new List<PrefabCollection>() { collection });
            //    }
            //}
            //foreach (PREFAB_COMPART key in PrefabSelectionButton.Keys)
            //{
            //    PrefabSelectionButton psButton;
            //    if (PrefabSelectionButton.InstanceExists(key, out psButton) && prefabCollectionsComparted.ContainsKey(key))
            //    {
            //        psButton.AddPrefabs(prefabCollectionsComparted[key]);
            //    }
            //}
        }

    }

    List<Prefab> GetPrefabs(PREFAB_COMPART compart, FLOORTYPE floor)
    {
        if (compartedPrefabs.ContainsKey(compart))
        {
            if (compartedPrefabs[compart].ContainsKey(floor))
            {
                return compartedPrefabs[compart][floor];
            }
        }
        return new List<Prefab>();
    }
    public void SwitchIcons(PREFAB_COMPART compart, FLOORTYPE floor)
    {
        List<Prefab> prefabs = GetPrefabs(compart, floor);
        List<PrefabIconScript> icons = PrefabIconScript.PrefabIcons;
        for (int i = 0; i < icons.Count; i++)
        {
            if (i < prefabs.Count)
            {
                icons[i].gameObject.SetActive(true);
                icons[i].Prefab = prefabs[i];
            }
            else
            {
                icons[i].gameObject.SetActive(false);
            }
        }
    }
}