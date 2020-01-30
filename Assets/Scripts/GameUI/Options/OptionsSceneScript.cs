#pragma warning disable IDE0018

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsSceneScript : MonoBehaviour
{
    void Awake()
    {
        SceneObjectScript menu;
        if (SceneObjectScript.InstanceExists(SCENE.Menu,out menu))
        {
            menu.SetActive(false);
        }
    }
}