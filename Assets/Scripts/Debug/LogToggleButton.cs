using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// A button which makes the UI log active and inactive.
/// </summary>
public class LogToggleButton : UIButton
{
    public Image log;
    
    protected override void Start()
    {
        base.Start();
        Click += LogToggleButton_Click;
    }
    void LogToggleButton_Click(UIButton sender)
    {
        if (log != null)
        {
            log.gameObject.SetActive(!log.gameObject.activeSelf);
        }
        else
        {
            Logger.Log("Log is null", LogType.Warning);
        }
    }
}