using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabIconGrid : MultitonScript<PrefabIconGrid,PREFAB_COMPART>
{
    public PREFAB_COMPART compart;
    Dictionary<int, PrefabIconScript> icons = new Dictionary<int, PrefabIconScript>();
    private void Awake()
    {
        SetInstance(compart, this);
    }
    // Start is called before the first frame update
    void Start()
    {
        PrefabIconScript[] iconArr = GetComponentsInChildren<PrefabIconScript>();
        foreach(PrefabIconScript icon in iconArr)
        {
            if (icons.ContainsKey(icon.index))
            {
                icons[icon.index] = icon;
            }
            else
            {
                icons.Add(icon.index, icon);
            }
        }
        gameObject.SetActive(false);
    }
    public void SetPrefabs(List<Prefab> prefabs)
    {
        for (int i = 0; i < icons.Count; i++)
        {
            if (icons.ContainsKey(i))
            {
                if (i < prefabs.Count)
                {
                    icons[i].Prefab = prefabs[i];
                    icons[i].gameObject.SetActive(true);
                }
                else
                {
                    icons[i].gameObject.SetActive(false);
                }
            }
        }
    }
    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }
}