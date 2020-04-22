using System.Collections;
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
     
        if (Animator != null)
        {
            StartCoroutine(Destruct());
        }
    }
    const float delay = 1f;
    IEnumerator Destruct()
    {
        Animator.SetBool("Exterior_Wall_Des", true);
        yield return new WaitForSeconds(delay);
        IntegratedSoundManager.PlaySoundAsync(SOUNDNAME.DESTRUCTION_0);
        Destroy(gameObject);
    }
}