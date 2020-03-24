#pragma warning disable IDE0017
#pragma warning disable IDE0018
#pragma warning disable IDE0039
#pragma warning disable IDE0044
#pragma warning disable IDE1005

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//[RequireComponent(typeof(Image))]
[RequireComponent(typeof(RawImage))]
public class PrefabView : MonoBehaviour
{
    //Image image;
    //Image Image
    //{
    //    get
    //    {
    //        return image ?? (image = GetComponent<Image>());
    //    }
    //}

    RawImage rawImage;
    RawImage RawImage
    {
        get
        {
            return rawImage ?? (rawImage = GetComponent<RawImage>());
        }
    }

    private void Awake()
    {
        SingletonUtil.SetInstance(this);
        DeActivate();
    }

    /// <summary>
    /// Set the game object to be inactive.
    /// </summary>
    public void DeActivate()
    {
        gameObject.SetActive(false);
    }

    /// <summary>
    /// Set the prefab to be displayed and
    /// set the game object to be active.
    /// </summary>
    /// <param name="prefab"></param>
    public void SetPrefab(Prefab prefab)
    {
        gameObject.SetActive(true);
        //Sprite spr;
        //if (ResourceManager.GetItem(prefab.type, out spr))
        //{
        //    Image.sprite = spr;
        //}
        Texture2D texture;
        if (ResourceManager.GetItem(prefab.type,out texture))
        {
            RawImage.texture = texture;
        }
    }
}