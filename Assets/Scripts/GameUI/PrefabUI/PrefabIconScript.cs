using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrefabIconScript : UIButton
{
    static List<PrefabIconScript> prefabIcons = new List<PrefabIconScript>();
    public static List<PrefabIconScript> PrefabIcons
    {
        get
        {
            return prefabIcons;
        }
    }

    public static void ClearIcons()
    {
        prefabIcons.Clear();
    }

    public Text lblCounter;

    Prefab prefab;
    public Prefab Prefab
    {
        get
        {
            return prefab;
        }
        set
        {
            prefab = value;
            if (lblCounter != null)
            {
                PrefabCounter counter;
                if (PrefabCounter.InstanceAvailable(out counter))
                {
                    lblCounter.text = counter.GetCount(prefab).ToString();
                }
            }
        }
    }

    void Awake()
    {
        prefabIcons.Add(this);
    }

    protected override void Start()
    {
        base.Start();
        Click += PrefabIconScript_Click;
        PrefabCounter counter;
        if (PrefabCounter.InstanceAvailable(out counter))
        {
            counter.CounterChanged += CounterChanged;
            if (lblCounter != null)
            {
                lblCounter.text = counter.GetCount(prefab).ToString();
            }
        }
    }

    void CounterChanged(Prefab changedPrefab, int counter)
    {
        if (prefab == changedPrefab)
        {
            if (lblCounter != null)
            {
                lblCounter.text = counter.ToString();
            }
        }
    }

    void PrefabIconScript_Click(UIButton sender)
    {
        PrefabCounter counter;
        if (PrefabCounter.InstanceAvailable(out counter))
        {
            counter.SelectPrefab(Prefab);
        }
    }
}