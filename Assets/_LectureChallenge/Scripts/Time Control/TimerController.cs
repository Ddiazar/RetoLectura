using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerController : MonoBehaviour
{
    private float m_Time = 10.0f;
    public Text m_TimerText;
    private float m_StartTime;
    [HideInInspector]
    public bool m_IsActive = false;
    public float m_CurrentTime;

    public LectureController m_Controller;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(m_IsActive)
        {
            if (Time.time - m_StartTime < m_Time)
            {
                m_CurrentTime = Time.time - m_StartTime;
                int timeToFinish = (int)(m_Time - (Time.time - m_StartTime));

                if (timeToFinish < 0)
                {
                    timeToFinish = 0;
                }

                m_TimerText.text = TimerFormat(timeToFinish);
            }
            else
            {
                m_IsActive = false;
                m_Controller.ActiveStoryGuide();
            }
        }
        
    }

    public string TimerFormat(int time)
    {
        string timeformat = "00:00";
        if(time < 10)
        {
            timeformat = "00:0" + time.ToString();
        }
        else 
        {
            int minutes = (time - time % 60) / 60;
            int seconds = time % 60;
            if(minutes < 10)
            {
                timeformat = "0" + minutes.ToString() + ":";
                if (seconds < 10)
                {
                    timeformat = timeformat + "0" + seconds.ToString();
                }
                else
                {
                    timeformat = timeformat + seconds.ToString();
                }
            }
            
        }
        return timeformat;
    }

    public void StartTimer(float blockTime)
    {
        m_StartTime = Time.time;
        m_IsActive = true;
        m_Time = blockTime;
    }
}
