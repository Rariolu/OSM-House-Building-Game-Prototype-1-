#pragma warning disable IDE0017
#pragma warning disable IDE0018
#pragma warning disable IDE0044
#pragma warning disable IDE1005

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class BrochureUtil : NullableInstanceClassSingleton<BrochureUtil>
{
    Contract demoContract;
    public Contract DemoContract
    {
        get
        {
            return demoContract;
        }
    }
    SCENE prevScene;
    public SCENE ParentScene
    {
        get
        {
            return prevScene;
        }
    }
    private BrochureUtil(Contract contract, SCENE parentScene)
    {
        demoContract = contract;
        prevScene = parentScene;
    }
    public static void SetContract(Contract contract, SCENE parentScene)
    {
        SetInstance(new BrochureUtil(contract, parentScene));
        Util.LoadScene(SCENE.Brochure, UnityEngine.SceneManagement.LoadSceneMode.Additive);
    }
}