using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SubtaskPanel : MonoBehaviour
{
    public Text lblBudget;
    public Text lblFixtures;
    public SubtaskCheckbox cbDestroyedPrefabs;

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

    int fixtures;
    int Fixtures
    {
        get
        {
            return fixtures;
        }
        set
        {
            fixtures = value;
            if (lblFixtures != null)
            {
                lblFixtures.text = "Fixtures: {0}".FormatText(fixtures);
            }
            else
            {
                Logger.Log("lblFixtures is null.");
            }
        }
    }

    private void Awake()
    {
        SingletonUtil.SetInstance(this);
        gameObject.SetActive(false);
    }

    void Start()
    {
        InGameSceneScript gameSceneScript;
        if (SingletonUtil.InstanceAvailable(out gameSceneScript))
        {
            gameSceneScript.FixturesChanged += (fixs) => { Fixtures = fixs; };
        }

        ConstructionUtil constructionUtil;
        if (SingletonUtil.InstanceAvailable(out constructionUtil))
        {
            SetContract(constructionUtil.Contract);
            constructionUtil.DestroyedPrefabChange += (prefabs) => { cbDestroyedPrefabs.CompletionState = SubtaskCheckbox.COMPLETION_STATE.FAILED; };
        }

        if (cbDestroyedPrefabs != null)
        {
            cbDestroyedPrefabs.CompletionState = SubtaskCheckbox.COMPLETION_STATE.SUCCEEDED;
        }
    }

    void SetContract(Contract contract)
    {
        Budget = contract.budget;
        Fixtures = contract.fixtures;
    }
}