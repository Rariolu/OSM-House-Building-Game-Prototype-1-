using UnityEngine;
using System;
using System.Collections;

/// <summary>
/// A struct which represents an orientation of the camera
/// (a combined position and rotation).
/// </summary>
[Serializable]
public struct CameraPosition
{
    public Vector3 position;
    public Vector3 rotation;
}