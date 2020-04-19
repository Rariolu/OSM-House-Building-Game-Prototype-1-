using UnityEngine;
using System.Collections;

public class ActiveToggleButton : UIButton
{
    public GameObject[] gameObjects;
    public bool initiallyActive = false;
    bool objectsActive;
    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        Click += ActiveToggleButton_Click;
        objectsActive = initiallyActive;
        SetActive(objectsActive);
    }

    void ActiveToggleButton_Click(UIButton sender)
    {
        objectsActive = !objectsActive;
        SetActive(objectsActive);
    }

    void SetActive(bool active)
    {
        foreach(GameObject obj in gameObjects)
        {
            obj.SetActive(active);
        }
    }
}
