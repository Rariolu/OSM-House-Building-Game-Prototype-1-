﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void PrefabPlacementClick();
public delegate void PrefabPlacementDeleted();
public class PrefabPlacementScript : MonoBehaviour
{
    public PrefabPlacementClick Click;
    public PrefabPlacementDeleted PrefabPlacementDeleted;
    public PrefabPlacedObject parentPrefabInstance;
    public Prefab Prefab
    {
        get
        {
            return parentPrefabInstance.Prefab;
        }
    }
    float holdDown = 0.5f;
    bool held = false;
    Animator animator;
    Animator Animator
    {
        get
        {
            return animator ?? (animator = GetComponent<Animator>());
        }
    }
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
        held = true;
        StartCoroutine(DestroyOnHold());
    }

    void OnMouseUp()
    {
        held = false;
    }
    private void OnDestroy()
    {
        if (PrefabPlacementDeleted != null)
        {
            PrefabPlacementDeleted();
        }
    }

    public void DestructionAnimation()
    {
        InGameSceneScript gameScene;
        if (SingletonUtil.InstanceAvailable(out gameScene))
        {
            StartCoroutine(Destruct(gameScene.vibrationShake,gameScene.destructionDelay));
        }

    }
    IEnumerator Destruct(float shake, float delay)
    {
        if (Animator != null)
        {
            Animator.SetBool("Exterior_Wall_Des", true);
        }
        Vector3 ogPos = transform.position;
        float t = 0;
        while (t < delay)
        {
            t += Time.deltaTime;
            Shake(shake, ogPos);
            yield return 0;
        }
        transform.position = ogPos;
        //
        //yield return new WaitForSeconds(delay);
        IntegratedSoundManager.PlaySoundAsync(SOUNDNAME.DESTRUCTION_0);
        Destroy(gameObject);
    }
    void Shake(float shake, Vector3 ogPos)
    {
        System.Func<float> randomnum = () =>
        {
            return (float)System.Math.Round(Util.rand.NextDouble(-1d, 1d), 3);
        };
        float offsetX = Prefab.snapType == SNAP_POINT_TYPE.CENTRE ? 0f : shake * randomnum();
        float offsetY = shake * randomnum();
        float offsetZ = Prefab.snapType == SNAP_POINT_TYPE.EDGE ? 0f : shake * randomnum();
        transform.position = ogPos + new Vector3(offsetX, offsetY, offsetZ);
    }
}