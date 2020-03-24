using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Image))]
//[RequireComponent(typeof(RawImage))]
public class PrefabCompartSelectionButton : MultitonUIButton<PrefabCompartSelectionButton,PREFAB_COMPART>
{
    public PREFAB_COMPART compart;

    static PREFAB_COMPART currentCompart = PREFAB_COMPART.NONE;
    public static PREFAB_COMPART CurrentCompart
    {
        get
        {
            return currentCompart;
        }
        private set
        {
            currentCompart = value;
        }
    }

    bool currentlyActive = false;

    private void Awake()
    {
        SetInstance(compart, this);
    }

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        Click += PrefabCompartSelecitonButton_Click;
        SetActive(false);
    }

    void PrefabCompartSelecitonButton_Click(UIButton sender)
    {
        currentlyActive = !currentlyActive;
        SetActive(currentlyActive);
        if (currentlyActive)
        {
            foreach(PREFAB_COMPART pCompart in Util.GetEnumValues<PREFAB_COMPART>())
            {
                if (pCompart != compart)
                {
                    PrefabCompartSelectionButton button;
                    if (InstanceExists(pCompart,out button))
                    {
                        button.SetActive(false);
                    }
                }
            }
        }

        CompartPrefabSystem compartSystem;
        
        if (CompartPrefabSystem.InstanceAvailable(out compartSystem))
        {
            if (currentlyActive)
            {
                compartSystem.SwitchCompart(compart);
            }
            else
            {
                PrefabIconGrid grid;
                if (PrefabIconGrid.InstanceExists(compart, out grid))
                {
                    grid.SetActive(false);
                }
            }
        }
    }
    
    public void SetActive(bool active)
    {
        string textureName = "{0}_{1}".FormatText(compart, active ? "active" : "inactive");
        //Texture2D texture;
        //if (ResourceManager.GetItem(textureName,out texture))
        //{
        //    RawImage.texture = texture;
        //}
        //Sprite spr;
        //if (ResourceManager.GetItem(textureName, out spr))
        //{
        //    Image.sprite = spr;
        //}
        //else
        //{
        //    Debug.LogFormat("{0} not found.", textureName);
        //}
        Color colour = Image.color;
        Image.color = new Color(colour.r, colour.g, colour.b, active ? 1f : 0.5f);
    }
}