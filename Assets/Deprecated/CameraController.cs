using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if CAMERACONTROLLER

/// <summary>
/// A script used to control the camera's movement
/// in the main game scene.
/// </summary>
public class CameraController : NullableInstanceScriptSingleton<CameraController>//MonoBehaviour
{
	public Transform cameraTransform;
	public float movementSpeed;
	public float movementTime;
	public float rotationAmount;
	public Vector3 zoomAmount;
	public Vector3 newPos;
	public Quaternion newRot;
	public Vector3 zoom;
    public float mouseMult = 10f;


    Rect rect;
    bool mousedown = false;
    Vector3 ogMouse;

    private void Awake()
    {
        SetInstance(this);
    }

    void MovementInputHandler()
    {
        if (Input.GetMouseButtonDown(0) && rect.Contains(Input.mousePosition))
        {
            mousedown = true;
            ogMouse = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(0) || !rect.Contains(Input.mousePosition))
        {
            mousedown = false;
        }

        if (mousedown)
        {
            Vector3 dir = Vector3.Normalize(Input.mousePosition - ogMouse);
            dir = -new Vector3(dir.x, 0, dir.y);
            newPos += dir * movementSpeed * mouseMult * Time.deltaTime;
            ogMouse = Input.mousePosition;
        }

        if (Input.GetKey(KeyCode.W))
        {

            newPos += (transform.forward * movementSpeed);

        }
        if (Input.GetKey(KeyCode.S))
        {

            newPos += (transform.forward * -movementSpeed);

        }
        if (Input.GetKey(KeyCode.D))
        {

            newPos += (transform.right * movementSpeed);

        }
        if (Input.GetKey(KeyCode.A))
        {

            newPos += (transform.right * -movementSpeed);

        }

        if (Input.GetKey(KeyCode.Q))
        {

            newRot *= Quaternion.Euler(Vector3.up * rotationAmount);

        }
        if (Input.GetKey(KeyCode.E))
        {

            newRot *= Quaternion.Euler(Vector3.up * -rotationAmount);

        }

        if (Input.GetKey(KeyCode.R))
        {

            zoom += zoomAmount;

        }
        if (Input.GetKey(KeyCode.F))
        {

            zoom -= zoomAmount;

        }

        transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime * movementTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRot, Time.deltaTime * movementTime);
        cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, zoom, Time.deltaTime * movementTime);
    }

    /// <summary>
    /// Navigate the camera towards a given y position
    /// (used to alternate between the different floors of the building.
    /// </summary>
    /// <param name="y"></param>
    public void SetFloor(float y)
    {
        newPos = new Vector3(newPos.x, 8.8f + y, newPos.z);
    }

    // Start is called before the first frame update
    void Start()
    {
        rect = Util.GetScreenRect(Camera.main);
		newPos = transform.position;
		newRot = transform.rotation;
		zoom = cameraTransform.localPosition;

    }

    // Update is called once per frame
    void Update()
    {
        MovementInputHandler();
    }
}

#endif