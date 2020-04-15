using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// A script which updates the UI with the current amount of
/// used fixings.
/// </summary>
[RequireComponent(typeof(Text))]
public class Fixings_Count : MonoBehaviour
{
    Text lbl;
    Text fixingsUsedText
    {
        get
        {
            return lbl ?? (lbl = GetComponent<Text>());
        }
    }

    private void Start()
    {
        FixingsUtil util;
        if (SingletonUtil.InstanceAvailable(out util))
        {
            fixingsUsedText.text = "Fixings: {0}".FormatText(util.Fixings);
            util.FixingsChanged += (fixings) => { fixingsUsedText.text = "Fixings: {0}".FormatText(fixings); };
        }
    }
}
