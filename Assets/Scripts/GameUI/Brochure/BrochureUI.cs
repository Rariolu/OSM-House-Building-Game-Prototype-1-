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
        BrochureMenuCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

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
