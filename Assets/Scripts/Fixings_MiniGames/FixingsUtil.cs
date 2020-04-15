using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void FixingsChanged(int fixings);

/// <summary>
/// A class used to store and retrieve the Intersection that is currently
/// being used in the "Fixings" scene.
/// </summary>
public class FixingsUtil
{
    Intersection currentIntersection;
    public Intersection CurrentIntersection
    {
        get
        {
            return currentIntersection;
        }
    }

    int fixings;
    public int Fixings
    {
        get
        {
            return fixings;
        }
        set
        {
            fixings = value;
            if (FixingsChanged != null)
            {
                FixingsChanged(fixings);
            }
        }
    }

    public FixingsChanged FixingsChanged;

    private FixingsUtil(Intersection intersection)
    {
        currentIntersection = intersection;
    }
    public static void ApplyFixingsToIntersection(Intersection intersection)
    {
        SingletonUtil.SetInstance(new FixingsUtil(intersection));
    }
}