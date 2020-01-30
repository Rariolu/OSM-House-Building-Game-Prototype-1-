using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if CAMERATEST

public class CameraTest : MonoBehaviour
{
    bool canRotate = true;
    public float speed = 1;

    IEnumerator Rotate(float angle, float time)
    {
        canRotate = false;
        float d = 0;
        Quaternion start = transform.rotation;
        
        Quaternion target = Quaternion.AngleAxis(angle, transform.up) * start;
        while (d < time)
        {
            float dt = Time.deltaTime;
            float a = angle * (dt / time);
            d += dt;
            //transform.Rotate(transform.InverseTransformPoint(Vector3.up), a);
            transform.rotation = Quaternion.Lerp(start, target, d / time);
            yield return 0;
        }
        canRotate = true;
    }
    void Start()
    {
        
    }

    void Update()
    {
        if (canRotate)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                StartCoroutine(Rotate(90, speed));
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                StartCoroutine(Rotate(-90, speed));
            }
        }
    }
}

#endif