#pragma warning disable IDE0017
#pragma warning disable IDE0018
#pragma warning disable IDE0039
#pragma warning disable IDE0044
#pragma warning disable IDE1005

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

//[ExecuteInEditMode]

/// <summary>
/// A class which manages contracts by creating a UI element for each one and adding it to the canvas.
/// </summary>
public class ContractManager : MonoBehaviour
{
    /// <summary>
    /// The array of contracts to be displayed
    /// </summary>
    public Contract[] contracts;

    static bool staticContractsSet = false;
    static Contract[] staticContracts;

    private void Start()
    {
        if (!staticContractsSet)
        {
            staticContracts = contracts;
            staticContractsSet = true;
        }
        Logger.Log("ContractManager->Start()");
        for (int i = 0; i < staticContracts.Length; i++)
        {
            Contract contract = staticContracts[i];
            ContractSelectionScript contractSelection;
            if (ContractSelectionScript.InstanceExists(contract.finishedConstruction,out contractSelection))
            {
                contractSelection.SetContract(contract);
            }
        }
        //ContractSelectionScript[] contractSelections = ContractSelectionScript.Values;
        //for (int i = 0; i < contractSelections.Length; i++)
        //{
        //    if (i < staticContracts.Length)
        //    {
        //        Contract contract = staticContracts[i];
        //        contractSelections[i].SetContract(contract);
        //    }
        //    else
        //    {
        //        contractSelections[i].SetActive(false);
        //    }
        //}
    }

    public static Dictionary<FINISHED_CONSTRUCTION, int> highestSellingPrices = new Dictionary<FINISHED_CONSTRUCTION, int>();
}