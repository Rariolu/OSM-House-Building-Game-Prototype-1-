using UnityEngine;
using System;
using System.Collections;

public class PrefabLoader : MonoBehaviour
{
    //https://answers.unity.com/questions/145262/naming-array-elements-in-editor.html
    [LabeledArray(typeof(PREFABTYPE))]
    public PrefabResource[] prefabs;
    private void Awake()
    {
        foreach(PrefabResource prefab in prefabs)
        {
            RescItem<GameObject> go = new RescItem<GameObject>(prefab.type.ToString(), prefab.gameObject);
            RescItem<Sprite> sprite = new RescItem<Sprite>(prefab.type.ToString(),prefab.sprite);

            ResourceManager.AddItem(go);
            ResourceManager.AddItem(sprite);
        }
    }
}

[Serializable]
public struct PrefabResource
{
    [SerializeField]
    public PREFABTYPE type;

    [SerializeField]
    public GameObject gameObject;

    [SerializeField]
    public Sprite sprite;

    public override string ToString()
    {
        return type.ToString();
    }
}