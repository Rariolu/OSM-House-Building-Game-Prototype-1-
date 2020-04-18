using UnityEngine;
using System.Collections;

public class TurnManager : MonoBehaviour
{
    public uint turnsPerMonth = 10;
    int currentTurns;
    private void Awake()
    {
        SingletonUtil.SetInstance(this);
    }
    public void IncrementTurns()
    {
        currentTurns++;
        if (currentTurns >= turnsPerMonth)
        {
            currentTurns = 0;
            ConstructionUtil util;
            if (SingletonUtil.InstanceAvailable(out util))
            {
                util.IncrementDaysPassed();
                Logger.Log("Months: {0};", util.DaysPassed);
            }
        }
        
    }
}