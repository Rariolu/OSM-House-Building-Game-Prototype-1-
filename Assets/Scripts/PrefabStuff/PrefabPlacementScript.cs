using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void PrefabPlacementClick();
public class PrefabPlacementScript : MonoBehaviour
{
    public PrefabPlacementClick Click;
    void OnMouseDown()
    {
        if (Click != null)
        {
            Click();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}