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
public class Intersection
{
    List<FIXINGSECTION> fixingSections = new List<FIXINGSECTION>();
    public List<FIXINGSECTION> FixingSections
    {
        get
        {
            return fixingSections;
        }
    }
    GameObject gameObject;
    public Intersection()
    {
        gameObject = new GameObject();
        
        GameObject intersectionPrefab;
        if (ResourceManager.GetItem("Intersection", out intersectionPrefab))
        {
            gameObject = GameObject.Instantiate(intersectionPrefab);
        }
        else
        {
            gameObject.transform.localScale = new Vector3(1f, 5f, 1f);
            BoxCollider boxCollider = gameObject.AddComponent<BoxCollider>();
            MeshFilter filter = gameObject.AddComponent<MeshFilter>();
            MeshRenderer renderer = gameObject.AddComponent<MeshRenderer>();
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
        gameObject.layer = (int)LAYER.IntersectionLayer;
        gameObject.name = "Intersection";
        SetClick();
    }
    public void Destroy()
    {
        Object.Destroy(gameObject);
    }
    void SetClick()
    {
        IntersectionComponent ic = gameObject.AddComponent<IntersectionComponent>();
        ic.Click += () =>
        {
            buildSystem bs;
            bool apply = !buildSystem.InstanceAvailable(out bs) || !bs.PrefabSet;
            if (apply)
            {
                Util.ApplyFixturesToIntersection(this);
            }
            else
            {
                Debug.Log("Lack of application worked.");
            }
        };
    }
    public void SetFixingSections(List<FIXINGSECTION> fSections)
    {
        fixingSections = fSections;
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