#pragma warning disable IDE0017
#pragma warning disable IDE0018
#pragma warning disable IDE0039
#pragma warning disable IDE0044
#pragma warning disable IDE1005

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

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
    public float x = 0.05f;
    public float y = 0.05f;
    public float add = 0.3f;
    void ContractSelection_Click(ContractSelection sender)
    {
        Util.SetContract(sender.Contract);
    }
    private void Start()
    {
        foreach (Contract contract in contracts)
        {
            ContractSelection cs = new ContractSelection(contract);
            cs.Click += ContractSelection_Click;
            cs.SetParent(transform);
            cs.SetPosition(new Vector2(x, y));
            if (x < (1f-add))
            {
                x += add;
            }
            else
            {
                y += add+0.05f;
                x = 0f;
            }
        }
    }
}