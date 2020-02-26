#pragma warning disable IDE1006
#pragma warning disable IDE0017
#pragma warning disable IDE0018
#pragma warning disable IDE0039
#pragma warning disable IDE0044
#pragma warning disable IDE1005

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


/// <summary>
/// A class representing a unique contract.
/// </summary>
[Serializable]
public class Contract
{
    /// <summary>
    /// The quantity of money that the player has available to complete contract.
    /// </summary>
    public int budget;

    /// <summary>
    /// What the resulting building should look like.
    /// </summary>
    public FINISHED_CONSTRUCTION finishedConstruction;

    /// <summary>
    ///  The quantity of "fixtures" (joints between prefabs) available.
    /// </summary>
    public int fixtures;

    /// <summary>
    /// The name of the contract.
    /// </summary>
    public string name;


    public Vector3[] positionsTaken;
    /// <summary>
    /// The prefabs that the player has available to them.
    /// </summary>
    public PrefabCollection[] prefabCollections;
    public Standard[] standards;
    public Task[] tasks;

    /// <summary>
    /// The ideal amount of time to deliver the contract in.
    /// </summary>
    [SerializeField]
    public uint time;

    public override bool Equals(object obj)
    {
        if (obj is Contract)
        {
            return this == (obj as Contract);
        }
        return false;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override string ToString()
    {
        StringBuilderPro sb = new StringBuilderPro();
        sb.AppendLineFormat("Name: {0};",name);
        sb.AppendLineFormat("Budget: {0};", budget);
        sb.AppendLine("Prefabs:");
        foreach (PrefabCollection prefabCollection in prefabCollections)
        {
            sb.AppendLineFormat("\t{0}", prefabCollection);
        }
        sb.AppendLine("Standards:");
        foreach (Standard standard in standards)
        {
            sb.AppendLineFormat("\t{0}", standard);
        }
        sb.AppendLine("Tasks:");
        foreach(Task task in tasks)
        {
            sb.AppendLineFormat("\t{0}", task);
        }
        return sb.ToString();
    }

    public static bool operator==(Contract c1, Contract c2)
    {
        object oc1 = c1;
        object oc2 = c2;
        if (oc1 == null || oc2 == null)
        {
            return oc1 == oc2;
        }
        return c1.name.StringEquals(c2.name);
    }

    public static bool operator!=(Contract c1, Contract c2)
    {
        return !(c1 == c2);
    }
}