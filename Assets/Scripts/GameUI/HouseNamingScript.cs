﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HouseNamingScript : MonoBehaviour
{

    public InputField houseName;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Return))
        {
            Util.LoadScene(SCENE.EndScene);
            //Application.LoadLevel("EndScene");
        }
    }

    public void NameHouseMenu()
    {
        Debug.Log("House Name is:" + houseName.text);

       
    }
}