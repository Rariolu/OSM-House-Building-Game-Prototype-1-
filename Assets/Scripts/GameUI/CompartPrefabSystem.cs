using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompartPrefabSystem : NullableInstanceScriptSingleton<CompartPrefabSystem>
{
    void Awake()
    {
        SetInstance(this);
    }
    // Start is called before the first frame update
    void Start()
    {
        ConstructionUtil util;
        if (ConstructionUtil.InstanceAvailable(out util))
        {
            PrefabCollection[] collections = util.Contract.prefabCollections;
            Dictionary<PREFAB_COMPART, List<PrefabCollection>> prefabCollectionsComparted = new Dictionary<PREFAB_COMPART, List<PrefabCollection>>();
            foreach(PrefabCollection collection in collections)
            {
                PREFAB_COMPART pc = collection.prefab.compart;
                if (prefabCollectionsComparted.ContainsKey(pc))
                {
                    prefabCollectionsComparted[pc].Add(collection);
                }
                else
                {
                    prefabCollectionsComparted.Add(pc, new List<PrefabCollection>() { collection });
                }
            }
            foreach (PREFAB_COMPART key in PrefabSelectionButton.Keys)
            {
                PrefabSelectionButton psButton;
                if (PrefabSelectionButton.InstanceExists(key, out psButton) && prefabCollectionsComparted.ContainsKey(key))
                {
                    psButton.AddPrefabs(prefabCollectionsComparted[key]);
                }
            }
        }

    }
}