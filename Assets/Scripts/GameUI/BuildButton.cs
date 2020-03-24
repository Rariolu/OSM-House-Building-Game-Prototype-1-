using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildButton : MonoBehaviour
{
    bool IsBuildActive = false;
    public GameObject btnWallPrefabs;
    public GameObject btnFloorPrefabs;
    public GameObject btnRoofPrefabs;

    public void BuildActive()
    {
        IsBuildActive = !IsBuildActive;
        btnWallPrefabs.SetActive(IsBuildActive);
        btnFloorPrefabs.SetActive(IsBuildActive);
        btnRoofPrefabs.SetActive(IsBuildActive);
    }

    
    void Start()
    {
        btnWallPrefabs.SetActive(false);
        btnFloorPrefabs.SetActive(false);
        btnRoofPrefabs.SetActive(false);
    }
}
