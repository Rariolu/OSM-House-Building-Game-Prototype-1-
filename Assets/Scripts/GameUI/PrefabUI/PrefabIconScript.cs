using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void PrefabIconScriptClick(PrefabIconScript sender);

public class PrefabIconScript : MonoBehaviour
{
    Prefab prefab;
    public Prefab Prefab
    {
        get
        {
            return prefab;
        }
        set
        {
            prefab = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnMouseDown()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }


}