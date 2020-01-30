using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// An abstract class used as a template for all non-monobehaviour classes
/// with a singleton arrangement. "NullableInstance" refers to the fact
/// that this pattern doesn't force the "instance" variable to have a
/// definite value. Inspired by https://www.codeproject.com/Articles/572263/A-Reusable-Base-Class-for-the-Singleton-Pattern-in
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class NullableInstanceClassSingleton<T> where T : class
{
    /// <summary>
    /// The definitive instance of this class.
    /// </summary>
    private static T instance;

    /// <summary>
    /// A boolean method which returns true if an instance
    /// of this class has been assigned. It also outputs
    /// the value stored in "instance" to be used elsewhere.
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static bool InstanceAvailable(out T obj)
    {
        obj = instance;
        return instance != null;
    }

    /// <summary>
    /// Sets the given value as the definitive
    /// instance of this class.
    /// </summary>
    /// <param name="obj"></param>
    protected static void SetInstance(T obj)
    {
        instance = obj;
    }
}