using UnityEngine;
using System.Collections;

public class BuildUIButton : UIButton
{
    bool buildActive = false;
    public GameObject btnWallPrefabs;
    public GameObject btnFloorPrefabs;
    public GameObject btnRoofPrefabs;
    protected override void Start()
    {
        base.Start();
        Click += BuildUIButton_Click;
        btnWallPrefabs.SetActive(false);
        btnFloorPrefabs.SetActive(false);
        btnRoofPrefabs.SetActive(false);
    }
    
    void BuildUIButton_Click(UIButton sender)
    {
        buildActive = !buildActive;
        btnWallPrefabs.SetActive(buildActive);
        btnFloorPrefabs.SetActive(buildActive);
        btnRoofPrefabs.SetActive(buildActive);
    }
}