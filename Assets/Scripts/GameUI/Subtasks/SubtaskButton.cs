using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubtaskButton : UIButton
{
    public Image subtaskPanel;
    bool panelActive = false;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        Click += SubtaskButton_Click;
    }

    void SubtaskButton_Click(UIButton sender)
    {
        panelActive = !panelActive;
        if (subtaskPanel != null)
        {
            subtaskPanel.gameObject.SetActive(panelActive);
        }
        else
        {
            Logger.Log("subtaskPanel not provided");
        }
    }
}
