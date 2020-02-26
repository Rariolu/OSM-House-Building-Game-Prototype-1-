#pragma warning disable IDE0017
#pragma warning disable IDE0018
#pragma warning disable IDE0039
#pragma warning disable IDE0044
#pragma warning disable IDE1005

#if UNITY_EDITOR

using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;

public class ContractEditing : ScriptableObject
{
    const string dir = "Assets\\Contracts";

    [MenuItem("Tools/Contract/Save")]
    static void SaveContracts()
    {
        if (EditorUtility.DisplayDialog("Save", "Save (and overwrite) contract files?", "OK", "Cancel"))
        {
            GameObject pnlMain = GameObject.Find("pnlMain");
            if (pnlMain != null)
            {
                ContractManager contractManager = pnlMain.GetComponent<ContractManager>();
                if (contractManager != null)
                {
                    Contract[] contracts = contractManager.contracts;
                    Dictionary<string,int> typenames = new Dictionary<string, int>();
                    if (contracts.Length > 0)
                    {
                        foreach (Contract contract in contracts)
                        {
                            string typename = "{0}_{1}".FormatText(contract.name, contract.finishedConstruction);
                            string aggr;
                            if (typenames.ContainsKey(typename))
                            {
                                aggr = (typenames[typename]++).ToString();
                            }
                            else
                            {
                                typenames.Add(typename, 0);
                                aggr = "0";
                            }
                            XMLUtil.SaveContract(dir, contract, aggr);
                        }
                    }
                    else
                    {
                        Debug.Log("No contracts");
                    }
                }
                else
                {
                    Debug.Log("No contract manager");
                }
            }
            else
            {
                Debug.Log("pnlMain is null");
            }
        }
    }

    [MenuItem("Tools/Contract/Load")]
    static void LoadContracts()
    {
        if (EditorUtility.DisplayDialog("Load", "Load contract files over current settings?", "OK", "Cancel"))
        {
            string[] xmlfiles = Directory.GetFiles(dir, "*.xml");
            Contract[] contracts = new Contract[xmlfiles.Length];
            for (int i = 0; i < xmlfiles.Length; i++)
            {
                Contract c;
                if (XMLUtil.LoadContract(xmlfiles[i], out c))
                {
                    contracts[i] = c;
                }
            }
            GameObject pnlMain = GameObject.Find("pnlMain");
            if (pnlMain != null)
            {
                ContractManager contractManager = pnlMain.GetComponent<ContractManager>();
                if (contractManager != null)
                {
                    contractManager.contracts = contracts;
                }
            }
        }
    }
}

#endif