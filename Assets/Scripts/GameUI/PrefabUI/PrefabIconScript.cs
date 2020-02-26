using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrefabIconScript : UIButton//MultitonUIButton<PrefabIconScript,int>
{
    //static Dictionary<PREFAB_COMPART, AutoList<PrefabIconScript>> icons = new Dictionary<PREFAB_COMPART, AutoList<PrefabIconScript>>();
    //static void AddIcon(PREFAB_COMPART compart, int index, PrefabIconScript icon)
    //{
    //    if (icons.ContainsKey(compart))
    //    {
    //        icons[compart].AddOutboundElement(index, icon);
    //    }
    //    else
    //    {
    //        AutoList<PrefabIconScript> list = new AutoList<PrefabIconScript>();
    //        list.AddOutboundElement(index, icon);
    //        icons.Add(compart, list);
    //    }
    //}
    //public static PrefabIconScript[] GetIcons(PREFAB_COMPART compart)
    //{
    //    if (!icons.ContainsKey(compart))
    //    {
    //        return new PrefabIconScript[0];
    //    }
    //    return icons[compart].ToArray();
    //}
    //public static PrefabIconScript[] GetIcons(PREFAB_COMPART compart)
    //{
    //    List<PrefabIconScript> icons = new List<PrefabIconScript>();
    //    foreach(PrefabIconScript icon in Values)
    //    {
    //        if (icon.compart == compart)
    //        {
    //            icons.Add(icon);
    //        }
    //    }
    //    return icons.ToArray();
    //}

    public int index;
    //public PREFAB_COMPART compart;
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
                if (PrefabCounter.InstanceAvailable(out counter))
                {
                    lblCounter.text = counter.GetCount(prefab).ToString();
                }
            }
            Sprite sprite;
            if (ResourceManager.GetItem(prefab.type,out sprite))
            {
                Image.sprite = sprite;
            }
        }
    }

    void Awake()
    {
        SetIndex();
        //SetInstance(new PrefabSelectionButtonIndex(index, compart), this);
        //AddIcon(compart, index, this);
        //SetInstance(index, this);
        //prefabIcons.Add(this);
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
                Debug.LogFormat("Num str was {0}.", numStr);
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
        if (PrefabCounter.InstanceAvailable(out counter))
        {
            counter.CounterChanged += CounterChanged;
            if (lblCounter != null)
            {
                lblCounter.text = counter.GetCount(prefab).ToString();
            }
        }
        //gameObject.SetActive(false);
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
        if (PrefabCounter.InstanceAvailable(out counter))
        {
            counter.SelectPrefab(Prefab);
        }
    }
}