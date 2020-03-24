using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class SingletonUtil
{
    /// <summary>
    /// A dictionary that stores the only possible instance of each type.
    /// </summary>
    static Dictionary<Type, object> instances = new Dictionary<Type, object>();

    /// <summary>
    /// Add (or replace) the given type and value to the
    /// dictionary of stored instances so that the instance
    /// can be retrieved.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="inst"></param>
    public static void SetInstance<T>(T inst)
    {
        Type type = typeof(T);
        if (instances.ContainsKey(type))
        {
            instances[type] = inst;
        }
        else
        {
            instances.Add(type, inst);
        }
    }

    /// <summary>
    /// Check if the dictionary stores a value of the 
    /// requested type, and output the value if found.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="inst"></param>
    /// <returns></returns>
    public static bool InstanceAvailable<T>(out T inst)
    {
        Type type = typeof(T);
        if (instances.ContainsKey(type))
        {
            inst = (T)instances[type];
            return true;
        }
        inst = default(T);
        return false;
    }
}