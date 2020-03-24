#pragma warning disable IDE0017
#pragma warning disable IDE0018
#pragma warning disable IDE0044
#pragma warning disable IDE1005

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Image))]
public class HouseViewerButton : UIButton
{
    
    protected override void Start()
    {
        base.Start();
        Click += HouseViewerButton_Click;
        Sprite sprite;
        if (ResourceManager.GetItem("BROCHURE", out sprite))
        {
            Image.sprite = sprite;
        }
    }
    void HouseViewerButton_Click(UIButton sender)
    {
        ConstructionUtil util;
        if (SingletonUtil.InstanceAvailable(out util))
        {
            DeActivateGameScenes();
            BrochureUtil.SetContract(util.Contract, SCENE.InGame);
        }
    }
    void DeActivateGameScenes()
    {
        SceneObjectScript gameScene;
        if (SceneObjectScript.InstanceExists(SCENE.InGame, out gameScene))
        {
            gameScene.SetActive(false);
        }
    }
}