using UnityEngine;
using System;
using System.Collections;

[Serializable]
public struct IntVec2
{
    public int x;
    public int y;
    public IntVec2(int _x, int _y)
    {
        x = _x;
        y = _y;
    }
}