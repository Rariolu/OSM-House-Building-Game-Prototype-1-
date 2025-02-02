﻿#pragma warning disable IDE0017
#pragma warning disable IDE0018
#pragma warning disable IDE0044
#pragma warning disable IDE1005

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The script which generally manages the "Fixings" scene.
/// </summary>
public class FixingsScene : MonoBehaviour
{
    void Start()
    {
        FixingsUtil fixingsUtil;
        if (SingletonUtil.InstanceAvailable(out fixingsUtil))
        {
            InGameSceneScript gameScene;
            if (SingletonUtil.InstanceAvailable(out gameScene))
            {
                int fixs = gameScene.AvailableFixtures;
                fixingsUtil.Fixings = fixs;
            }
            else
            {
                fixingsUtil.Fixings = 4;
            }

            foreach (FIXINGSECTION fSection in fixingsUtil.CurrentIntersection.FixingSections)
            {
                Fixings fixings;
                if (Fixings.InstanceExists(fSection, out fixings))
                {
                    fixings.Confirm();
                }
            }
        }
    }
}