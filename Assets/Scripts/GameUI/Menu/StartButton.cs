using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : UIButton
{
    protected override void Start()
    {
        base.Start();
        Click += StartButton_Click;
    }
    void StartButton_Click(UIButton sender)
    {
        Util.StartGame();
    }
}