#pragma warning disable IDE0018

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
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        if (getPreviousSceneFromStack)
        {
            previousScene = Util.PrevMainScene();
            Debug.LogFormat("Prev scene set to {0}", previousScene);
        }
        Click += BackButton_Click;
    }
    //public for now but this is only due to Unity being annoying
    public void BackButton_Click(UIButton sender)
    {
        SceneObjectScript prev;
        if (SceneObjectScript.InstanceExists(previousScene,out prev))
        {
            prev.SetActive(true);
            SceneObjectScript uiScene;
            if (previousScene == SCENE.InGame && SceneObjectScript.InstanceExists(SCENE.InGameUI,out uiScene))
            {
                uiScene.SetActive(true);
            }
            Util.UnloadScene(currentScene);
        }
        else
        {
            Debug.LogFormat("Previous Scene {0} doesn't exist :/",previousScene);
        }
    }
}