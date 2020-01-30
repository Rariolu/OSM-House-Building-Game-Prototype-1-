using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

public class SceneLoadButton : UIButton
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
        Util.LoadScene(scene, loadSceneMode);
    }
}