﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrefabIconScript : UIButton
{
    public int index;
    public Text lblCounter;

    Prefab prefab;
    public Prefab Prefab
    {
        get
        {
            return prefab;
        }
        set
        {
            prefab = value;
            if (lblCounter != null)
            {
                PrefabCounter counter;
                if (SingletonUtil.InstanceAvailable(out counter))
                {
                    lblCounter.text = counter.GetCount(prefab).ToString();
                }
            }
            Sprite sprite;
            if (ResourceManager.GetItem(prefab.type, out sprite))
            {
                Image.sprite = sprite;
            }
        }
    }

    void Awake()
    {
        SetIndex();
    }
    void SetIndex()
    {
        string name = gameObject.name;

        int openBracketIndex = name.LastIndexOf('(');
        int closeBracketIndex = name.LastIndexOf(')');
        if (openBracketIndex > -1 && closeBracketIndex > -1 && closeBracketIndex > openBracketIndex)
        {
            int startIndex = openBracketIndex + 1;
            int length = closeBracketIndex - startIndex;
            string numStr = name.Substring(startIndex, length);
            int n;
            if (int.TryParse(numStr, out n))
            {
                index = n;
            }
            else
            {
                Logger.Log("Num str was {0}.", numStr);
            }
        }
    }
    void SetLabel()
    {
        Text label = GetComponentInChildren<Text>();
        lblCounter = label;
        if (label != null)
        {
            label.name = "lblPrefabIconCounter ({0})".FormatText(index);
        }
    }
    protected override void Start()
    {
        base.Start();
        SetLabel();
        Click += PrefabIconScript_Click;
        PrefabCounter counter;
        if (SingletonUtil.InstanceAvailable(out counter))
        {
            counter.CounterChanged += CounterChanged;
            if (lblCounter != null)
            {
                lblCounter.text = counter.GetCount(prefab).ToString();
            }
        }
    }

    void CounterChanged(Prefab changedPrefab, int counter)
    {
        if (prefab == changedPrefab)
        {
            if (lblCounter != null)
            {
                lblCounter.text = counter.ToString();
            }
            gameObject.SetActive(counter > 0);
        }
    }

    void PrefabIconScript_Click(UIButton sender)
    {
        PrefabCounter counter;
        if (SingletonUtil.InstanceAvailable(out counter))
        {
            counter.SelectPrefab(Prefab);
        }
    }
}