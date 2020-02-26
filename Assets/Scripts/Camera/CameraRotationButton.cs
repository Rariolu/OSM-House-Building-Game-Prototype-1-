using UnityEngine;
using System.Collections;

/// <summary>
/// A UIButton which rotates the camera around the current floor when clicked.
/// </summary>
public class CameraRotationButton : UIButton
{
    /// <summary>
    /// The angle of rotation around the floor's axis.
    /// </summary>
    public int rotationAngle = 90;
    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        Click += CameraRotationButton_Click;
    }
    void CameraRotationButton_Click(UIButton sender)
    {
        OtherCameraTest mainCamera;
        if (OtherCameraTest.InstanceAvailable(out mainCamera))
        {
            mainCamera.RotateCamera(rotationAngle);
        }
    }
}