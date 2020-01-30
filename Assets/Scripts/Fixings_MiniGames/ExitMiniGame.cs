using System;
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
        InGameSceneScript gameScene;
        if (InGameSceneScript.InstanceAvailable(out gameScene))
        {
            gameScene.AvailableFixtures = Fixings.fixings;
        }
        FixingsUtil fixingsUtil;
        if (FixingsUtil.InstanceAvailable(out fixingsUtil))
        {
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
            fixingsUtil.CurrentIntersection.SetFixingSections(fixingSections);
        }
        SceneObjectScript prevScene;
        if (SceneObjectScript.InstanceExists(scene, out prevScene))
        {
            prevScene.SetActive(true);
            if (scene == SCENE.InGame)
            {
                SceneObjectScript gameUIScene;
                if (SceneObjectScript.InstanceExists(SCENE.InGameUI, out gameUIScene))
                {
                    gameUIScene.SetActive(true);
                }
            }
        }
        Util.UnloadScene(SCENE.Fixings_MiniGame);
    }
}