using UnityEngine;
using System.Collections;

public delegate void DestroyedPrefabChange(int prefabs);
public delegate void FixingsReset();
public delegate void TimePassed(uint t);

/// <summary>
/// A class used to set and retrieve the current contract that's being used
/// and store information on the progression of the game.
/// </summary>
public class ConstructionUtil
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
    uint weeksPassed = 0;
    public uint WeeksPassed
    {
        get
        {
            return weeksPassed;
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

    public DestroyedPrefabChange DestroyedPrefabChange;

	int fixingsChanged = 0;
	public int FixingsChanged
	{
		get 
		{
			return fixingsChanged;
		}
	}

    bool fixingsRedone = false;
    public bool FixingsRedone
    {
        get
        {
            return fixingsRedone;
        }
    }

    public FixingsReset FixingsReset;
    public TimePassed TimePassed;

    #endregion

    ConstructionUtil(Contract c)
    {
        contract = c;
    }
    #region IncrementFunctions
    public void IncrementDaysPassed()
    {
        weeksPassed++;
        if (TimePassed != null)
        {
            TimePassed(weeksPassed);
        }
    }
    public void IncrementDestruction()
    {
        destroyed++;
        if (DestroyedPrefabChange != null)
        {
            DestroyedPrefabChange(destroyed);
        }
    }
	public void IncrementModifiedFixings()
	{
		fixingsChanged++;
	}

    public void NewSetOfFixings()
    {
        fixingsRedone = true;
        if (FixingsReset != null)
        {
            FixingsReset();
        }
    }

    #endregion
    public static void SetContract(Contract c)
    {
        SingletonUtil.SetInstance(new ConstructionUtil(c));
    }
}