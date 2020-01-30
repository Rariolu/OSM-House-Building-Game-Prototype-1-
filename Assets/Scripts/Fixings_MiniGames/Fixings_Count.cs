using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// A script which updates the UI with the current amount of
/// used fixings.
/// </summary>
public class Fixings_Count : MonoBehaviour
{
    public Text fixingsUsedText;

    private void Start()
    {
        fixingsUsedText = GetComponent<Text>();
    }

    private void Update()
    {
        fixingsUsedText.text = "Fixings: " + Fixings.fixings.ToString();
    }
}
