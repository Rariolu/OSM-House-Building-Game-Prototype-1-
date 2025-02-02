﻿#pragma warning disable IDE0018

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A button which returns to the previous scene
/// (also opens game UI if the previous scene happens
/// to be the main game scene).
/// </summary>
public class BackButton : UIButton
{
    public bool getPreviousSceneFromStack = false;
    public SCENE currentScene;
    public SCENE previousScene;
    
    protected override void Start()
    {
        base.Start();
        if (getPreviousSceneFromStack)
        {
            previousScene = Util.PrevMainScene();
            Logger.Log("Prev scene set to {0}", previousScene);
        }
        Click += BackButton_Click;
    }

    //public for now but this is only due to Unity being annoying
    public void BackButton_Click(UIButton sender)
    {
        Util.ReturnToPreviousScene();
    }
}