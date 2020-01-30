#pragma warning disable IDE0017
#pragma warning disable IDE0018
#pragma warning disable IDE0039
#pragma warning disable IDE0044
#pragma warning disable IDE1005

#if PREFABOBJECT

using UnityEngine;
using System.Collections;

public class PrefabObject
{
    GameObject gameObject;
    public GameObject GameObject
    {
        get
        {
            return gameObject;
        }
    }
    public PrefabObject(Prefab prefab)
    {
        gameObject = new GameObject();
        SceneObjectScript gameScene;
        if (SceneObjectScript.InstanceExists(SCENE.InGame, out gameScene))
        {
            gameObject.transform.SetParent(gameScene.transform);
        }
        gameObject.name = prefab.type.ToString();
        prefab.offset.ApplyOffset(gameObject.transform);
        MeshFilter filter = gameObject.AddComponent<MeshFilter>();
        MeshRenderer renderer = gameObject.AddComponent<MeshRenderer>();
        Demo preview = gameObject.AddComponent<Demo>();
        preview.prefab = gameObject;
        preview.prefabProperties = prefab;
        ResourceManager.GetItem("NonValid", out preview.nonValidMat);
        ResourceManager.GetItem("Valid", out preview.validMat);
        preview.snapTag = TAG.TESTTAG.ToString();
        Mesh mesh;
        if (ResourceManager.GetItem(prefab.type,out mesh))
        {
            filter.mesh = mesh;
        }
        Material mat;
        if (ResourceManager.GetItem("default", out mat))
        {
            renderer.material = mat;
        }
        Collision();
    }
    void Collision()
    {
        BoxCollider boxCollider = gameObject.AddComponent<BoxCollider>();
        boxCollider.isTrigger = true;
        Rigidbody rigidBody = gameObject.AddComponent<Rigidbody>();
        rigidBody.useGravity = false;
    }
}

#endif