﻿#pragma warning disable IDE0017
#pragma warning disable IDE0018
#pragma warning disable IDE0044
#pragma warning disable IDE1005

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public delegate void FixturesChanged(int fixtures);
public delegate void PrefabDestroyed();
public delegate void PrefabPlaced();
public delegate void IntersectionSpawned();
public delegate void IntersectionClicked();

/// <summary>
/// A script attached to a GameObject in the main game scene which manages its
/// operation and resources.
/// </summary>
public class InGameSceneScript : MonoBehaviour
{
    #region PropertiesAndVariables

    int availableFixtures;
    /// <summary>
    /// The quantity of remaining fixtures.
    /// </summary>
    public int AvailableFixtures
    {
        get
        {
            return availableFixtures;
        }
        set
        {
            availableFixtures = value;
            if (FixturesChanged != null)
            {
                FixturesChanged(availableFixtures);
            }
        }
    }

    Contract currentContract;
    public FixturesChanged FixturesChanged;
    public IntersectionSpawned IntersectionSpawned;
    Dictionary<MATERIAL, int> matQuantities = new Dictionary<MATERIAL, int>();

    public Image[] pbBlueprints;
    public Image pbHouse;
    public MaterialQuantity[] materialQuantites;
    public string xmlBackupFile = "Assets\\Contracts\\Semi-Detached House_SEMI_DETACHED_HOUSE_0.xml";

    public float destructionDelay = 2f;
    public float vibrationShake = 1f;

    #endregion

    void Awake()
    {
        SingletonUtil.SetInstance(this);
        PrefabCounter.CreatePrefabCounter();
        PrefabPlacedObject.Clear();
        Intersection.Clear();
        Util.PreventCollisions(LAYER.DEFAULT, LAYER.IntersectionLayer);
        ConstructionUtil util;
        if (SingletonUtil.InstanceAvailable(out util))
        {
            currentContract = util.Contract;
            AvailableFixtures = currentContract.fixtures;
        }
#if UNITY_EDITOR
        else
        {
            Contract contract;
            Logger.Log("Attempting to load \"{0}\".", xmlBackupFile);
            if (XMLUtil.LoadContract(xmlBackupFile, out contract))
            {
                Logger.Log("Loaded xml file.");
                ConstructionUtil.SetContract(contract);
                currentContract = contract;
                AvailableFixtures = currentContract.fixtures;
            }
        }
#endif
    }

    public PrefabPlaced PrefabPlaced;

    public void MaterialPlaced(MATERIAL material)
    {
        if (matQuantities.ContainsKey(material))
        {
            int soundNum = Util.rand.Next(matQuantities[material]);
            string name = "{0}_{1}".FormatText(material, soundNum);
            IntegratedSoundManager.PlaySoundAsync(name);
        }
        if (PrefabPlaced != null)
        {
            PrefabPlaced();
        }
    }

    void Start()
    {
        Floor groundFloor;
        if (Floor.InstanceExists(FLOORTYPE.GROUND_FLOOR, out groundFloor))
        {
            groundFloor.Focus();
        }
        foreach (MaterialQuantity mq in materialQuantites)
        {
            if (matQuantities.ContainsKey(mq.material))
            {
                matQuantities[mq.material] = mq.number;
            }
            else
            {
                matQuantities.Add(mq.material, mq.number);
            }
        }
        ConstructionUtil util;
        if (SingletonUtil.InstanceAvailable(out util))
        {
            for(int i = 0; i < pbBlueprints.Length; i++)
            {
                SetFloorPlan(util.Contract.finishedConstruction, i, pbBlueprints[i]);
            }

            if (pbHouse != null)
            {
                Sprite house;
                if (ResourceManager.GetItem(util.Contract.name + "_House", out house))
                {
                    pbHouse.sprite = house;
                }
            }
            else
            {
                Logger.Log("pbHouse is null.");
            }
        }
    }

    void SetFloorPlan(FINISHED_CONSTRUCTION conType, int floor, Image pbFloorPlan)
    {
        string strFloor = "{0}_floorplan_{1}".FormatText(conType, floor);
        Sprite sprite;
        if (ResourceManager.GetItem(strFloor, out sprite))
        {
            pbFloorPlan.sprite = sprite;
        }
        else
        {
            pbFloorPlan.gameObject.SetActive(false);
            Logger.Log("{0} not found in resource manager.", LogType.Warning, strFloor);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            Util.SetExitState(EndConditionUtil.Pass());
        }
    }
    public PrefabDestroyed PrefabDestroyed;
    public void PrefabDestroy()
    {
        if (PrefabDestroyed != null)
        {
            PrefabDestroyed();
        }
    }

    public void IntersectionSpawn()
    {
        if (IntersectionSpawned != null)
        {
            IntersectionSpawned();
        }
    }

    public IntersectionClicked IntersectionClicked;
    public void IntersectionClick()
    {
        if (IntersectionClicked != null)
        {
            IntersectionClicked();
        }
    }
}

/// <summary>
/// A struct that determines the quantity of sounds
/// to used for a particular material.
/// </summary>
[Serializable]
public struct MaterialQuantity
{
    public MATERIAL material;
    public int number;
}