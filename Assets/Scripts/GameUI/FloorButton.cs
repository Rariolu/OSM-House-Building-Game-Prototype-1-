#pragma warning disable IDE0017
#pragma warning disable IDE0018
#pragma warning disable IDE0039
#pragma warning disable IDE0044
#pragma warning disable IDE1005

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(RawImage))]
public class FloorButton : MultitonUIButton<FloorButton, FLOORTYPE>
{
    public FLOORTYPE floorType;
    static FLOORTYPE currentFloor = FLOORTYPE.GROUND_FLOOR;
    public static FLOORTYPE CurrentFloor
    {
        get
        {
            return currentFloor;
        }
        private set
        {
            currentFloor = value;
        }
    }
    public bool initiallyVisible = false;
    void Awake()
    {
        SetInstance(floorType, this);
    }
    
    protected override void Start()
    {
        base.Start();
        Click += FloorButton_Click;
        MakeVisible(initiallyVisible);
    }

    void FloorButton_Click(UIButton button)
    {
        MakeVisible(false);

        CurrentFloor = floorType;

        Floor floor;

        if (Floor.InstanceExists(floorType, out floor))
        {
            floor.Focus();
        }

        CompartPrefabSystem compartSystem;
        if (SingletonUtil.InstanceAvailable(out compartSystem))
        {
            compartSystem.SwitchFloors(floorType);
        }

        const int floorQuantity = 3;

        //Get next floor (or ground if already on top floor)
        FLOORTYPE key = (FLOORTYPE)(((int)floorType + 1) % floorQuantity);

        FloorButton otherButton;
        if (InstanceExists(key, out otherButton))
        {
            otherButton.MakeVisible();
        }
    }
    public void MakeVisible(bool visible = true)
    {
        RawImage.enabled = visible;
        //Image.enabled = visible;
        foreach(Transform child in transform)
        {
            child.gameObject.SetActive(visible);
        }
    }
}