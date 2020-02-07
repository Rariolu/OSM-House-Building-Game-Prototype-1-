using UnityEngine;
using System.Collections;

/// <summary>
/// A class used to set and retrieve the current contract that's being used.
/// </summary>
public class ConstructionUtil : NullableInstanceClassSingleton<ConstructionUtil>
{
    readonly Contract contract;
    public Contract Contract
    {
        get
        {
            return contract;
        }
    }
    ConstructionUtil(Contract c)
    {
        contract = c;
    }
    public static void SetContract(Contract c)
    {
        SetInstance(new ConstructionUtil(c));
    }
}