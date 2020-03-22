using UnityEngine;
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
    // Use this for initialization
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