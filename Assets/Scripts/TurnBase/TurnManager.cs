using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TurnManager : MonoBehaviour
{
    public Image pbTransition;
    public Text lblTurn;
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
            }

            TransitionScript transition;
            if (SingletonUtil.InstanceAvailable(out transition))
            {
                transition.Transition();
            }
        }
        SetTurnText(currentTurns);
    }

    void SetTurnText(int turn)
    {
        if (lblTurn != null)
        {
            lblTurn.text = "Turn: {0}".FormatText(turn);
        }
    }
}