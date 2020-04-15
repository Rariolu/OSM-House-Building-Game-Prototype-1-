#pragma warning disable IDE0017
#pragma warning disable IDE0018
#pragma warning disable IDE0039
#pragma warning disable IDE0044
#pragma warning disable IDE1005
#pragma warning disable IDE1006

using UnityEngine;
using System.Collections;

public delegate void SnapPointDeleted();
public delegate void SnapPointTriggered();
public delegate void SnapPointUnTriggered();

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
            Logger.Log("Blep");
            if (isSnapped)
            {
                if (SnapPointTriggered != null)
                {
                    SnapPointTriggered();
                }
                else
                {
                    Logger.Log("triggered delegate null");
                }
            }
            else
            {
                if (SnapPointUnTriggered != null)
                {
                    SnapPointUnTriggered();
                }
                else
                {
                    Logger.Log("untriggered delegate null");
                }
            }
        }
    }

    public SnapPointDeleted SnapPointDeleted;
    public SnapPointTriggered SnapPointTriggered;
    public SnapPointUnTriggered SnapPointUnTriggered;

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
            if (SingletonUtil.InstanceAvailable(out counter))
            {
                Prefab currentPrefab;
                if (counter.PrefabAvailable(out currentPrefab))
                {
                    if (currentPrefab.snapType == snapType)
                    {
                        ConstructionUtil constructionUtil;
                        if (SingletonUtil.InstanceAvailable(out constructionUtil))
                        {
                            PrefabPlacedObject ppo = new PrefabPlacedObject(currentPrefab, transform.position, constructionUtil.Contract.finishedConstruction);
                            ppo.SnapPointTrigger = this;
                            ppo.SetSceneParent();

                            InGameSceneScript gameSceneScript;
                            if (SingletonUtil.InstanceAvailable(out gameSceneScript))
                            {
                                gameSceneScript.AddPlacement(ppo);
                            }
                            Snapped = true;
                            counter.DecrementCount(currentPrefab);
                        }
                    }
                }
            }
        }
    }
    private void OnDestroy()
    {
        if (SnapPointDeleted != null)
        {
            SnapPointDeleted();
        }
    }
}