#pragma warning disable IDE0017
#pragma warning disable IDE0018
#pragma warning disable IDE0039
#pragma warning disable IDE0044
#pragma warning disable IDE1005

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A class which spawns a prefab gameobject at a selected position.
/// </summary>
public class PrefabPlacedObject
{
    GameObject gameObject;
    InGameSceneScript gameScene;
    List<Intersection> intersections = new List<Intersection>();
    List<Vector3> intersectionPoints = new List<Vector3>();
    private Vector3 roundedPosition;
    public Vector3 RoundedPosition
    {
        get
        {
            return roundedPosition;
        }
    }
    private Prefab templatePrefab;
    public Prefab Prefab
    {
        get
        {
            return templatePrefab;
        }
    }
    SnapPointTrigger snapPointTrigger;
    public SnapPointTrigger SnapPointTrigger
    {
        get
        {
            return snapPointTrigger;
        }
        set
        {
            snapPointTrigger = value;
        }
    }
    public PrefabPlacedObject(Prefab prefab, Vector3 position)
    {
        templatePrefab = prefab;
        GameObject template;
        if (ResourceManager.GetItem(prefab.type, out template))
        {
            gameObject = Object.Instantiate(template);
        }
        else
        {
            gameObject = new GameObject();
            MeshFilter filter = gameObject.AddComponent<MeshFilter>();
            MeshRenderer renderer = gameObject.AddComponent<MeshRenderer>();
            Mesh mesh;
            if (ResourceManager.GetItem(prefab.type, out mesh))
            {
                filter.mesh = mesh;
            }
            Material mat;
            if (ResourceManager.GetItem(prefab.type, out mat))
            {
                renderer.material = mat;
            }
        }
        SceneObjectScript gameSceneObjectScript;
        if (SceneObjectScript.InstanceExists(SCENE.InGame, out gameSceneObjectScript))
        {
            gameObject.transform.SetParent(gameSceneObjectScript.transform);
        }
        Vector3 offset;
        switch(prefab.snapType)
        {
            case SNAP_POINT_TYPE.CENTRE:
            {
                offset = new Vector3(-2.5f, 0, -2.5f);
                break;
            }
            case SNAP_POINT_TYPE.FLOOR:
            {
                offset = new Vector3(0, 0, -2.5f);
                break;
            }
            default:
            {
                offset = new Vector3();
                break;
            }
        }


        Vector3 roundPos = position.RoundToNearestMultiple(5);
        roundPos += offset;
        gameObject.transform.position = roundPos;
        prefab.offset.ApplyOffset(gameObject.transform);

        roundPos = gameObject.transform.position;
        if (prefab.position == PREFAB_POSITION.EXTERIOR && ((prefab.snapType == SNAP_POINT_TYPE.CENTRE && position.x > 0) || (prefab.snapType == SNAP_POINT_TYPE.EDGE && position.z < 0)))
        {
            gameObject.transform.Rotate(0, 180f, 0, Space.World);
        }
        gameObject.name = prefab.type.ToString();
        gameObject.tag = TAG.TESTTAG.ToString();

        roundedPosition = roundPos;
        if (InGameSceneScript.InstanceAvailable(out gameScene))
        {
            for (int i = -1; i < 2; i++)
            {
                if (i != 0)
                {
                    switch(prefab.snapType)
                    {
                        case SNAP_POINT_TYPE.EDGE:
                        {
                            AddIntersection(new Vector2(i/2f, 0), prefab.snapType);
                            break;
                        }
                        case SNAP_POINT_TYPE.CENTRE:
                        {
                            AddIntersection(new Vector2(0, i / 2f), prefab.snapType);
                            break;
                        }
                    }
                }
            }
            gameScene.MaterialPlaced(prefab.material);
        }

        //Get the PrefabPlacementScript component or add one if there is none.
        PrefabPlacementScript pps = gameObject.GetComponent<PrefabPlacementScript>() ?? gameObject.AddComponent<PrefabPlacementScript>();

        CameraMovementScript camera;
        if (CameraMovementScript.InstanceAvailable(out camera))
        {
            camera.CameraMoved += CameraMoved;
        }
    }

    MeshRenderer mRenderer;
    MeshRenderer MeshRenderer
    {
        get
        {
            return mRenderer ?? (mRenderer = gameObject.GetComponent<MeshRenderer>());
        }
    }
    const float blep = 40f;
    void CameraMoved(CameraMovementScript camera, int index)
    {
        if (Prefab.position == PREFAB_POSITION.EXTERIOR)
        {
            Vector3 pos = gameObject.transform.position;
            Vector3 cameraPos = camera.ActualCameraPosition;
            if (Prefab.snapType == SNAP_POINT_TYPE.EDGE)
            {
                bool z = (pos.z < 0 && cameraPos.z < 0) || (pos.z > 0 && cameraPos.z > 0);
                MeshRenderer.enabled = !z;
            }
            else if (Prefab.snapType == SNAP_POINT_TYPE.CENTRE)
            {
                bool x = (pos.x < 0 && cameraPos.x < 0) || (pos.x > 0 && cameraPos.x > 0);
                MeshRenderer.enabled = !x;
            }
        }
        //MeshRenderer.enabled = Vector3.Distance(camera.transform.position, gameObject.transform.position) < blep|| Prefab.snapType == SNAP_POINT_TYPE.FLOOR;
        //if (index == SnapPointTrigger.dropIndex)
        //{

        //}
        //else
        //{

        //}
    }

    void AddIntersection(Vector2 offset, SNAP_POINT_TYPE sType)
    {
        Vector3 intersectionPosition = (gameObject.transform.position + new Vector3(offset.x * 5f, 2.5f, offset.y * 5f)).RoundToNearestMultiple(2.5f);
        Intersection intersection;
        if (gameScene.AddIntersection(Prefab.floorType, intersectionPosition, out intersection))
        {
            intersections.Add(intersection);
        }
        intersectionPoints.Add(intersectionPosition);
    }

    /// <summary>
    /// Destroy the placed object and the intersection between it
    /// and the prefab it had snapped to.
    /// </summary>
    public void Destroy()
    {
        Object.Destroy(gameObject);
        foreach(Vector3 intersection in intersectionPoints)
        {
            gameScene.RemovePrefabIntersectionPoint(Prefab.floorType, intersection);
        }
        intersectionPoints.Clear();
        ConstructionUtil util;
        if (ConstructionUtil.InstanceAvailable(out util))
        {
            util.IncrementDestruction();
        }

        CameraMovementScript camera;
        if (CameraMovementScript.InstanceAvailable(out camera))
        {
            camera.CameraMoved -= CameraMoved;
        }
    }

    public void SetSceneParent()
    {
        SceneObjectScript gameScene;
        if (SceneObjectScript.InstanceExists(SCENE.InGame, out gameScene))
        {
            gameObject.transform.SetParent(gameScene.transform);
        }
    }
}