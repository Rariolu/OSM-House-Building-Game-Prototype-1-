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
public class PrefabPlacedObject : MultitonClass<PrefabPlacedObject,int>
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

    MeshRenderer mRenderer;
    MeshRenderer MeshRenderer
    {
        get
        {
            return mRenderer ?? (mRenderer = gameObject.GetComponent<MeshRenderer>());
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

    Vector3 originalScale;

    static int instCount = 0;

    readonly int instID;

    public PrefabPlacedObject(Prefab prefab, Vector3 position, FINISHED_CONSTRUCTION construction = FINISHED_CONSTRUCTION.SEMI_DETACHED_HOUSE)
    {
        Logger.Log("Construction: {0};", construction);

        instID = instCount++;
        SetInstance(instID, this);

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
        Vector3 offset = new Vector3();
        switch (prefab.snapType)
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
        }


        Vector3 roundPos = position.RoundToNearestMultiple(5);
        roundPos += offset;
        gameObject.transform.position = roundPos;
        prefab.offset.ApplyOffset(gameObject.transform);

        roundPos = gameObject.transform.position;

        gameObject.name = prefab.type.ToString();
        gameObject.tag = TAG.TESTTAG.ToString();

        roundedPosition = roundPos;

        if (prefab.position == PREFAB_POSITION.EXTERIOR)
        {
            bool centre = prefab.snapType == SNAP_POINT_TYPE.CENTRE && (position.x > 0 || (construction == FINISHED_CONSTRUCTION.DETACHED_HOUSE && RoundedPosition.x == -2.5f && RoundedPosition.z < 0f));
            bool edge = (prefab.snapType == SNAP_POINT_TYPE.EDGE && position.z < 0);
            //bool centreException = centre && (construction != FINISHED_CONSTRUCTION.DETACHED_HOUSE || (RoundedPosition.x != -2.5f));
            //centre = centreException;
            if (centre || edge)
            {
                Logger.Log("Rotated");
                gameObject.transform.Rotate(0, 180f, 0, Space.World);
            }
            //Logger.Log("Name: {0}; Centre: {1}; Edge: {2}; CentreException: {3}; SnapType: {4};",gameObject.name, centre, edge, centreException, prefab.snapType);
        }

        Logger.Log("RoundedPosition: {0};", RoundedPosition);
        if (SingletonUtil.InstanceAvailable(out gameScene))
        {
            switch (prefab.snapType)
            {
                case SNAP_POINT_TYPE.EDGE:
                {
                    AddIntersection(new Vector2(-1f / 2f, 0), prefab.snapType);
                    AddIntersection(new Vector2( 1f / 2f, 0), prefab.snapType);
                    break;
                }
                case SNAP_POINT_TYPE.CENTRE:
                {
                    AddIntersection(new Vector2(0, -1f / 2f), prefab.snapType);
                    AddIntersection(new Vector2(0,  1f / 2f), prefab.snapType);
                    break;
                }
            }
            gameScene.MaterialPlaced(prefab.material);
        }

        //Get the PrefabPlacementScript component or add one if there is none.
        PrefabPlacementScript pps = gameObject.GetComponent<PrefabPlacementScript>() ?? gameObject.AddComponent<PrefabPlacementScript>();
        pps.parentPrefabInstance = this;
        pps.PrefabPlacementDeleted += () =>
        {
            RemoveInstance(instID);
        };

        BoxCollider collider = gameObject.GetComponent<BoxCollider>() ?? gameObject.AddComponent<BoxCollider>();

        originalScale = gameObject.transform.localScale;

        CreateBottomHalf();

        CameraMovementScript camera;
        if (SingletonUtil.InstanceAvailable(out camera))
        {
            camera.CameraMoved += CameraMoved;
            CameraMoved(camera);
        }

        DropWallButton dropWallButton;
        if (SingletonUtil.InstanceAvailable(out dropWallButton))
        {
            Drop(dropWallButton.Dropped);
        }
    }

    GameObject bottomHalf;
    
    void CreateBottomHalf()
    {
        Material bottomMat = MeshRenderer.material;
        if (bottomHalf == null)
        {
            GameObject templateBottomHalf;
            if (ResourceManager.GetItem("BottomHalfTemplate", out templateBottomHalf))
            {
                bottomHalf = Object.Instantiate(templateBottomHalf);
                bottomHalf.GetComponent<MeshRenderer>().material = bottomMat;
                Logger.Log("BottomHalf template found and instantiated.");
            }
            else
            {
                Debug.LogWarning("BottomHalf template not found so making a blank object sos.");
                bottomHalf = new GameObject();
            }
            if (Prefab.snapType == SNAP_POINT_TYPE.EDGE)
            {
                bottomHalf.transform.Rotate(0, 90f, 0, Space.World);
            }
        }
        bottomHalf.name = gameObject.name + " bottom half";
        bottomHalf.transform.position = gameObject.transform.position;
        bottomHalf.transform.parent = gameObject.transform;
        SceneObjectScript gameSceneObjectScript;
        if (SceneObjectScript.InstanceExists(SCENE.InGame, out gameSceneObjectScript))
        {
            bottomHalf.transform.SetParent(gameSceneObjectScript.transform);
        }
    }
    bool dropped = false;
    void CameraMoved(CameraMovementScript camera)
    {
        if (Prefab.position == PREFAB_POSITION.EXTERIOR)
        {
            Vector3 pos = gameObject.transform.position;
            Vector3 cameraPos = camera.ActualCameraPosition;
            if (Prefab.snapType == SNAP_POINT_TYPE.EDGE)
            {
                bool z = (pos.z < 0 && cameraPos.z < 0) || (pos.z > 0 && cameraPos.z > 0);
                dropped = z;
            }
            else if (Prefab.snapType == SNAP_POINT_TYPE.CENTRE)
            {
                bool x = (pos.x < 0 && cameraPos.x < 0) || (pos.x > 0 && cameraPos.x > 0);
                dropped = x;
            }
        }
    }

    public void Drop(bool drop)
    {
        bool willDrop = drop && dropped;
        gameObject.SetActive(!willDrop);
        bottomHalf.SetActive(willDrop);
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
        Object.Destroy(bottomHalf);
        foreach(Vector3 intersection in intersectionPoints)
        {
            gameScene.RemovePrefabIntersectionPoint(Prefab.floorType, intersection);
        }
        intersectionPoints.Clear();
        ConstructionUtil util;
        if (SingletonUtil.InstanceAvailable(out util))
        {
            util.IncrementDestruction();
        }

        CameraMovementScript camera;
        if (SingletonUtil.InstanceAvailable(out camera))
        {
            camera.CameraMoved -= CameraMoved;
        }

        PrefabCounter counter;
        if (SingletonUtil.InstanceAvailable(out counter))
        {
            counter.IncrementCount(Prefab);
        }

        gameScene.RemovePlacedPrefab(this);

        SnapPointTrigger.Snapped = false;

        RemoveInstance(instID);
    }

    public void SetSceneParent()
    {
        SceneObjectScript gameScene;
        if (SceneObjectScript.InstanceExists(SCENE.InGame, out gameScene))
        {
            gameObject.transform.SetParent(gameScene.transform);
        }
    }

    public void AddRigidBody(float mass)
    {
        Rigidbody rigidBody = gameObject.AddComponent<Rigidbody>();
        rigidBody.useGravity = true;
    }

    public void Implode(float force, Vector3 explosionCentre)
    {
        const float explosionDistance = 50f;
        Rigidbody rigidBody = gameObject.GetComponent<Rigidbody>();
        rigidBody.AddExplosionForce(-force, explosionCentre, explosionDistance);
    }

    public static void AddRigidBodies(float mass = 1f, float force = 0f)
    {
        Vector3 explosionCentre = new Vector3();
        foreach(PrefabPlacedObject ppo in Values)
        {
            ppo.AddRigidBody(mass);
            explosionCentre += ppo.RoundedPosition;
        }
        explosionCentre /= Values.Length;
        foreach(PrefabPlacedObject ppo in Values)
        {
            ppo.Implode(force, explosionCentre);
        }
    }
}