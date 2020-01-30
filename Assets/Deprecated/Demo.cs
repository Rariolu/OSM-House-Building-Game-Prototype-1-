#pragma warning disable IDE0017
#pragma warning disable IDE0018
#pragma warning disable IDE0044
#pragma warning disable IDE1005

//#define DEMO

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if DEMO

public class Demo : MonoBehaviour
{
    public Prefab prefabProperties;
    // Declaring the prefabs that are used in the game
    public GameObject prefab;
    // Declaring my renderer
    private MeshRenderer myRender;
    // Declaring materials for when a placement is valid or invalid
    public Material validMat;
    public Material nonValidMat;
    // Declaring the build system
    private buildSystem buildSystem;
    // Creating a boolean to determine if a prefab is snapped to another
    private bool isSnapped = false;
    // Creating a boolean to determine if a prefab is considered a foundation
    private bool isFoundation = false;
    //public List<string> tagsIsSnapTo = new List<string>();
    public string snapTag = "TESTTAG";
    void ChangeColour()
    {
        // If a prefab is either snapped to another or is a foundation it can be placed
        if (isSnapped)
        {
            // Sets the material for the renderer to valid
            myRender.material = validMat;
            // Sets isSnapped to true
        }
        else
        {
            // If not the material is set to invalid
            myRender.material = nonValidMat;
        }

		if(isFoundation)
		{
		
			myRender.material = validMat;
			isSnapped = true;

		}
    }
    //SnapPointTrigger snappedTrigger;
    void OnTriggerEnter(Collider other)
    {
        if (other.tag.StringEquals(snapTag))
        {
            SnapPointTrigger sTrigger = other.gameObject.GetComponent<SnapPointTrigger>();
            if (!sTrigger.Snapped)
            {
                //snappedTrigger = sTrigger;
                // Calls the PauseBuild method from the "buildSystem" script
                buildSystem.PauseBuild(true);
                // Sets the position of the prefab to the posistion of the snap point
                transform.position = other.transform.position;
                // "isSnapped" is set to true
                isSnapped = true;
                buildSystem.snapCollider = other.GetComponent<Collider>();
                // Calls the ChangeColour method from BuildSystems script
                ChangeColour();
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag.StringEquals(snapTag))
        {
            // "isSnapped" is set to false
            isSnapped = false;
            // Calls the ChangeColour method from BuildSystem
            ChangeColour();
        }
    }
    public void Place()
    {
        PrefabPlacedObject ppo = new PrefabPlacedObject(prefabProperties, transform.position.RoundVec3());//,snappedTrigger.transform.parent.position);
        ppo.SetMaterial(validMat);
        InGameSceneScript gameSceneScript;
        if (InGameSceneScript.InstanceAvailable(out gameSceneScript))
        {
            gameSceneScript.AddPlacement(ppo);
        }
        Destroy(prefab);
    }
    public bool Snapped()
    {
        // returns if the prefab is snapped to another or not
        return isSnapped;
    }
    public bool isSpawned = true;
    // Start is called before the first frame update
    private void Start()
    {
        buildSystem = GameObject.FindObjectOfType<buildSystem>();
        // Sets "myRender" to the Mesh Renderer
        myRender = GetComponent<MeshRenderer>();
        // Calls the ChangeColour method
        ChangeColour();
        if (isSpawned)
        {
            Vector3 point;
            if (Util.GetMouseHit(GameObject.FindObjectOfType<Camera>(), out point))
            {
                transform.position = new Vector3(point.x, 0, point.z);
            }
            else
            {
                transform.position = new Vector3(0, 0, -30f);
            }
        }
    }
}

#endif