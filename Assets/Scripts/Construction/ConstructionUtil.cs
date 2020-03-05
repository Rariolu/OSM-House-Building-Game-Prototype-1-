using UnityEngine;
using System.Collections;

/// <summary>
/// A class used to set and retrieve the current contract that's being used
/// and store information on the progression of the game.
/// </summary>
public class ConstructionUtil : NullableInstanceClassSingleton<ConstructionUtil>
{
    #region Properties
    readonly Contract contract;
    public Contract Contract
    {
        get
        {
            return contract;
        }
    }
    uint daysPassed = 0;
    public uint DaysPassed
    {
        get
        {
            return daysPassed;
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
    #endregion

    ConstructionUtil(Contract c)
    {
        contract = c;
    }
    #region IncrementFunctions
    public void IncrementDaysPassed()
    {
        daysPassed++;
    }
    public void IncrementDestruction()
    {
        destroyed++;
    }
	public void IncrementModifiedFixings()
	{
		fixingsChanged++;
	}
    #endregion
    public static void SetContract(Contract c)
    {
        SetInstance(new ConstructionUtil(c));
    }
}