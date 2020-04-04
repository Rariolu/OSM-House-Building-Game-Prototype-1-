using UnityEngine;
using System.Collections;

public class SnapPointLauncher : MonoBehaviour
{
    public SNAP_POINT_TYPE snapType;
    private void Awake()
    {
        SnapPoint snapPoint = new SnapPoint(snapType, gameObject);
    }
}
