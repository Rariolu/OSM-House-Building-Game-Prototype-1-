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
    public int maxBound = 10;
    MeshRenderer meshRenderer;
    MeshRenderer MeshRenderer
    {
        get
        {
            return meshRenderer ?? (meshRenderer = GetComponent<MeshRenderer>());
        }
    }
    public int minBound = -10;
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
    void CreateSnapTriggers()
    {
        float y = transform.position.y;
        for (int x = minBound; x <= maxBound; x++)
        {
            GameObject snapRow = new GameObject();
            snapRow.name = "Snap Row {0}".Format(x);
            snapRow.transform.SetParent(transform);
            for (int z = minBound; z <= maxBound; z++)
            {
                SnapPoint sp = new SnapPoint(SNAP_POINT_TYPE.EDGE);
                sp.SetParent(snapRow.transform);
                Vector3 spPos = new Vector3(spaceInterval * x, y, spaceInterval * z);
                sp.SetPosition(spPos);

                SnapPoint spFloor = new SnapPoint(SNAP_POINT_TYPE.FLOOR);
                spFloor.SetParent(snapRow.transform);
                Vector3 spFloorPos = new Vector3(spaceInterval * x, y, spPos.z + (spaceInterval / 2f));
                spFloor.SetPosition(spFloorPos);

                SnapPoint centreSP = new SnapPoint(SNAP_POINT_TYPE.CENTRE);
                centreSP.SetParent(snapRow.transform);
                centreSP.SetPosition(new Vector3(spPos.x + (spaceInterval / 2f), spPos.y, spPos.z + (spaceInterval / 2f)));
            }
        }
        if (roofSnapPoint)
        {
            SnapPoint sp = new SnapPoint(SNAP_POINT_TYPE.ROOF);
            sp.SetParent(transform);
            sp.SetPosition(new Vector3(roofSnapPos.x,y,roofSnapPos.y));
        }
    }
    public void Focus()
    {
        gameObject.SetActive(true);
        FocusedFloor = this;
        //Set the current floortype of the panel so it displays
        //correct prefabs.
        PrefabSelectionButton.SetCurrentFloorAllButtons(floorType);

        OtherCameraTest camControl;
        if (OtherCameraTest.InstanceAvailable(out camControl))
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
    // Start is called before the first frame update
    void Start()
    {
        Material floorMat;
        if (ResourceManager.GetItem("floormat",out floorMat))
        {
            MeshRenderer.material = floorMat;
        }

        CreateSnapTriggers();
    }

    public void Unfocus(bool deactivate = false)
    {
        gameObject.SetActive(!deactivate);
    }
}