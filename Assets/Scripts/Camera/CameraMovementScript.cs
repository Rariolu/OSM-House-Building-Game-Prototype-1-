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

    /// <summary>
    /// The camera positions in anti-clockwise order.
    /// Used to guarantee that the camera gets to the correct
    /// position after rotation.
    /// </summary>
    public CameraPosition[] cameraPositions;

    public float minSwipeDistance = 10f;
    Vector3 originalMousePosition;
    public float speed = 2;
    bool storedMousePos = false;

    /// <summary>
    /// A bool which determines whether or not the camera changes
    /// its y position when the floor is changed.
    /// </summary>
    public bool moveY = true;
    float yIntercept;
    float originalY;
    private void Awake()
    {
        SetInstance(this);
        
    }
    private void Start()
    {
        originalY = transform.position.y;
        Floor ground;
        if (Floor.InstanceExists(FLOORTYPE.GROUND_FLOOR,out ground))
        {
            yIntercept = originalY - ground.transform.position.y;
        }
        else
        {
            yIntercept = 8.8f;
        }
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
        int mult = (int)cameraDir;
        const int angle = 90;
        Transform floorTransform = Floor.FocusedFloor.transform;
        Vector3 floorUp = floorTransform.up;
        Vector3 floorPos = floorTransform.position;
        int newIndex = currentPosition + (int)cameraDir;
        newIndex = newIndex < 0 ? cameraPositions.Length + newIndex : newIndex;
        Debug.LogFormat("New camera index: {0}; Modded: {1};", newIndex, newIndex % cameraPositions.Length);
        CameraPosition newPosition = cameraPositions[newIndex % cameraPositions.Length];
        float current = 0;
        while (d < angle)
        {
            float a = angle * Time.deltaTime;
            d += a * speed;
            float c = a * mult * speed;
            current += c;
            transform.RotateAround(floorPos, floorUp, c);
            yield return 0;
        }
        currentPosition = newIndex;
        Vector3 newPos = newPosition.position;
        newPos.y = moveY ? floorPos.y + yIntercept : originalY;
        transform.position = newPos;
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

    /// <summary>
    /// Navigate the camera towards a given y position
    /// (used to alternate between the different floors of the building.
    /// </summary>
    /// <param name="y"></param>
    public void SetFloor(float y)
    {
        if (moveY)
        {
            transform.position = new Vector3(transform.position.x, y + yIntercept, transform.position.z);
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