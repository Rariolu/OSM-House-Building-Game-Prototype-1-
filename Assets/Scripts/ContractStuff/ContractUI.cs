using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContractUI : MonoBehaviour
{
    public string Menu;
    public GameObject ContractMenuCanvas;

    bool contractActive = false;

    private void Start()
    {
        ContractMenuCanvas.SetActive(false);
    }

    public void ContractActive()
    {
        contractActive = !contractActive;
        ContractMenuCanvas.SetActive(contractActive);
    }
}