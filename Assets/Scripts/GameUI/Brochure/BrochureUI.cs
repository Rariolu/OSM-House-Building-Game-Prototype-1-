using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrochureUI : MonoBehaviour
{

    public string Menu;
    public bool IsBrochureActive;
    public GameObject BrochureMenuCanvas;

    
    void Start()
    {
        BrochureMenuCanvas.SetActive(false);
    }

	bool brochureActive = false;
    public void BrochureActive()
    {
		brochureActive = !brochureActive;
        BrochureMenuCanvas.SetActive(brochureActive);
    }
    public void Continue()
    {
        IsBrochureActive = false;
        Time.timeScale = 1;
    }

    public void SellHouse()
    {

        Util.LoadScene(SCENE.ContractSelection);
    }
}
