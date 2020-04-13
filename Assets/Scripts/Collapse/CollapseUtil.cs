using UnityEngine;
using System.Collections;

public static class CollapseUtil
{
    public static void Collapse()
    {
        GameObject collapser = new GameObject();
        CollapseScript script = collapser.AddComponent<CollapseScript>();
    }
}