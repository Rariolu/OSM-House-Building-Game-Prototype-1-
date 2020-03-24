using UnityEngine;
using System.Collections;

public class HouseViewSceneScript : MonoBehaviour
{
    
    void Start()
    {
        HouseViewSingle houseView;
        if (SingletonUtil.InstanceAvailable(out houseView))
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