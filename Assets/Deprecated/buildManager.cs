using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if BUILDMANAGER

public class buildManager : MonoBehaviour
{
    public GameObject foundation;

    public buildSystem BuildSystem;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            BuildSystem.NewBuild(foundation);
        }
    }
}

#endif