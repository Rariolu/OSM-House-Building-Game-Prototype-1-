#pragma warning disable IDE0017
#pragma warning disable IDE0018
#pragma warning disable IDE0044
#pragma warning disable IDE1005

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class HouseDemo : UIButton
{
    protected override void Start()
    {
        base.Start();
        Sprite sprite;
        if (ResourceManager.GetItem("ShowHouse", out sprite))
        {
            Image.sprite = sprite;
        }
        Click += HouseDemo_Click;
    }
    void HouseDemo_Click(UIButton sender)
    {
        BrochureUtil util;
        if (BrochureUtil.InstanceAvailable(out util))
        {   
            Mesh m;
            if (ResourceManager.GetItem(util.DemoContract.finishedConstruction, out m))
            {
                House h = new House(util.DemoContract.name, m);
                Util.ShowHouse(h);
            }
            else
            {
                Debug.Log("No mesh");
            }
        }
        else
        {
            Debug.Log("No util");
        }
    }
}