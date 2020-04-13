using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContractUI : MonoBehaviour
{
    public string Menu;
    public bool IsContractActive;
    public GameObject ContractMenuCanvas;

    bool contractActive = false;
    public void ContractActive()
    {
        contractActive = !contractActive;
        ContractMenuCanvas.SetActive(contractActive);
    }
    public void Continue()
    {
        IsContractActive = false;
    }
}