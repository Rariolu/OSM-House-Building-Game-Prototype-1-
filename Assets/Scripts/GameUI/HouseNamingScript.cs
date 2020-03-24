using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HouseNamingScript : MonoBehaviour
{
    public InputField houseName;

    void Update()
    {
        if (Input.GetKey(KeyCode.Return))
        {
            Util.LoadScene(SCENE.EndScene);
        }
    }

    public void NameHouseMenu()
    {
        Logger.Log("House Name is:" + houseName.text);
    }
}