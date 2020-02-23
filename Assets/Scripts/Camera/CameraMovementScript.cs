using UnityEngine;
using System.Collections;

/// <summary>
/// A script which is used to rotate its parent object (the camera) around the up axis
/// of the current floor.
/// </summary>
public class CameraMovementScript : NullableInstanceScriptSingleton<CameraMovementScript>
{
    bool canRotate = true;
    int currentPosition = 0;
    public CameraPosition[] cameraPositions;
    public float minSwipeDistance = 10f;
    Vector3 originalMousePosition;
    public float speed = 2;
    bool storedMousePos = false;
    private void Awake()
    {
        SetInstance(this);
    }
    void MouseDown()
    {
        originalMousePosition = Input.mousePosition;
        storedMousePos = true;
    }

    void MouseUp()
    {
        Vector3 newPos = Input.mousePosition;
        if (storedMousePos)
        {
            if (Vector3.Distance(originalMousePosition, newPos) > minSwipeDistance)
            {
                Vector3 dir = Vector3.Normalize(newPos - originalMousePosition);
                CAMERA_DIR cDir = dir.x > 0 ? CAMERA_DIR.RIGHT : CAMERA_DIR.LEFT;
                RotateCamera(cDir);
            }
            storedMousePos = false;
        }
    }
    IEnumerator Rotate(CAMERA_DIR cameraDir, float speed)
    {
        canRotate = false;
        float d = 0;
        float time = 1f / speed;
        CameraPosition cPosition = cameraPositions[currentPosition % cameraPositions.Length];
        int newIndex = currentPosition + (int)cameraDir;
        newIndex = newIndex < 0 ? cameraPositions.Length + newIndex : newIndex;
        Debug.LogFormat("New camera index: {0}; Modded: {1};", newIndex,newIndex % cameraPositions.Length);
        CameraPosition newPosition = cameraPositions[newIndex % cameraPositions.Length];
        while (d < time)
        {
            d += Time.deltaTime;
            transform.position = Vector3.Lerp(cPosition.position, newPosition.position, d / time);
            Vector3 vec3Rotation = Vector3.Lerp(cPosition.rotation, newPosition.rotation, d / time);
            transform.rotation = Quaternion.Euler(vec3Rotation);
            yield return 0;
        }
        currentPosition = newIndex;
        transform.position = newPosition.position;
        transform.rotation = Quaternion.Euler(newPosition.rotation);
        canRotate = true;
    }
    public void RotateCamera(CAMERA_DIR cameraDir)
    {
        if (canRotate)
        {
            StartCoroutine(Rotate(cameraDir, speed));
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MouseDown();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            MouseUp();
        }
    }
}