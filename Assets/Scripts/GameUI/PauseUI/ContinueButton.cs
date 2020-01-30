using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueButton : UIButton
{
    public GameObject pauseMenuUI;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        Click += ContinueButton_Click;
    }
    void ContinueButton_Click(UIButton sender)
    {
        pauseMenuUI.SetActive(false);
        //Time.timeScale = 1;
        Util.IsPaused = false;
    }
}