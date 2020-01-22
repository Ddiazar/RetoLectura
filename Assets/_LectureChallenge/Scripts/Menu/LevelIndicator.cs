using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelIndicator : MonoBehaviour
{
    public bool m_LevelIsActive = false;
    public GameObject m_ActiveObject;
    public GameObject m_InactiveObject;
    public Text m_Value;

    public List<GameObject> m_Stars;
    [Range(0,3)]
    public int m_StarsValue;
    public int m_LevelText;



    // Start is called before the first frame update
    void Start()
    {
        InitializeValues();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitializeValues()
    {
        if(m_LevelIsActive)
        {
            string starsValuePref = m_LevelText < 10 ? "Level0" + m_LevelText.ToString() : "Level" + m_LevelText.ToString();
            
            m_StarsValue = PlayerPrefs.GetInt(starsValuePref, 0);

            print(starsValuePref + "   " + m_StarsValue);
            m_ActiveObject.SetActive(true);
            m_InactiveObject.SetActive(false);
            //m_Value.text = level;

            for(int i = 0; i < m_StarsValue; i++)
            {
                m_Stars[i].SetActive(true);
            }
        }
        else
        {
            m_ActiveObject.SetActive(false);
            m_InactiveObject.SetActive(true);
            //m_Value.text = level;
        }
    }
}
