#pragma warning disable IDE0017
#pragma warning disable IDE0018
#pragma warning disable IDE0039
#pragma warning disable IDE0044
#pragma warning disable IDE1005

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A class, an instance of which represents a single point that
/// a prefab can "snap" to.
/// </summary>
public class SnapPoint
{
    /// <summary>
    /// A dictionary used to distinguish the different types of snap points.
    /// </summary>
    static Dictionary<SNAP_POINT_TYPE, List<SnapPoint>> snapPointInstances = new Dictionary<SNAP_POINT_TYPE, List<SnapPoint>>();

    /// <summary>
    /// The associated GameObject which exists in the game world and allows the SnapPoint to be clicked.
    /// </summary>
    GameObject gameObject;

    public void SetDropIndex(int index)
    {
        trigger.dropIndex = index;
    }

    SnapPointTrigger trigger;

    /// <summary>
    /// The snap type of this SnapPoint instance.
    /// </summary>
    SNAP_POINT_TYPE snapType;

    /// <summary>
    /// Add a SnapPoint of a given type to the dictionary of instances.
    /// </summary>
    /// <param name="snapType"></param>
    /// <param name="instance"></param>
    static void AddInstance(SNAP_POINT_TYPE snapType, SnapPoint instance)
    {
        if (snapPointInstances.ContainsKey(snapType))
        {
            snapPointInstances[snapType].Add(instance);
        }
        else
        {
            snapPointInstances.Add(snapType, new List<SnapPoint>() { instance });
        }
    }

    /// <summary>
    /// Set the gameobjects of the SnapPoints of a given type to be active.
    /// </summary>
    /// <param name="snapType"></param>
    public static void ShowSnapPoints(SNAP_POINT_TYPE snapType)
    {
        SetSnapPointActive(snapType, true);
    }

    /// <summary>
    /// Set the gameobjects of the SnapPoints of a given type to be inactive.
    /// </summary>
    /// <param name="snapType"></param>
    public static void HideSnapPoints(SNAP_POINT_TYPE snapType)
    {
        SetSnapPointActive(snapType, false);
    }

    /// <summary>
    /// Sets the gameobjects of the SnapPoints of a given type to be active
    /// or inactive depending on the given boolean.
    /// </summary>
    /// <param name="snapType"></param>
    /// <param name="active"></param>
    static void SetSnapPointActive(SNAP_POINT_TYPE snapType, bool active)
    {
        foreach (SnapPoint snapPoint in snapPointInstances[snapType])
        {
            if (!active || !snapPoint.IsSnapped)
            {
                snapPoint.SetActive(active);
            }
        }
    }
    
    public SnapPoint(SNAP_POINT_TYPE type = SNAP_POINT_TYPE.EDGE)
    {
        GameObject sphereObj;
        GameObject temp;
        string snapPointGameObjName = "SnapPoint_" + type;
        if (ResourceManager.GetItem(snapPointGameObjName,out sphereObj))
        {
            temp = Object.Instantiate(sphereObj);
        }
        else if (ResourceManager.GetItem("SnapPoint", out sphereObj))
        {
            temp = Object.Instantiate(sphereObj);
        }
        else
        {
            temp = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            const float sphereScale = 0.5f;
            temp.transform.localScale = new Vector3(sphereScale, sphereScale, sphereScale);
        }

        Init(temp, type);
    }

    public SnapPoint(SNAP_POINT_TYPE type, GameObject snapObject)
    {
        Init(snapObject, type);
    }

    void Init(GameObject obj, SNAP_POINT_TYPE type)
    {
        AddInstance(type, this);
        snapType = type;
        gameObject = obj;

        gameObject.name = "SnapPoint";
        gameObject.tag = TAG.TESTTAG.ToString();

        if (gameObject.GetComponent<Collider>() == null)
        {
            SphereCollider collider = gameObject.AddComponent<SphereCollider>();
            collider.radius = 1f;
            collider.isTrigger = false;
        }

        //Attempt to retrieve snaptrigger or add one if there isn't one present.
        trigger = gameObject.GetComponent<SnapPointTrigger>() ?? gameObject.AddComponent<SnapPointTrigger>();
        trigger.snapType = type;
        trigger.SnapPointDeleted += () =>
        {
            snapPointInstances[type].RemoveAll(sp => sp == this);
        };
        trigger.SnapPointTriggered += () =>
        {
            SetActive(false);
        };

        gameObject.SetActive(false);
    }

    public bool IsSnapped
    {
        get
        {
            return trigger.Snapped;
        }
    }

    /// <summary>
    /// Set the associated GameObject to be active (or inactive).
    /// </summary>
    /// <param name="active"></param>
    public void SetActive(bool active)
    {
        if (gameObject != null)
        {
            gameObject.SetActive(active);
        }
        else
        {
            Logger.Log("Snap point that doesn't exist is referenced.", LogType.Warning);
            //snapPointInstances[snapType].RemoveAll(sp => sp == this);
        }
    }

    /// <summary>
    /// Set the parent transform of the associated GameObject.
    /// </summary>
    /// <param name="transform"></param>
    public void SetParent(Transform transform)
    {
        gameObject.transform.SetParent(transform);
    }

    /// <summary>
    /// Set the position of the associated GameObject
    /// and set its name accordingly.
    /// </summary>
    /// <param name="pos"></param>
    public void SetPosition(Vector3 pos)
    {
        gameObject.transform.position = pos;
        gameObject.name = "SnapTrigger {0}_{1}_{2}".FormatText(pos.x, pos.y, pos.z);
    }
}