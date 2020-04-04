using UnityEngine;
using System.Collections;

public class EnvironmentScript : MultitonScript<EnvironmentScript,FINISHED_CONSTRUCTION>
{
    public FINISHED_CONSTRUCTION contract;
    private void Awake()
    {
        SetActive(false);
        SetInstance(contract, this);
        ConstructionUtil util;
        if (SingletonUtil.InstanceAvailable(out util))
        {
            SetActive(contract == util.Contract.finishedConstruction);
        }
        else
        {
            SetActive(contract == FINISHED_CONSTRUCTION.SEMI_DETACHED_HOUSE);
        }
        //SetActive(false);
    }

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }
}