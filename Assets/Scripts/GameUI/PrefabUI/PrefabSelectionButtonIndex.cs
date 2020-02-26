using UnityEngine;
using System.Collections;
using System;

[Serializable]
public struct PrefabSelectionButtonIndex
{
    public int index;
    public PREFAB_COMPART compart;
    public PrefabSelectionButtonIndex(int i, PREFAB_COMPART c)
    {
        index = i;
        compart = c;
    }
}