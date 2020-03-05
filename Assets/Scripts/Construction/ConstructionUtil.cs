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
    int destroyed = 0;
    public int Destroyed
    {
        get
        {
            return destroyed;
        }
    }
	int fixingsChanged = 0;
	public int FixingsChanged
	{
		get 
		{
			return fixingsChanged;
		}
	}
    ConstructionUtil(Contract c)
    {
        contract = c;
    }
    public void IncrementDestruction()
    {
        destroyed++;
    }
	public void IncrementModifiedFixings()
	{
		fixingsChanged++;
	}
    public static void SetContract(Contract c)
    {
        SetInstance(new ConstructionUtil(c));
    }
}