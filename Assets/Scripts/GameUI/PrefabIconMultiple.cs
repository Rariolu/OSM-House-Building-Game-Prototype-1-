#pragma warning disable IDE0017
#pragma warning disable IDE0018
#pragma warning disable IDE0039
#pragma warning disable IDE0044
#pragma warning disable IDE1005

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// An event which is called when the prefab icon is clicked
/// </summary>
/// <param name="sender">The prefab icon that fired the event (i.e. the one that was clicked).</param>
public delegate void PrefabIconMultiplierClick(PrefabIconMultipler sender);
/// <summary>
/// A class which spawns a prefab icon on the canvas which shows the quantity
/// of a particular prefab.
/// </summary>
public class PrefabIconMultipler
{
    Rect bounds = new Rect(0, 0, 0.2f, 0.2f);
    public Vector2 Position
    {
        get
        {
            return new Vector2(bounds.x, bounds.y);
        }
    }
    public PrefabIconMultiplierClick Click;
    int count;
    public int Count
    {
        get
        {
            return count;
        }
        set
        {
            count = value;
            label.text = count.ToString();
        }
    }
    GameObject gameObject;
    int id;
    public int ID
    {
        get
        {
            return id;
        }
        set
        {
            id = value;
        }
    }
    Text label;
    PrefabCollection pCollection;
    public Prefab Prefab
    {
        get
        {
            return pCollection.prefab;
        }
    }
    
    RectTransform rectTransform;
    public RectTransform RectTransform
    {
        get
        {
            return rectTransform;
        }
    }
    
    public PrefabIconMultipler(PrefabCollection prefabCollection, int _id)
    {
        id = _id;
        pCollection = prefabCollection;
        gameObject = new GameObject();
        gameObject.name = Prefab.type.ToString();

        Image image = gameObject.AddComponent<Image>();
        rectTransform = image.rectTransform;
        Sprite spr;
        if (ResourceManager.GetItem(Prefab.type, out spr))
        {
            image.sprite = spr;
        }
        SetText(pCollection.quantity);

        UnityAction<BaseEventData> click = (data) =>
        {
            if (Click != null)
            {
                Click(this);
            }
        };
        gameObject.SetEvent(click);
    }

    /// <summary>
    /// Destroy the game object associated with this prefab icon.
    /// </summary>
    public void Dispose()
    {
        Object.Destroy(gameObject);
    }
    void ResetAnchor(bool newShit = false)
    {
        Vector2 ancmin;
        Vector2 ancmax;
        if (!newShit)
        {
            ancmin = new Vector2(bounds.x, bounds.y);
            ancmax = new Vector2(bounds.x + bounds.width, 1f - (bounds.y + bounds.height));
        }
        else
        {
            ancmin = new Vector2(bounds.x, bounds.y);
            ancmax = new Vector2(bounds.x + bounds.width, (bounds.y + bounds.height));
        }
        rectTransform.anchoredPosition = new Vector2(bounds.width / 2f, bounds.height / 2f);
        rectTransform.anchorMin = ancmin;
        rectTransform.anchorMax = ancmax;
        rectTransform.sizeDelta = new Vector2();
    }

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }

    public void SetParent(Transform parent)
    {
        rectTransform.SetParent(parent);
    }

    public void SetPosition(Vector2 pos, bool newShit = false)
    {
        bounds.x = pos.x;
        bounds.y = pos.y;
        ResetAnchor(newShit);
    }

    public void SetSize(Vector2 size, bool newShit = false)
    {
        bounds.width = size.x;
        bounds.height = size.y;
        ResetAnchor(newShit);
    }

    /// <summary>
    /// Set the text of the prefab icon to reflect a given quantity
    /// of the relevant prefab.
    /// </summary>
    /// <param name="c">The quantity of the prefab</param>
    void SetText(int c)
    {
        count = c;
        GameObject labelObject = new GameObject();
        labelObject.transform.SetParent(gameObject.transform);
        label = labelObject.AddComponent<Text>();
        //label.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
        Font f = Font.CreateDynamicFontFromOSFont("Arial", 15);
        label.alignment = TextAnchor.LowerRight;
        label.color = Color.black;
        label.font = f;
        label.text = c.ToString();

        RectTransform labelTransform = label.rectTransform;
        labelTransform.anchoredPosition = new Vector2();
        labelTransform.anchorMin = new Vector2(0f, 0f);
        labelTransform.anchorMax = new Vector2(1f, 0.5f);
        labelTransform.sizeDelta = new Vector2();
    }
}