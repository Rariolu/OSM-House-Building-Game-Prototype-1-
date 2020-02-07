using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabIconScript : UIButton
{
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
        }
    }

    protected override void Start()
    {
        base.Start();
        Click += PrefabIconScript_Click;
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