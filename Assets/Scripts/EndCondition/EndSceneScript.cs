using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndSceneScript : MonoBehaviour
{
    public Text lblExitState;
    public Text lblWeeks;
    public string timeFormat = "You took {0} weeks.";
    void Start()
    {
        EndGameUtil endUtil;
        if (SingletonUtil.InstanceAvailable(out endUtil) &&
            lblExitState != null)
        {
            lblExitState.text = endUtil.ExitState.ToString();
        }

        ConstructionUtil constructionUtil;
        if (SingletonUtil.InstanceAvailable(out constructionUtil))
        {
            if (lblWeeks != null)
            {
                lblWeeks.text = timeFormat.FormatText(constructionUtil.WeeksPassed);
            }
        }
    }
}