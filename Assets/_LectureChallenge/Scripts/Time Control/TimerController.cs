using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerController : MonoBehaviour
{
    private float m_Time = 10.0f;
    public Text m_TimerText;
    private float m_StartTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - m_StartTime < m_Time)
        {
            int timeToFinish = (int)(m_Time - (Time.time - m_StartTime));

            if(timeToFinish < 0)
            {
                timeToFinish = 0;
            }

            m_TimerText.text = timeToFinish.ToString();
        }
    }

    public void StartTimer()
    {
        m_StartTime = Time.time;
    }
}
