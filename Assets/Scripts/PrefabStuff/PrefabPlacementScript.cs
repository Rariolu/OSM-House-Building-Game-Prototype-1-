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
    const float vibRange = 1f;
    float dur = 0.1f;
    IEnumerator Destruct()
    {
        Vector3 ogPos = transform.position;
        float minX = ogPos.x - vibRange;
        float maxX = ogPos.x + vibRange;
        float minY = ogPos.y - vibRange;
        float maxY = ogPos.y + vibRange;
        float minZ = ogPos.z - vibRange;
        float maxZ = ogPos.z + vibRange;
        float t = 0;
        while (t < delay)
        {
            t += Time.deltaTime;
            float x = ogPos.x + Mathf.PerlinNoise(ogPos.x, t*100f);
            float y = ogPos.y + Mathf.PerlinNoise(ogPos.y, t*100f);
            float z = ogPos.z + Mathf.PerlinNoise(ogPos.z, t*100f);
            Logger.Log("X: {0}; Y: {1}; Z: {2};", x, y, z);
            transform.position = new Vector3(x, y, z);
        }
        //Animator.SetBool("Exterior_Wall_Des", true);
        yield return new WaitForSeconds(delay);
        IntegratedSoundManager.PlaySoundAsync(SOUNDNAME.DESTRUCTION_0);
        Destroy(gameObject);
    }
}