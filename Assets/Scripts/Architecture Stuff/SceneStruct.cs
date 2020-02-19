using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if SCENESTRUCT

public struct SceneStruct
{
    public SCENE mainScene;
    public List<SCENE> scenes;
    public SceneStruct(SCENE scene)
    {
        mainScene = scene;
        scenes = new List<SCENE>() { scene };
    }
}

#endif