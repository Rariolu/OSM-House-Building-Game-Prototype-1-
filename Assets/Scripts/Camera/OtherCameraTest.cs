using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A script which is used to rotate its parent object (the camera) around the up axis
/// of the current floor.
/// </summary>
public class OtherCameraTest : NullableInstanceScriptSingleton<OtherCameraTest>
{
    bool canRotate = true;
    public float speed = 2;
    void Awake()
    {
        SetInstance(this);
    }

    public static Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Quaternion rotation)
    {
        return rotation * (point - pivot) + pivot;
    }

    /// <summary>
    /// Rotate around the current floor by a given angle (within a time limit).
    /// </summary>
    /// <param name="angle"></param>
    /// <param name="time"></param>
    /// <returns></returns>
    IEnumerator Rotate(int angle, float time)
    {
        canRotate = false;
        float d = 0;
        int mult = angle < 0 ? -1 : 1;
        angle = Mathf.Abs(angle);
        Transform floorTransform = Floor.FocusedFloor.transform;
        Vector3 floorUp = floorTransform.up;
        Vector3 floorPos = floorTransform.position;
        float current = 0;
        Vector3 fullRotation = transform.rotation*RotatePointAroundPivot(transform.position, floorUp, Quaternion.Euler(0, angle,0));
        while (d < angle)
        {
            float a = angle * Time.deltaTime;
            d += a * speed;
            float c = a * mult * speed;
            current += c;
            transform.RotateAround(floorPos, floorUp, c);
            yield return 0;
        }
        angle = angle * mult;
        float rest = angle > 0 ? angle - current : current - (float)angle;
        Debug.LogFormat("Rest: {0}; Current: {1}; Angle: {2};", rest.ToString(),current.ToString(),angle.ToString());
        transform.RotateAround(floorPos, floorUp, rest);
        //transform.position = fullRotation;
        //transform.LookAt(floorUp);
        canRotate = true;
    }
    /// <summary>
    /// Rotate around the current floor by a given angle.
    /// </summary>
    /// <param name="angle"></param>
    public void RotateCamera(int angle)
    {
        if (canRotate)
        {
            StartCoroutine(Rotate(angle, speed));
        }
    }
    /// <summary>
    /// Navigate the camera towards a given y position
    /// (used to alternate between the different floors of the building.
    /// </summary>
    /// <param name="y"></param>
    public void SetFloor(float y)
    {
        transform.position = new Vector3(transform.position.x, 8.8f + y, transform.position.z);
    }

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

    Vector3 originalMousePosition;
    bool storedMousePos = false;
    public float minSwipeDistance = 10f;

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
                int angle = dir.x > 0 ? 90 : -90;
                RotateCamera(angle);

            }
            storedMousePos = false;

        }
    }
}
