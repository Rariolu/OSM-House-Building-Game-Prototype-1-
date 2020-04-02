using UnityEngine;
using System.Collections;

public class EnvironmentScript : MultitonScript<EnvironmentScript,FINISHED_CONSTRUCTION>
{
    public FINISHED_CONSTRUCTION contract;
    private void Awake()
    {
        SetInstance(contract, this);
    }

    public void SetActive(bool active)
    {
        gameObject.SetActive(true);
    }
}