using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[Serializable]
public struct PrefabCollection
{
    /// <summary>
    /// The positions that this prefab is supposed to take.
    /// </summary>
    public Vector3[] positionsTakenWithinContract;
    
    /// <summary>
    /// The specifications of the prefab.
    /// </summary>
    public Prefab prefab;

    /// <summary>
    /// The quantity of the prefab.
    /// </summary>
    public int quantity;

    public override string ToString()
    {
        return "\"{0}\" x {1};".FormatText(prefab.ToString(), quantity);
    }
}