using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpecTestScript : MonoBehaviour
{
    public Text lblFPS;
    //Declare these in your class
    int m_frameCounter = 0;
    float m_timeCounter = 0.0f;
    float m_lastFramerate = 0.0f;
    float LastFrameRate
    {
        get
        {
            return m_lastFramerate;
        }
        set
        {
            m_lastFramerate = value;
            if (lblFPS != null)
            {
                lblFPS.text = m_lastFramerate.ToString();
            }
            else
            {
                Logger.Log("lblFps is null.");
            }
        }
    }
    public float m_refreshTime = 0.5f;


    private void Start()
    {
        if (m_refreshTime == 0f)
        {
            m_refreshTime = 0.5f;
        }
    }

    void Update()
    {
        if (m_timeCounter < m_refreshTime)
        {
            m_timeCounter += Time.deltaTime;
            m_frameCounter++;
        }
        else
        {
            LastFrameRate = m_frameCounter / m_timeCounter;
            m_frameCounter = 0;
            m_timeCounter = 0.0f;
        }
    }
}