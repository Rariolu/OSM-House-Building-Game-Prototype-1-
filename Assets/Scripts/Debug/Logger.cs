using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// A resource used for logging. This uses the standard
/// Unity logging methods but also sends messages to an
/// instance of UILogger so that they're visible
/// in-game.
/// </summary>
public static class Logger
{
    public static void Log(string text, LogType logType = LogType.Log)
    {
        switch(logType)
        {
            case LogType.Warning:
            {
                Debug.LogWarning(text);
                break;
            }
            default:
            {
                Debug.Log(text);
                break;
            }
        }
        UILogger uiLogger;
        if (SingletonUtil.InstanceAvailable(out uiLogger))
        {
            uiLogger.Log(text, logType);
        }
        else
        {
            backLog.Enqueue(text);
            backLogTypes.Enqueue(logType);
        }
    }

    public static void DequeueBackLog(UILogger uiLogger)
    {
        while (backLog.Count > 0)
        {
            uiLogger.Log(backLog.Dequeue(), backLogTypes.Dequeue());
        }
    }

    static Queue<string> backLog = new Queue<string>();
    static Queue<LogType> backLogTypes = new Queue<LogType>();
    public static void Log(object obj, LogType logType = LogType.Log)
    {
        Log(obj.ToString(),logType);
    }
    public static void Log(string format, params object[] args)
    {
        Log(string.Format(format, args));
    }
    public static void Log(string format, LogType logType = LogType.Log, params object[] args)
    {
        Log(string.Format(format, args), logType);
    }
}