﻿#pragma warning disable IDE0017
#pragma warning disable IDE0018
#pragma warning disable IDE0039
#pragma warning disable IDE0044
#pragma warning disable IDE1005

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// A script which is attached to a gameobject which has
/// all gameobjects in the scene attached to it as child
/// objects (so that something that happens to this object
/// can happen to all objects in the scene).
/// </summary>
public class SceneObjectScript : MultitonScript<SceneObjectScript,SCENE>//MonoBehaviour
{

    /// <summary>
    /// A "SCENE" enum value which represents the specific
    /// scene that this instance is used for.
    /// </summary>
    public SCENE scene;
    public bool loadResourceLoader = true;
    public bool ShowTestScene = false;
    /// <summary>
    /// A method which adds this scene to the "instances" dictionary.
    /// It's run by Unity before all "Start" methods which
    /// allows all "Start" methods elsewhere to use the instance
    /// immediately.
    /// </summary>
    private void Awake()
    {
        if (loadResourceLoader)
        {
            Util.LoadResources();
        }
        //if (!Util.SceneLoaded(SCENE.ResourceLoader) && loadResourceLoader)
        //{
        //    Util.LoadScene(SCENE.ResourceLoader, LoadSceneMode.Additive, false);
        //}
       
        string sceneName = gameObject.scene.name;
        
        if (scene.ToString().NormaliseString() != sceneName.NormaliseString())
        {
            //Logger.Log("Scene \"{0}\" has SceneObject with value \"{1}\".",sceneName,scene);
            SCENE parsedScene;
            if (Util.EnumTryParse(sceneName,out parsedScene))
            {
                scene = parsedScene;
                //Logger.Log("SceneObject enum value changed to {0}.", scene);
            }
            else
            {
                Logger.Log("No viable scene enum value found for this scene's name.");
            }
        }
        SetInstance(scene, this);
        if (Util.StackCount < 1)
        {
            Util.PushScene(scene);
        }
    }
    void Start()
    {

#if DEVELOPMENT_BUILD
        ShowTestScene = true;
#endif
        if (!Util.SceneLoaded(SCENE.SpecTestScene) && ShowTestScene)
        {
            Util.ShowTest();
        }
    }
    /// <summary>
    /// Makes the attached gameobject inactive
    /// (which should make all objects in the
    /// scene inactive).
    /// </summary>
    /// <param name="active"></param>
    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
        Logger.Log("Setting {0} to active state {1}", scene, active);
    }

    private void OnDestroy()
    {
        RemoveInstance(scene);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Util.Quit();
        }
    }
}