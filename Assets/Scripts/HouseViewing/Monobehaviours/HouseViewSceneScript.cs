using UnityEngine;
using System.Collections;

public class HouseViewSceneScript : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        HouseViewSingle houseView;
        if (HouseViewSingle.InstanceAvailable(out houseView))
        {
            House house = houseView.CurrentHouse;
            house.Instantiate();
        }
        SceneObjectScript brochureScene;
        if (SceneObjectScript.InstanceExists(SCENE.Brochure, out brochureScene))
        {
            brochureScene.SetActive(false);
        }
    }
}