using UnityEngine;
using System.Collections;

public class SnapPointLauncher : MonoBehaviour
{
    public SNAP_POINT_TYPE snapType;
    SnapPointTrigger trigger;
    public SnapPointTrigger Trigger
    {
        get
        {
            return trigger ?? (trigger = GetComponent<SnapPointTrigger>());
        }
    }
    private void Awake()
    {
        SnapPoint snapPoint = new SnapPoint(snapType, gameObject);
    }
}
