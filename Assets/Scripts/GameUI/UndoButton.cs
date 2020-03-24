using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndoButton : UIButton
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        Click += UndoButton_Click;
        //Sprite undo;
        //if (ResourceManager.GetItem("UNDO",out undo))
        //{
        //    Image.sprite = undo;
        //}
    }

    void UndoButton_Click(UIButton sender)
    {
        InGameSceneScript gameScene;
        if (SingletonUtil.InstanceAvailable(out gameScene))
        {
            gameScene.Undo();
        }
        else
        {
            Logger.Log("No game scene :'(");
        }
    }
}
