using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void PrefabPlacementClick();
public class PrefabPlacementScript : MonoBehaviour
{
    public PrefabPlacementClick Click;
    public PrefabPlacedObject parentPrefabInstance;
    float holdDown = 0.5f;
    bool held = false;
    IEnumerator DestroyOnHold()
    {
        float t = 0;
        bool unHeldDuringCountdown = false;
        while (t < holdDown)
        {
            if (!held)
            {
                unHeldDuringCountdown = true;
                break;
            }
            t += Time.deltaTime;
            yield return 0;
        }
        if (held && !unHeldDuringCountdown)
        {
            DestroyPrefab();
        }
    }
    void DestroyPrefab()
    {
        parentPrefabInstance.Destroy();
    }
    void OnMouseDown()
    {
        if (Click != null)
        {
            Click();
        }
        //Debug.Log("ifrfijrf");
        //StartCoroutine(ClickCountThing());

        held = true;
        StartCoroutine(DestroyOnHold());
    }

    float clickInterval = 0.5f;
    int clickCount = 0;
    IEnumerator ClickCountThing()
    {
        clickCount++;
        Debug.LogFormat("Click count: {0};", clickCount);
        if (clickCount >= 2)
        {
            DestroyPrefab();
        }
        float t = 0;
        while (t < clickInterval)
        {
            t += Time.deltaTime;
            yield return 0;
        }
        clickCount--;
    }

    void OnMouseUp()
    {
        held = false;
    }
}