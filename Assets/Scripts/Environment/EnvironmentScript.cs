using UnityEngine;
using System.Collections;

public class EnvironmentScript : MultitonScript<EnvironmentScript,FINISHED_CONSTRUCTION>
{
    public FINISHED_CONSTRUCTION contract;
    private void Awake()
    {
        SetActive(false);
        SetInstance(contract, this);
        bool active;
        ConstructionUtil util;
        if (SingletonUtil.InstanceAvailable(out util))
        {
            active = (contract == util.Contract.finishedConstruction);
        }
        else
        {
            active = (contract == FINISHED_CONSTRUCTION.SEMI_DETACHED_HOUSE);
        }
        if (!active)
        {
            Destroy(gameObject);
        }
        else
        {
            SetActive(true);
        }
        //SetActive(false);
    }

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }
}