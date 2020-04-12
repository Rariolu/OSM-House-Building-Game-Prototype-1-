using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SubtaskPanel : MonoBehaviour
{
    public Text lblBudget;

    int budget;
    int Budget
    {
        get
        {
            return budget;
        }
        set
        {
            budget = value;
            if (lblBudget != null)
            {
                lblBudget.text = "£{0}".FormatText(budget);
            }
            else
            {
                Logger.Log("lblBudget is null.");
            }
        }
    }



    private void Awake()
    {
        SingletonUtil.SetInstance(this);
    }
    // Use this for initialization
    void Start()
    {
        ConstructionUtil constructionUtil;
        if (SingletonUtil.InstanceAvailable(out constructionUtil))
        {
            SetContract(constructionUtil.Contract);
        }
    }

    void SetContract(Contract contract)
    {
        Budget = contract.budget;
    }

    // Update is called once per frame
    void Update()
    {

    }
}