#pragma warning disable IDE0017
#pragma warning disable IDE0018
#pragma warning disable IDE0039
#pragma warning disable IDE0044
#pragma warning disable IDE1005
#pragma warning disable IDE1006

using UnityEngine;
using System.Collections;


/// <summary>
/// A script which is attached to the SnapPoints which allows
/// them to be clicked (which places a prefab if one is selected).
/// </summary>
[RequireComponent(typeof(Collider))]
public class SnapPointTrigger : MonoBehaviour
{
    bool isSnapped = false;
    /// <summary>
    /// A boolean which represents whether or not a prefab is
    /// attached to the SnapPoint.
    /// </summary>
    public bool Snapped
    {
        get
        {
            return isSnapped;
        }
        set
        {
            isSnapped = value;
        }
    }
    /// <summary>
    /// The snap type (which determines which objects
    /// can be "snapped" to this) of this SnapPoint.
    /// </summary>
    public SNAP_POINT_TYPE snapType;
    void OnMouseDown()
    {
        if (!Snapped)
        {
            PrefabCounter counter;
            if (PrefabCounter.InstanceAvailable(out counter))
            {
                Prefab currentPrefab;
                if (counter.PrefabAvailable(out currentPrefab))
                {
                    if (currentPrefab.snapType == snapType)
                    {
                        PrefabPlacedObject ppo = new PrefabPlacedObject(currentPrefab, transform.position);
                        ppo.SnapPointTrigger = this;
                        ppo.SetSceneParent();
                        InGameSceneScript gameSceneScript;
                        if (InGameSceneScript.InstanceAvailable(out gameSceneScript))
                        {
                            gameSceneScript.AddPlacement(ppo);
                        }
                        isSnapped = true;
                        counter.DecrementCount(currentPrefab);
                    }
                }
            }
        }
    }
}