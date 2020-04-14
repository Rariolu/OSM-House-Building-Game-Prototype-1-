#pragma warning disable IDE1006
#pragma warning disable IDE0017
#pragma warning disable IDE0018
#pragma warning disable IDE0039
#pragma warning disable IDE0044
#pragma warning disable IDE1005

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// A script attached to an Intersection's gameobject which allows it to be clicked.
/// </summary>
public class IntersectionComponent : MonoBehaviour
{
    MeshRenderer meshRenderer;
    MeshRenderer MeshRenderer
    {
        get
        {
            return meshRenderer ?? (meshRenderer = GetComponent<MeshRenderer>());
        }
    }

    public Action Click;

    int clickCount = 0;
    float clickInterval = 0.5f;

    IEnumerator ClickCountThing()
    {
        clickCount++;
        Logger.Log("Pre Click count: {0};", clickCount);
        if (clickCount == 2)
        {
            clickCount = 0;
            if (Click != null)
            {
                Click();

                Material confirmedIntersection;
                if (ResourceManager.GetItem("Valid", out confirmedIntersection))
                {
                    MeshRenderer.material = confirmedIntersection;
                }
            }
        }
        float t = 0;
        while (t < clickInterval)
        {
            t += Time.deltaTime;
            yield return 0;
        }
        clickCount--;
        Logger.Log("Post Click count: {0};", clickCount);
    }

    void OnMouseDown()
    {
        StartCoroutine(ClickCountThing());
    }
}