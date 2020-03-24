using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabIconGrid : MultitonScript<PrefabIconGrid, PREFAB_COMPART>
{
    public PREFAB_COMPART compart;
    Dictionary<int, PrefabIconScript> icons = new Dictionary<int, PrefabIconScript>();
    private void Awake()
    {
        SetInstance(compart, this);
    }
    
    void Start()
    {
        SetIcons();
        gameObject.SetActive(false);
    }
    void SetIcons()
    {
        if (icons.Count < 1)
        {
            PrefabIconScript[] iconArr = GetComponentsInChildren<PrefabIconScript>();
            foreach (PrefabIconScript icon in iconArr)
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
        }
    }
    public void SetPrefabs(List<Prefab> prefabs)
    {
        SetIcons();
        //Logger.Log("Prefabs being set");
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