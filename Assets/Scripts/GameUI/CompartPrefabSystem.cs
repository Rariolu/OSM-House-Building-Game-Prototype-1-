using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompartPrefabSystem : MonoBehaviour//NullableInstanceScriptSingleton<CompartPrefabSystem>
{
    void Awake()
    {
        SingletonUtil.SetInstance(this);
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
        if (SingletonUtil.InstanceAvailable(out util) && SingletonUtil.InstanceAvailable(out counter))
        {
            PrefabCollection[] collections = util.Contract.prefabCollections;
            foreach (PrefabCollection collection in collections)
            {
                counter.SetCount(collection.prefab, collection.quantity);
                AddPrefabToDict(collection.prefab);
            }
            SwitchFloors(FLOORTYPE.GROUND_FLOOR);
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
    public void SwitchFloors(FLOORTYPE floor)
    {
        foreach (PREFAB_COMPART compart in Util.GetEnumValues<PREFAB_COMPART>())
        {
            List<Prefab> prefabs = GetPrefabs(compart, floor);
            PrefabIconGrid grid;
            if (PrefabIconGrid.InstanceExists(compart, out grid))
            {
                grid.SetPrefabs(prefabs);
            }
        }
    }

    public void SwitchCompart(PREFAB_COMPART compart)
    {
        foreach (PREFAB_COMPART c in Util.GetEnumValues<PREFAB_COMPART>())
        {
            PrefabIconGrid grid;
            if (PrefabIconGrid.InstanceExists(c,out grid))
            {
                grid.SetActive(c == compart);
            }
        }
    }
}