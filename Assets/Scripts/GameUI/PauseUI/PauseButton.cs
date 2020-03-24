using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButton : UIButton
{
    public GameObject pauseMenuUI;
    
    protected override void Start()
    {
        base.Start();
        Click += PauseButton_Click;
    }
    void PauseButton_Click(UIButton sender)
    {
        Logger.Log("Pause button clicked");
        if (Util.IsPaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }

    void Resume()
    {
        pauseMenuUI.SetActive(false);
        Util.IsPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Util.IsPaused = true;
    }
}
