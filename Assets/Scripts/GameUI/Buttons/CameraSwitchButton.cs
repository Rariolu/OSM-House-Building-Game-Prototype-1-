using UnityEngine;
using System.Collections;

public class CameraSwitchButton : UIButton
{
    public GameObject mainCamera;
    public GameObject topCamera;
    bool mainCameraActive = true;
    protected override void Start()
    {
        base.Start();
        Click += CameraSwitchButton_Click;
        SetMainCameraActive(true);
    }

    void CameraSwitchButton_Click(UIButton sender)
    {
        mainCameraActive = !mainCameraActive;
        SetMainCameraActive(mainCameraActive);
    }

    void SetMainCameraActive(bool active)
    {
        mainCamera.SetActive(active);
        topCamera.SetActive(!active);
    }
}