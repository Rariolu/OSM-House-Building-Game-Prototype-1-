﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ContractSelectionScript : MonoBehaviour
{
    public Text lblContractName;
    public UIButton btnStart;
    public UIButton btnBrochure;
    Contract contract;
    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }
    public void SetContract(Contract _contract)
    {
        contract = _contract;
        if (lblContractName != null)
        {
            lblContractName.text = contract.name;
        }
    }
    
    void Start()
    {
        if (btnStart != null)
        {
            btnStart.Click += StartContract;
        }
        else
        {
            Logger.Log("btnStart was null.");
        }
        if (btnBrochure != null)
        {
            btnBrochure.Click += (sender) => { Logger.Log("btnBrochure clicked."); };
        }
        else
        {
            Logger.Log("btnBrochure was null.");
        }
    }
    void StartContract(UIButton sender)
    {
        if (contract != null)
        {
            Util.SetContract(contract);
        }
        else
        {
            Logger.Log("Contract was null.");
        }
    }
}