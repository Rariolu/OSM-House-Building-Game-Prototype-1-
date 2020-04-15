using UnityEngine;
using System.Collections;

public class IntersectionLauncher : MonoBehaviour
{
    public SnapPointLauncher[] snapPoints;
    Intersection intersection;
    private void Awake()
    {
        intersection = new Intersection(gameObject);
        intersection.SetActive(false);
    }
    private void Start()
    {
        foreach(SnapPointLauncher spl in snapPoints)
        {
            intersection.AddSnapPoint(spl.Trigger);
        }
    }
}