#pragma warning disable IDE0017
#pragma warning disable IDE0018
#pragma warning disable IDE0039
#pragma warning disable IDE0044
#pragma warning disable IDE1005

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

/// <summary>
/// A class which represents a particular intersection (bridge between
/// two separate prefabs).
/// </summary>
public class Intersection : MultitonClass<Intersection,int>
{
    bool fixingsPreviouslySet = false;
    public bool FixingsPreviouslySet
    {
        get
        {
            return fixingsPreviouslySet;
        }
    }
    List<FIXINGSECTION> fixingSections = new List<FIXINGSECTION>();
    public List<FIXINGSECTION> FixingSections
    {
        get
        {
            return fixingSections;
        }
    }
    GameObject gameObject;

    static int instCount = 0;

    int instID;

    List<SnapPointTrigger> snapPointTriggers = new List<SnapPointTrigger>();
    int snapPointsSnapped = 0;
    int SnapPointsSnapped
    {
        get
        {
            return snapPointsSnapped;
        }
        set
        {
            snapPointsSnapped = value;
            SetActive(snapPointsSnapped == snapPointTriggers.Count);
        }
    }

    public Intersection()
    {
        GameObject gameObjectTemp = new GameObject();
        
        GameObject intersectionPrefab;
        if (ResourceManager.GetItem("Intersection", out intersectionPrefab))
        {
            gameObjectTemp = Object.Instantiate(intersectionPrefab);
        }
        else
        {
            gameObjectTemp.transform.localScale = new Vector3(1f, 5f, 1f);
            BoxCollider boxCollider = gameObjectTemp.AddComponent<BoxCollider>();
            MeshFilter filter = gameObjectTemp.AddComponent<MeshFilter>();
            MeshRenderer renderer = gameObjectTemp.AddComponent<MeshRenderer>();
            Mesh mesh;
            if (ResourceManager.GetItem("intersection", out mesh))
            {
                filter.mesh = mesh;
            }
            Material mat;
            if (ResourceManager.GetItem("intersection", out mat))
            {
                renderer.material = mat;
            }
        }

        Init(gameObjectTemp);
    }

    public Intersection(GameObject gameObjectBase)
    {
        Init(gameObjectBase);
    }

    public void AddSnapPoint(SnapPointTrigger trigger)
    {
        snapPointTriggers.Add(trigger);
        trigger.SnapPointTriggered += () => { SnapPointsSnapped++; };
        trigger.SnapPointUnTriggered += () => { SnapPointsSnapped--; };
    }

    void Init(GameObject gameObjectBase)
    {
        instID = instCount++;
        SetInstance(instID, this);
        gameObject = gameObjectBase;
        gameObject.layer = (int)LAYER.IntersectionLayer;
        gameObject.name = "Intersection";
        SetClick();
    }

    public void Destroy()
    {
        Object.Destroy(gameObject);
        RemoveInstance(instID);
    }

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }

    void SetClick()
    {
        IntersectionComponent ic = gameObject.GetComponent<IntersectionComponent>() ?? gameObject.AddComponent<IntersectionComponent>();
        ic.Click += () =>
        {
            buildSystem bs;
            bool apply = !SingletonUtil.InstanceAvailable(out bs) || !bs.PrefabSet;
            if (apply)
            {
                Util.ApplyFixturesToIntersection(this);
            }
            else
            {
                Logger.Log("Lack of application worked.");
            }
        };
    }
    public void SetFixingSections(List<FIXINGSECTION> fSections)
    {
        fixingSections = fSections;
        fixingsPreviouslySet = true;
    }
    public void SetParent(Transform transform)
    {
        gameObject.transform.SetParent(transform);
    }
    public void SetPosition(Vector3 position)
    {
        gameObject.transform.position = position;
    }
}