using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildButton : MonoBehaviour
{

    public bool IsBuildActive;
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
        {
            btnWallPrefabs.SetActive(true);
        }
        {
            btnFloorPrefabs.SetActive(true);
        }
        {
            btnRoofPrefabs.SetActive(true);
        }
    }

}
