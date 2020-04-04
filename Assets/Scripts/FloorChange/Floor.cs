#pragma warning disable IDE0017
#pragma warning disable IDE0018
#pragma warning disable IDE0039
#pragma warning disable IDE0044
#pragma warning disable IDE1005

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// A class which represents a single floor of the construction.
/// Multiton pattern is used so that a "FLOORTYPE" value
/// can represent an instance.
/// </summary>
[RequireComponent(typeof(MeshRenderer))]
public class Floor : MultitonScript<Floor,FLOORTYPE>
{
    public FLOORTYPE floorType;
    //public int maxBound = 10;
    MeshRenderer meshRenderer;
    MeshRenderer MeshRenderer
    {
        get
        {
            return meshRenderer ?? (meshRenderer = GetComponent<MeshRenderer>());
        }
    }
    //public int minBound = -10;
    public bool originalFocus = false;
    const float spaceInterval = 5f;
    public bool roofSnapPoint = false;
    public Vector2 roofSnapPos = new Vector2(0, 0);
    private void Awake()
    {
        SetInstance(floorType, this);
        if (originalFocus)
        {
            FocusedFloor = this;
        }
    }
    public void Focus()
    {
        gameObject.SetActive(true);
        FocusedFloor = this;

        CameraMovementScript camControl;
        if (SingletonUtil.InstanceAvailable(out camControl))
        {
            camControl.SetFloor(transform.position.y);
        }

        for(int i = 0; i < 3; i++)
        {
            FLOORTYPE ftype = (FLOORTYPE)i;
            if (ftype != floorType)
            {
                Floor other;
                if (InstanceExists(ftype, out other))
                {
                    other.Unfocus(true);
                }
            }
        }
    }
    public static Floor FocusedFloor;
    
    void Start()
    {
        Material floorMat;
        if (ResourceManager.GetItem("floormat",out floorMat))
        {
            MeshRenderer.material = floorMat;
        }
    }

    public void Unfocus(bool deactivate = false)
    {
        gameObject.SetActive(!deactivate);
    }
}