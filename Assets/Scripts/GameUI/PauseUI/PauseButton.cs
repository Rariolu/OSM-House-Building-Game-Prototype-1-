using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButton : UIButton
{
    public GameObject pauseMenuUI;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        Click += PauseButton_Click;
    }
    void PauseButton_Click(UIButton sender)
    {
        Debug.Log("Pause button clicked");
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
        //Time.timeScale = 1f;
        Util.IsPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        //Time.timeScale = 0f;
        Util.IsPaused = true;
    }
}
