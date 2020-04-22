using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class Timer : MonoBehaviour//NullableInstanceScriptSingleton<Timer>
{
   
    public float interval = 1f;
    Text label;
    Text Label
    {
        get
        {
            return label ?? (label = GetComponent<Text>());
        }
    }
    bool run = false;

    private void Awake()
    {
        SingletonUtil.SetInstance(this);
    }

    IEnumerator RunTimer()
    {
        ConstructionUtil constructionUtil;
        if (SingletonUtil.InstanceAvailable(out constructionUtil))
        {
            run = true;
            uint dayLimit = constructionUtil.Contract.time;
            float d = 0;
            while (run)
            {
                d += Time.deltaTime;
                if (d >= interval)
                {
                    constructionUtil.IncrementDaysPassed();
                    Label.text = "{0} : {1}".FormatText(constructionUtil.WeeksPassed, dayLimit);
                    d = 0;
                }
                yield return 0;
            }
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        Label.text = "0";
        StartTimer();
    }

    public void StartTimer()
    {
        if (!run)
        {
            StartCoroutine(RunTimer());
        }
    }

    public void StopTimer()
    {
        run = false;
    }
}