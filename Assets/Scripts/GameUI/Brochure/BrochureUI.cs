using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrochureUI : MonoBehaviour
{

    public string Menu;
    public bool IsBrochureActive;
    public GameObject BrochureMenuCanvas;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (IsBrochureActive)
        {
            BrochureMenuCanvas.SetActive(true);
            Time.timeScale = 0;
           // Cursor.visible = true;
           // Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            BrochureMenuCanvas.SetActive(false);
            Time.timeScale = 1;
          //  Cursor.visible = false;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            IsBrochureActive = !IsBrochureActive;
            Time.timeScale = 0;
        }
    }

    public void Continue()
    {
        IsBrochureActive = false;
        Time.timeScale = 1;
     //   Cursor.visible = false;
    }

    public void SellHouse()
    {
        Util.LoadScene(SCENE.ContractSelection);
        //Application.LoadLevel("ContractSelection");
    }
}
