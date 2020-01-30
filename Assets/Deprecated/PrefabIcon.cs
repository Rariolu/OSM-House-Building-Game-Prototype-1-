#pragma warning disable IDE0017
#pragma warning disable IDE0018
#pragma warning disable IDE0039
#pragma warning disable IDE0044
#pragma warning disable IDE1005

#if PREFABICON

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//public enum DRAGSTATE
//{
//    IDLE, DRAGGING, DRAGGED
//}

/// <summary>
/// An event which is called whenever the gameobject
/// of a "PrefabIcon" is clicked.
/// </summary>
/// <param name="sender">The "PrefabIcon" which was clicked.</param>
public delegate void PrefabIconClick(PrefabIcon sender);
public class PrefabIcon
{
    //DRAGSTATE dragState = DRAGSTATE.IDLE;
    //public DRAGSTATE DragState
    //{
    //    get
    //    {
    //        return dragState;
    //    }
    //}
    Rect bounds = new Rect(0, 0, 0.2f, 0.2f);
    public PrefabIconClick Click;
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
    Prefab prefab;
    public Prefab Prefab
    {
        get
        {
            return prefab;
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
    public PrefabIcon(Prefab prefab, int _id)
    {
        id = _id;
        this.prefab = prefab;
        gameObject = new GameObject();
        gameObject.name = prefab.type.ToString();
        Image image = gameObject.AddComponent<Image>();
        rectTransform = image.rectTransform;
        Sprite spr;
        if (ResourceManager.GetItem(prefab.type, out spr))
        {
            image.sprite = spr;
        }
        UnityEngine.Events.UnityAction<BaseEventData> drag = (data) =>
        {
            //dragState = DRAGSTATE.DRAGGING;
            if (Click != null)
            {
                Click(this);
            }
        };
        gameObject.SetEvent(drag);//, EventTriggerType.BeginDrag);
    }
    public void Dispose()
    {
        Object.Destroy(gameObject);
    }
    void ResetAnchor()
    {
        Vector2 ancmin = new Vector2(bounds.x, bounds.y);
        Vector2 ancmax = new Vector2(bounds.x + bounds.width, 1f - (bounds.y + bounds.height));
        rectTransform.anchoredPosition = new Vector2(bounds.width / 2f, bounds.height / 2f);
        rectTransform.anchorMin = ancmin;
        rectTransform.anchorMax = ancmax;
        rectTransform.sizeDelta = new Vector2();
    }
    public void SetParent(Transform parent)
    {
        rectTransform.SetParent(parent);
    }
    public void SetPosition(Vector2 pos)
    {
        bounds.x = pos.x;
        bounds.y = pos.y;
        ResetAnchor();
    }
    public void SetSize(Vector2 size)
    {
        bounds.width = size.x;
        bounds.height = size.y;
        ResetAnchor();
    }
}

#endif