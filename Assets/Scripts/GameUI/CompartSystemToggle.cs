using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompartSystemToggle : UIButton
{
    bool childrenActive = false;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        SetChildrenActive(false);
        Click += CompartSystemToggle_Click;
    }
    void CompartSystemToggle_Click(UIButton sender)
    {
        childrenActive = !childrenActive;
    }
    void SetChildrenActive(bool active)
    {
        foreach(Transform child in transform)
        {
            child.gameObject.SetActive(active);
        }
    }
}