using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class UILogger : NullableInstanceScriptSingleton<UILogger>
{
    Text lblLog;
    Text LblLog
    {
        get
        {
            return lblLog ?? (lblLog = GetComponent<Text>());
        }
    }
    private void Awake()
    {
        SetInstance(this);
        Logger.DequeueBackLog(this);
    }
    int logs = 0;
    public void Log(string text, LogType logType)
    {
        LblLog.text = string.Format("{0}: {1}\n{2}", (logs++).PadNumber(4), text, LblLog.text);
    }

}