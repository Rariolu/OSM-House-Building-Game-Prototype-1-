using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// A class which allows a derived Monobehaviour class to become a multiton,
/// using a given key type to represent the instances (typically
/// an enum type would be used so that each enum value has a corresponding
/// instance of the class).
/// </summary>
/// <typeparam name="T">The type of stored instance.</typeparam>
/// <typeparam name="U">The type used as a key to store and retrieve instances.</typeparam>
public abstract class MultitonScript<T,U> : MonoBehaviour where T : MonoBehaviour
{
    static Dictionary<U, T> instances = new Dictionary<U, T>();

    public static T[] Values
    {
        get
        {
            return instances.Values.ToArray();
        }
    }

    /// <summary>
    /// Determine whether an instance corresponding to the given
    /// key exists, and output the result.
    /// </summary>
    /// <param name="key">A value used to represent the required instance.</param>
    /// <param name="obj">The outputted instance (or a default value).</param>
    /// <returns>True if the instance exists and has been returned</returns>
    public static bool InstanceExists(U key, out T obj)
    {
        if (instances.ContainsKey(key))
        {
            obj = instances[key];
            return true;
        }
        obj = default(T);
        return false;
    }
    /// <summary>
    /// Set the instance of the given key
    /// to be the given value. If an instance
    /// of that key already exists, it will be
    /// replaced in the "instances" dictionary.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="obj"></param>
    protected static void SetInstance(U key, T obj)
    {
        if (instances.ContainsKey(key))
        {
            instances[key] = obj;
        }
        else
        {
            instances.Add(key, obj);
        }
    }

    protected static void Clear()
    {
        instances.Clear();
    }

    protected static void RemoveInstance(U key)
    {
        if (instances.ContainsKey(key))
        {
            instances.Remove(key);
        }
    }
}