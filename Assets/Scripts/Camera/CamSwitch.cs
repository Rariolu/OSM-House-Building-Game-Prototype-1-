using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CamSwitch : MonoBehaviour
{
    public GameObject Cam1;
    public GameObject Cam2;
    bool cam1Active = true;

    void Start()
    {
        Cam1.SetActive(true);
        Cam2.SetActive(false);
    }
    
    public void Camera()
    {
        cam1Active = !cam1Active;
        Cam1.SetActive(cam1Active);
        Cam2.SetActive(!cam1Active);
    }
}