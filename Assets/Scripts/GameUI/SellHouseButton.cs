using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellHouseButton : UIButton
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        Click += SellHouseButton_Click;
    }

    void SellHouseButton_Click(UIButton sender)
    {
        Util.SellHouse();
    }
}
