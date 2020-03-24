using UnityEngine;
using System.Collections;

public class DropWallButton : UIButton
{
    bool dropped = false;
    public bool Dropped
    {
        get
        {
            return dropped;
        }
        set
        {
            dropped = value;
            foreach (PrefabPlacedObject ppo in PrefabPlacedObject.Values)
            {
                ppo.Drop(Dropped);
            }
        }
    }
    private void Awake()
    {
        SingletonUtil.SetInstance(this);
    }
    
    protected override void Start()
    {
        base.Start();
        Click += DropWallButton_Click;
        CameraMovementScript cameraScript;
        if (SingletonUtil.InstanceAvailable(out cameraScript))
        {
            cameraScript.CameraMoved += (c) => { Dropped = false; };
        }
    }
    void DropWallButton_Click(UIButton sender)
    {
        //dropped = !dropped;
        Dropped = !Dropped;
    }
}