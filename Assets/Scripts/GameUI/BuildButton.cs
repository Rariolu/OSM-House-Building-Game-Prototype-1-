using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildButton : MonoBehaviour
{

    bool IsBuildActive = false;
    public GameObject btnWallPrefabs;
    public GameObject btnFloorPrefabs;
    public GameObject btnRoofPrefabs;

    // Start is called before the first frame update
    void Start()
    {
        btnWallPrefabs.SetActive(false);
        btnFloorPrefabs.SetActive(false);
        btnRoofPrefabs.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void BuildActive()
    {
        IsBuildActive = !IsBuildActive;
        
        btnWallPrefabs.SetActive(IsBuildActive);
        
        
        btnFloorPrefabs.SetActive(IsBuildActive);
        
        
        btnRoofPrefabs.SetActive(IsBuildActive);
        
    }

}
