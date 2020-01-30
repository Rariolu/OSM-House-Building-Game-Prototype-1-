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
using System.Collections.Generic;

public delegate void ContractClick(ContractSelection sender);
public class ContractSelection
{
    public ContractClick Click;
    Contract contract;
    public Contract Contract
    {
        get
        {
            return contract;
        }
    }
    GameObject gameObject;
    RectTransform rectTransform;
    Color backColor;
    public ContractSelection(Contract contract)
    {
        this.contract = contract;
        gameObject = new GameObject();
        gameObject.name = contract.name;
        Image image = gameObject.AddComponent<Image>();
        image.color = backColor = new Color(0f, 0.5864725f, 1f, 0.5f);
        //Text label = gameObject.AddComponent<Text>();
        //label.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
        //label.fontSize = 20;
        //label.text = contract.name;
        rectTransform = image.rectTransform;//gameObject.AddComponent<RectTransform>();//label.rectTransform;
        AddLabel(contract.name);
        AddStartButton();
        //gameObject.SetEvent((data) => { if (Click != null) Click(this); });
        //SetClick(gameObject, (data) => { if (Click != null) Click(this); });
        AddBrochureButton();
    }
    void AddLabel(string text)
    {
        GameObject labelObject = new GameObject();
        Text label = labelObject.AddComponent<Text>();
        label.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
        label.fontSize = 40;
        label.color = Color.yellow;//new Color(1f - backColor.r, 1f - backColor.g, 1f - backColor.b, 0.5f);
        label.text = text;
        RectTransform labelTransform = labelObject.GetComponent<RectTransform>();
        labelTransform.SetParent(rectTransform);
        labelTransform.anchorMin = new Vector2(0f, 0.5f);
        labelTransform.anchorMax = new Vector2(1f, 0.8f);
        labelTransform.anchoredPosition = new Vector2();
        labelTransform.sizeDelta = new Vector2(1, 1);
    }
    void AddStartButton()
    {
        GameObject btnStart = new GameObject();
        Image img = btnStart.AddComponent<Image>();
        Sprite sprite;
        if (ResourceManager.GetItem("start",out sprite))
        {
            img.sprite = sprite;
        }
        btnStart.transform.SetParent(rectTransform);
        RectTransform startTransform = img.rectTransform;
        startTransform.anchorMin = new Vector2(0.05f, 0.25f);
        startTransform.anchorMax = new Vector2(0.95f, 0.45f);
        startTransform.anchoredPosition = new Vector2();
        startTransform.offsetMin = new Vector2(0, 0);
        startTransform.offsetMax = new Vector2(0, 0);
        UnityAction<BaseEventData> btnStartClick = (data) =>
        {
            if (Click != null)
            {
                Click(this);
            }
        };
        btnStart.SetEvent(btnStartClick);
    }
    void AddBrochureButton()
    {
        GameObject brochureButton = new GameObject();
        Image img = brochureButton.AddComponent<Image>();
        Sprite sprite;
        if (ResourceManager.GetItem("Brochure",out sprite))
        {
            img.sprite = sprite;
        }
        brochureButton.transform.SetParent(gameObject.transform);
        RectTransform bbTransform = img.rectTransform;
        bbTransform.anchorMin = new Vector2(0.05f, 0f);
        bbTransform.anchorMax = new Vector2(0.95f, 0.2f);
        bbTransform.anchoredPosition = new Vector2();
        bbTransform.offsetMin = new Vector2(0, 0);
        bbTransform.offsetMax = new Vector2(0, 0);
        UnityAction<BaseEventData> bbClick = (data) =>
        {
            BrochureUtil.SetContract(contract, SCENE.ContractSelection);
        };
        brochureButton.SetEvent(bbClick);
    }

    public void SetParent(Transform transform)
    {
        rectTransform.SetParent(transform);
        SetPosition(new Vector2());
        rectTransform.offsetMin = new Vector2(0, 0);
        rectTransform.offsetMax = new Vector2(0, 0);
    }
    public void SetPosition(Vector2 vec2)
    {
        float y = 1f - (vec2.y + 0.3f);
        rectTransform.anchorMin = new Vector2(vec2.x, 0f);//(1f - (vec2.y+0.3f)));
        rectTransform.anchorMax = new Vector2(vec2.x + 0.3f, 1f);// y + 0.3f);
        rectTransform.anchoredPosition = new Vector2();
    }
}