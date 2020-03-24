using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueButton : UIButton
{
    public GameObject pauseMenuUI;
    
    protected override void Start()
    {
        base.Start();
        Click += ContinueButton_Click;
    }
    void ContinueButton_Click(UIButton sender)
    {
        pauseMenuUI.SetActive(false);
        Util.IsPaused = false;
    }
}