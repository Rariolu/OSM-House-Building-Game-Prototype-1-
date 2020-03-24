using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class SingletonUtil
{
    static Dictionary<Type, object> instances = new Dictionary<Type, object>();

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