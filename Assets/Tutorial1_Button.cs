using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial1_Button : MonoBehaviour
{

    public bool IsTutorialActive;
    public GameObject TutorialCanvas;

    // Start is called before the first frame update
    void Start()
    {
        TutorialCanvas.SetActive(true);
    }


    public void Continue()
    {
        IsTutorialActive = false;
        TutorialCanvas.SetActive(false);
    }
}
