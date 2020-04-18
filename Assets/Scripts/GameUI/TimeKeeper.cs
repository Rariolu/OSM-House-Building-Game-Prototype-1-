using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Text))]
public class TimeKeeper : MonoBehaviour
{
    Text label;
    Text Label
    {
        get
        {
            return label ?? (label = GetComponent<Text>());
        }
    }
    private void Awake()
    {
        SingletonUtil.SetInstance(this);
    }
    
    void Start()
    {
        ConstructionUtil util;
        if (SingletonUtil.InstanceAvailable(out util))
        {
            util.TimePassed += TimePassed;
        }
    }

    void TimePassed(uint time)
    {
        Label.text = "Month: {0}".FormatText(time);
    }
}