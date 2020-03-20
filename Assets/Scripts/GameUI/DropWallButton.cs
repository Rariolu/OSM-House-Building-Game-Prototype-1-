using UnityEngine;
using System.Collections;

public class DropWallButton : NullableInstanceUIButtonSingleton<DropWallButton>
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
        SetInstance(this);
    }
    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        Click += DropWallButton_Click;
        CameraMovementScript cameraScript;
        if (CameraMovementScript.InstanceAvailable(out cameraScript))
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