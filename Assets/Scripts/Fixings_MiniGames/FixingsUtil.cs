using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// A class used to store and retrieve the Intersection that is currently
/// being used in the "Fixings" scene.
/// </summary>
public class FixingsUtil //: NullableInstanceClassSingleton<FixingsUtil>
{
    Intersection currentIntersection;
    public Intersection CurrentIntersection
    {
        get
        {
            return currentIntersection;
        }
    }
    private FixingsUtil(Intersection intersection)
    {
        currentIntersection = intersection;
    }
    public static void ApplyFixingsToIntersection(Intersection intersection)
    {
        SingletonUtil.SetInstance(new FixingsUtil(intersection));
    }
}