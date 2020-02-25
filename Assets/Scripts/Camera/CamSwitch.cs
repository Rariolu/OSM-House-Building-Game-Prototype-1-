using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamSwitch : MonoBehaviour
{
    public GameObject Cam1;
    public GameObject Cam2;

    void Start()
    {
        Cam1.SetActive(true);
        Cam2.SetActive(false);
    }

    void Update()
    {
        if (Input.GetButtonDown("1Key"))
        {
            Cam1.SetActive(true);
            Cam2.SetActive(false);
        }
        if (Input.GetButtonDown("2Key"))
        {
            Cam1.SetActive(false);
            Cam2.SetActive(true);
        }
    }
}
