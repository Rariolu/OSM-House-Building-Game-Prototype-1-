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
    #region MemberVariables_N_Properties

    //Animator animator;

    GameObject bottomHalf;

    bool dropped = false;

    GameObject gameObject;
    InGameSceneScript gameScene;

    static int instCount = 0;

    readonly int instID;

    List<Intersection> intersections = new List<Intersection>();

    List<Vector3> intersectionPoints = new List<Vector3>();

    MeshRenderer mRenderer;
    MeshRenderer MeshRenderer
    {
        get
        {
            return mRenderer ?? (mRenderer = gameObject.GetComponent<MeshRenderer>());
        }
    }

    Vector3 originalScale;

    PrefabPlacementScript prefabPlacementScript;

    private Vector3 roundedPosition;
    public Vector3 RoundedPosition
    {
        get
        {
            return roundedPosition;
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

    private Prefab templatePrefab;
    public Prefab Prefab
    {
        get
        {
            return templatePrefab;
        }
    }

    #endregion

    public PrefabPlacedObject(Prefab prefab, Vector3 position, FINISHED_CONSTRUCTION construction = FINISHED_CONSTRUCTION.SEMI_DETACHED_HOUSE)
    {
        //Logger.Log("Construction: {0};", construction);

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
            if (centre || edge)
            {
                gameObject.transform.Rotate(0, 180f, 0, Space.World);
            }
        }
        
        if (SingletonUtil.InstanceAvailable(out gameScene))
        {
            //switch (prefab.snapType)
            //{
            //    case SNAP_POINT_TYPE.EDGE:
            //    {
            //        AddIntersection(new Vector2(-1f / 2f, 0), prefab.snapType);
            //        AddIntersection(new Vector2( 1f / 2f, 0), prefab.snapType);
            //        break;
            //    }
            //    case SNAP_POINT_TYPE.CENTRE:
            //    {
            //        AddIntersection(new Vector2(0, -1f / 2f), prefab.snapType);
            //        AddIntersection(new Vector2(0,  1f / 2f), prefab.snapType);
            //        break;
            //    }
            //}
            gameScene.MaterialPlaced(prefab.material);
        }

        //Get the PrefabPlacementScript component or add one if there is none.
        prefabPlacementScript = gameObject.GetComponent<PrefabPlacementScript>() ?? gameObject.AddComponent<PrefabPlacementScript>();
        prefabPlacementScript.parentPrefabInstance = this;
        prefabPlacementScript.PrefabPlacementDeleted += () =>
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

        //animator = gameObject.GetComponent<Animator>();
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

    public void AddRigidBody(float mass)
    {
        Rigidbody rigidBody = gameObject.AddComponent<Rigidbody>();
        rigidBody.useGravity = true;
    }

    public static void AddRigidBodies(float mass = 1f, float force = 0f)
    {
        Vector3 explosionCentre = new Vector3();
        foreach (PrefabPlacedObject ppo in Values)
        {
            ppo.AddRigidBody(mass);
            explosionCentre += ppo.RoundedPosition;
        }
        explosionCentre /= Values.Length;
        foreach (PrefabPlacedObject ppo in Values)
        {
            ppo.Implode(force, explosionCentre);
        }
    }

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

    void CreateBottomHalf()
    {
        Material bottomMat = MeshRenderer.material;
        GameObject templateBottomHalf;
        if (ResourceManager.GetItem("BottomHalfTemplate", out templateBottomHalf))
        {
            bottomHalf = Object.Instantiate(templateBottomHalf);
            bottomHalf.GetComponent<MeshRenderer>().material = bottomMat;
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
        bottomHalf.name = gameObject.name + " bottom half";
        bottomHalf.transform.position = gameObject.transform.position;
        bottomHalf.transform.parent = gameObject.transform;
    }

    /// <summary>
    /// Destroy the placed object and the intersection between it
    /// and the prefab it had snapped to.
    /// </summary>
    public void Destroy()
    {
        //Object.Destroy(gameObject);
        Object.Destroy(bottomHalf);

        foreach (Vector3 intersection in intersectionPoints)
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

        //animator.SetBool("Exterior_Wall_Des", true);

        prefabPlacementScript.DestructionAnimation();

        //RemoveInstance(instID);
    }

    public void Drop(bool drop)
    {
        bool willDrop = drop && dropped;
        gameObject.SetActive(!willDrop);
        bottomHalf.SetActive(willDrop);
    }

    public void Implode(float force, Vector3 explosionCentre)
    {
        const float explosionDistance = 35f;
        Rigidbody rigidBody = gameObject.GetComponent<Rigidbody>();
        rigidBody.AddExplosionForce(-force, explosionCentre, explosionDistance);
    }

    public void SetSceneParent()
    {
        SceneObjectScript gameScene;
        if (SceneObjectScript.InstanceExists(SCENE.InGame, out gameScene))
        {
            Transform placedPrefabsTransform = gameScene.transform.Find("PlacedPrefabs");
            if (placedPrefabsTransform == null)
            {
                GameObject ppObj = new GameObject();
                ppObj.name = "PlacedPrefabs";
                ppObj.transform.SetParent(gameScene.transform);
                placedPrefabsTransform = ppObj.transform;
            }
            gameObject.transform.SetParent(placedPrefabsTransform);
            bottomHalf.transform.SetParent(placedPrefabsTransform);
        }
    }
}