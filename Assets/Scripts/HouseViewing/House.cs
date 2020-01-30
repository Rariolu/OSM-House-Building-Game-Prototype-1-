using UnityEngine;
using System.Collections;

public struct House
{
    public string name;
    public Mesh mesh;
    public House(string n, Mesh m)
    {
        name = n;
        mesh = m;
    }
    public void Instantiate()
    {
        GameObject gameObject = new GameObject();
        gameObject.transform.position = new Vector3();
        MeshFilter filter = gameObject.AddComponent<MeshFilter>();
        MeshRenderer renderer = gameObject.AddComponent<MeshRenderer>();
        filter.mesh = mesh;
        Material mat;
        if (ResourceManager.GetItem("default",out mat))
        {
            renderer.material = mat;
        }
    }
}