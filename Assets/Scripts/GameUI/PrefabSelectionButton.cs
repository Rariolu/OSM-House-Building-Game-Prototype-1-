using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class PrefabSelectionButton : MultitonUIButton<PrefabSelectionButton, PREFAB_COMPART>
{
    static List<PREFAB_COMPART> keys = new List<PREFAB_COMPART>();
    public static PREFAB_COMPART[] Keys
    {
        get
        {
            return keys.ToArray();
        }
    }
    public float a = 0.06f;
    FLOORTYPE currentFloorType = FLOORTYPE.GROUND_FLOOR;
    public Vector2 iconSize = new Vector2(1, 0.05f);
    public float iconXValue = 0;
    public int maxColumnLength = 7;
    public float initialYvalue = 0.52f;
    GameObject parentObject;
    Dictionary<Prefab, PrefabCollection> prefabCollections = new Dictionary<Prefab, PrefabCollection>();
    public PREFAB_COMPART prefabCompart;
    Dictionary<FLOORTYPE, Dictionary<Prefab, PrefabIconMultipler>> separateFloorPrefabIconDict = new Dictionary<FLOORTYPE, Dictionary<Prefab, PrefabIconMultipler>>();
    Dictionary<FLOORTYPE, List<PrefabIconMultipler>> separatedFloorPrefabIcons = new Dictionary<FLOORTYPE, List<PrefabIconMultipler>>();
    
    /// <summary>
    /// Add prefab to panel.
    /// </summary>
    /// <param name="prefab"></param>
    public void AddPrefab(Prefab prefab)
    {
        if (separateFloorPrefabIconDict.ContainsKey(prefab.floorType))
        {
            if (separateFloorPrefabIconDict[prefab.floorType].ContainsKey(prefab))
            {
                separateFloorPrefabIconDict[prefab.floorType][prefab].Count++;
            }
            else
            {
                int count = separatedFloorPrefabIcons.ContainsKey(prefab.floorType) ? separatedFloorPrefabIcons[prefab.floorType].Count : 0;

                int num = count % maxColumnLength;Debug.Log("Num is " + num);
                float yOffset = num * a;Debug.Log("yOffset is " + yOffset);
                float y = ((count % maxColumnLength) * a) + initialYvalue;//0.5f;
                float x = (count / maxColumnLength) * (iconSize.x + 0.01f);
                PrefabIconMultipler pim = new PrefabIconMultipler(prefabCollections[prefab], count);
                pim.Count = 1;
                pim.Click += Icon_Click;
                pim.SetParent(parentObject.transform);
                pim.SetPosition(new Vector2(x, y),true);
                pim.SetSize(iconSize, true);
                if (separatedFloorPrefabIcons.ContainsKey(prefab.floorType))
                {
                    separatedFloorPrefabIcons[prefab.floorType].Add(pim);
                }
                else
                {
                    separatedFloorPrefabIcons.Add(prefab.floorType, new List<PrefabIconMultipler>() { pim });
                }
                if (separateFloorPrefabIconDict.ContainsKey(prefab.floorType))
                {
                    separateFloorPrefabIconDict[prefab.floorType].Add(prefab, pim);
                }
                else
                {
                    separateFloorPrefabIconDict.Add(prefab.floorType, new Dictionary<Prefab, PrefabIconMultipler>() { { prefab, pim } });
                }
            }
        }
    }
    public void AddPrefabs(List<PrefabCollection> prefabs)
    {
        if (prefabs.Count < 1)
        {
            return;
        }

        Dictionary<FLOORTYPE, float> yVals = new Dictionary<FLOORTYPE, float>()
        {
            {FLOORTYPE.GROUND_FLOOR,initialYvalue},
            {FLOORTYPE.FIRST_FLOOR,initialYvalue},
            {FLOORTYPE.SECOND_FLOOR,initialYvalue}
        };

        Dictionary<FLOORTYPE, float> xVals = new Dictionary<FLOORTYPE, float>()
        {
            {FLOORTYPE.GROUND_FLOOR,iconXValue},
            {FLOORTYPE.FIRST_FLOOR,iconXValue},
            {FLOORTYPE.SECOND_FLOOR,iconXValue}
        };
        Dictionary<FLOORTYPE, int> countVals = new Dictionary<FLOORTYPE, int>()
        {
            {FLOORTYPE.GROUND_FLOOR,0},
            {FLOORTYPE.FIRST_FLOOR,0},
            {FLOORTYPE.SECOND_FLOOR,0}
        };
        for (int i = 0; i < prefabs.Count; i++)
        {
            PrefabCollection collection = prefabs[i];
            Prefab prefab = collection.prefab;
            prefabCollections.Add(prefab, collection);
            PrefabIconMultipler icon = new PrefabIconMultipler(collection, countVals[prefab.floorType]++);

            icon.Click += Icon_Click;
            icon.SetParent(parentObject.transform);
            icon.SetPosition(new Vector2(xVals[prefab.floorType], yVals[prefab.floorType]), true);
            //icon.SetPosition(new Vector2(iconXValue, yVals[prefab.floorType]),true);
            icon.SetSize(iconSize, true);
            if (separatedFloorPrefabIcons.ContainsKey(prefab.floorType))
            {
                separatedFloorPrefabIcons[prefab.floorType].Add(icon);
            }
            else
            {
                separatedFloorPrefabIcons.Add(prefab.floorType, new List<PrefabIconMultipler>() { icon });
            }

            if (separateFloorPrefabIconDict.ContainsKey(prefab.floorType))
            {
                separateFloorPrefabIconDict[prefab.floorType].Add(prefab, icon);
            }
            else
            {
                Dictionary<Prefab, PrefabIconMultipler> floorIcons = new Dictionary<Prefab, PrefabIconMultipler>();
                floorIcons.Add(prefab, icon);
                separateFloorPrefabIconDict.Add(prefab.floorType, floorIcons);
            }
            yVals[prefab.floorType] += a;
            if (countVals[prefab.floorType] % maxColumnLength == 0)
            {
                yVals[prefab.floorType] = initialYvalue;//0.5f;
                xVals[prefab.floorType] += iconSize.x + 0.01f;
            }
            //yVals[prefab.floorType] -= a;
        }
        SetCurrentFloor(currentFloorType);
    }
    void Awake()
    {
        Clear();
        SetInstance(prefabCompart, this);
        if (!keys.Contains(prefabCompart))
        {
            keys.Add(prefabCompart);
        }
        InitialiseParentObject();
        //x = GetComponent<RectTransform>().anchoredPosition.x;
        Vector2 pos = RectTransformUtility.WorldToScreenPoint(GetComponentInParent<Canvas>().worldCamera, GetComponent<RectTransform>().anchoredPosition);
        //x = pos.x;
        Debug.LogFormat("X is {0}.", iconXValue);
    }
    public void Clear()
    {
        prefabCollections.Clear();
        separatedFloorPrefabIcons.Clear();
        separateFloorPrefabIconDict.Clear();
    }
    void Icon_Click(PrefabIconMultipler icon)
    {
        buildSystem bs;
        if (buildSystem.InstanceAvailable(out bs))
        {
            Prefab prev;
            if (bs.PrefabAvailable(out prev))
            {
                AddPrefab(prev);
            }
            Prefab prefab = icon.Prefab;
            bs.SetCurrentPrefab(prefab);

            PrefabView pv;
            if (PrefabView.InstanceAvailable(out pv))
            {
                pv.SetPrefab(prefab);
            }
            icon.Count--;
            if (icon.Count < 1)
            {
                RemoveIcon(icon.ID);
            }
        }
    }
    void InitialiseParentObject()
    {
        parentObject = new GameObject();
        parentObject.SetActive(false);
        parentObject.name = string.Format("pnlPrefabs_{0}", prefabCompart);
        parentObject.transform.position = transform.position;
        RectTransform pTransform = parentObject.AddComponent<RectTransform>();

        pTransform.SetParent(transform);
        pTransform.anchoredPosition = new Vector2();
        pTransform.anchorMin = new Vector2();
        pTransform.anchorMax = new Vector2(1, 1);
        pTransform.sizeDelta = new Vector2();
    }

    void PrefabSelectionButton_Click(UIButton sender)
    {
        if (parentObject.activeSelf)
        {
            SetParentActive(false);
        }
        else
        {
            foreach (PREFAB_COMPART key in Keys)
            {
                PrefabSelectionButton button;
                if (InstanceExists(key, out button))
                {
                    button.SetParentActive(key == prefabCompart);
                }
            }
        }
    }
    void RemoveIcon(int id)
    {
        PrefabIconMultipler picon = separatedFloorPrefabIcons[currentFloorType][id];
        float initialY = picon.Position.y;
        picon.Dispose();
        separateFloorPrefabIconDict[currentFloorType].Remove(picon.Prefab);
        separatedFloorPrefabIcons[currentFloorType].RemoveAt(id);
        float y = initialYvalue;//0.5f;
        float x = iconXValue;
        for (int i = 0; i < separatedFloorPrefabIcons[currentFloorType].Count; i++)
        {
            PrefabIconMultipler icon = separatedFloorPrefabIcons[currentFloorType][i];
            if (i >= id)
            {
                icon.ID--;
            }
            icon.SetPosition(new Vector2(x, y), true);
            icon.SetSize(iconSize, true);
            y += a;
            if ((i+1)%maxColumnLength == 0)
            {
                y = initialYvalue;
                x += iconSize.x + 0.01f;
            }
        }
    }
    public void SetCurrentFloor(FLOORTYPE floor)
    {
        currentFloorType = floor;
        foreach (FLOORTYPE key in separatedFloorPrefabIcons.Keys)
        {
            foreach (PrefabIconMultipler icon in separatedFloorPrefabIcons[key])
            {
                icon.SetActive(key == floor);
            }
        }
    }
    public static void SetCurrentFloorAllButtons(FLOORTYPE floor)
    {
        foreach(PREFAB_COMPART compart in Keys)
        {
            PrefabSelectionButton button;
            if (InstanceExists(compart,out button))
            {
                button.SetCurrentFloor(floor);
            }
        }
    }
    void SetParentActive(bool active)
    {
        parentObject.SetActive(active);
        string textureName = "{0}_{1}".Format(prefabCompart, active ? "active" : "inactive");
        Sprite spr;
        if (ResourceManager.GetItem(textureName, out spr))
        {
            Image.sprite = spr;
        }
        else
        {
            Debug.LogFormat("{0} not found.", textureName);
        }
    }
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        Click += PrefabSelectionButton_Click;
        string textureName = "{0}_inactive".Format(prefabCompart);
        Sprite spr;
        if (ResourceManager.GetItem(textureName, out spr))
        {
            Image.sprite = spr;
        }
    }
}