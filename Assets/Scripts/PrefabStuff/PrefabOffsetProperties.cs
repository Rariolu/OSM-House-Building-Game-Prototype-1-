using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A class used to adjust transformation properties
/// of an instantiated prefab so that it matches
/// the desired position, rotation, etc.
/// </summary>
[Serializable]
public class PrefabOffsetProperties
{
    /// <summary>
    /// A vector3 which is added to the local position of the prefab.
    /// </summary>
    public Vector3 offsetPosition = new Vector3();

    /// <summary>
    /// A vector3 which is used to set the rotation of the prefab
    /// (after conversion to Quaternion).
    /// </summary>
    public Vector3 offsetRotation = new Vector3();

    /// <summary>
    /// A vector3 which is used to set the local scale of the prefab.
    /// </summary>
    public Vector3 offsetScale = new Vector3(1, 1, 1);
    
    /// <summary>
    /// Apply the offset vectors to the given transform component.
    /// </summary>
    /// <param name="transform"></param>
    public void ApplyOffset(Transform transform)
    {
        transform.localPosition += offsetPosition;
        transform.localRotation = Quaternion.Euler(offsetRotation);
        transform.localScale = offsetScale;
    }
}