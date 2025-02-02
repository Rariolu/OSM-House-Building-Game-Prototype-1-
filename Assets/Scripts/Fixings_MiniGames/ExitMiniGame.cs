﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

/// <summary>
/// A UIButton which exists the "Fixings" minigame, storing the chosen
/// fixture points inside the necessary "Intersection".
/// </summary>
public class ExitMiniGame : UIButton
{
    public SCENE scene;
    public LoadSceneMode loadSceneMode = LoadSceneMode.Single;
    protected override void Start()
    {
        base.Start();
        Click += SceneLoad_Click;
    }
    void SceneLoad_Click(UIButton sender)
    {
 
        FixingsUtil fixingsUtil;
        if (SingletonUtil.InstanceAvailable(out fixingsUtil))
        {
            InGameSceneScript gameScene;
            if (SingletonUtil.InstanceAvailable(out gameScene))
            {
                //gameScene.AvailableFixtures = Fixings.fixings;
                gameScene.AvailableFixtures = fixingsUtil.Fixings;
            }
            List<FIXINGSECTION> fixingSections = new List<FIXINGSECTION>();
            foreach(FIXINGSECTION fSection in Enum.GetValues(typeof(FIXINGSECTION)))
            {
                Fixings fixings;
                if (Fixings.InstanceExists(fSection,out fixings))
                {
                    if (fixings.Confirmed)
                    {
                        fixingSections.Add(fSection);
                    }
                }
            }
            if (fixingsUtil.CurrentIntersection.FixingsPreviouslySet)
            {
                if (!Util.CollectionsEqual(fixingsUtil.CurrentIntersection.FixingSections,fixingSections))
                {
                    ConstructionUtil constructionUtil;
                    if (SingletonUtil.InstanceAvailable(out constructionUtil))
                    {
                        constructionUtil.NewSetOfFixings();
                    }
                }
            }
            fixingsUtil.CurrentIntersection.SetFixingSections(fixingSections);
        }
        SceneObjectScript prevScene;
        if (SceneObjectScript.InstanceExists(scene, out prevScene))
        {
            prevScene.SetActive(true);
        }
        Util.UnloadScene(SCENE.Fixings_MiniGame);
    }
}