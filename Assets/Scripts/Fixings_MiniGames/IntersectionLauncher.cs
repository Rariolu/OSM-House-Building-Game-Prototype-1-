using UnityEngine;
using System.Collections;

public class IntersectionLauncher : MonoBehaviour
{
    //public SnapPointLauncher snapPoints;
    private void Awake()
    {
        Intersection intersection = new Intersection(gameObject);
    }
}