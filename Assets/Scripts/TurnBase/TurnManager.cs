using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TurnManager : MonoBehaviour
{
    public Image pbTransition;
    public uint turnsPerMonth = 10;
    int currentTurns;
    private void Awake()
    {
        SingletonUtil.SetInstance(this);
    }
    private void Start()
    {
        if (pbTransition != null)
        {
            pbTransition.gameObject.SetActive(true);

        }
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

            TransitionScript transition;
            if (SingletonUtil.InstanceAvailable(out transition))
            {
                transition.Transition();
            }
        }
    }
}