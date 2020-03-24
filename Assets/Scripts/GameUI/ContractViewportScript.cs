using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContractViewportScript : UIButton
{
    public GameObject ContractButtonUI;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        Click += ContractButton_Click;
    }


    void ContractButton_Click(UIButton sender)
    {
        Logger.Log("Contract button clicked");
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
        ContractButtonUI.SetActive(false);
        Util.IsPaused = false;
    }

    void Pause()
    {
        ContractButtonUI.SetActive(true);
        Util.IsPaused = true;
    }
}
