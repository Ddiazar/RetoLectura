using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LectureController : MonoBehaviour
{
    private int m_StoriesIndex = 0;
    private int m_StoriesBlock = 0;
    public DataBaseStories m_DataBase;

    public Text m_SimpleText;
    public Text m_Tittle;

    private char[] m_LettersArray;
    private float m_TimeToHighLight = 0.55f;
    public Text m_HighLightedText;

    private AudioSource m_AudioSource;

    // Start is called before the first frame update
    void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
        LoadStory(0);
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.D))
        {
            LoadNextStoryBlock();
        }
        
    }

    public void LoadStory(int storyIndex)
    {
        m_StoriesIndex = storyIndex;
        m_StoriesBlock = 0;

        string inputText = GetStoryContent(m_StoriesIndex, m_StoriesBlock);
        m_Tittle.text = GetTittle(m_StoriesIndex);
        m_AudioSource.clip = GetStoryAudioContent(m_StoriesIndex, m_StoriesBlock);
        m_TimeToHighLight = (m_AudioSource.clip.length / inputText.ToCharArray().Length) * 0.75f;
        HightLightByLetters(inputText);
    }

    public void LoadNextStoryBlock()
    {
        if(m_StoriesBlock < m_DataBase.m_Stories[m_StoriesIndex].m_TextBlocks.Count - 1)
        {
            m_StoriesBlock++;
            m_HighLightedText.text = "";
            m_AudioSource.Stop();

            string inputText = GetStoryContent(m_StoriesIndex, m_StoriesBlock);
            m_AudioSource.clip = GetStoryAudioContent(m_StoriesIndex, m_StoriesBlock);
            HightLightByLetters(inputText);
        }        
    }

    private void HightLightByLetters(string inputText)
    {
        m_SimpleText.text = inputText;
        m_LettersArray = inputText.ToCharArray();
        m_AudioSource.Play();
        StartCoroutine(ShowHighLightedWords());
    }

    IEnumerator ShowHighLightedLetters()
    {
        int index = 0;
        string highLightedText = "";
        while(index < m_LettersArray.Length)
        {
            highLightedText += m_LettersArray[index].ToString();
            m_HighLightedText.text = highLightedText;
            index++;
            yield return new WaitForSeconds(m_TimeToHighLight);
            
        }
        yield return null;
    }

    IEnumerator ShowHighLightedWords()
    {
        int index = 0;
        string highLightedText = "";
        while (index < m_LettersArray.Length)
        {
            highLightedText += m_LettersArray[index].ToString();
            if (char.IsWhiteSpace(m_LettersArray[index]) || index == m_LettersArray.Length - 1)
            {                
                m_HighLightedText.text = highLightedText;
                //yield return new WaitForSeconds(m_TimeToHighLight);
            }
            yield return new WaitForSeconds(m_TimeToHighLight);
            index++;
        }
        yield return null;
    }

    public string GetTittle(int storyIndex)
    {
        return m_DataBase.m_Stories[storyIndex].m_Tittle;
    }

    public string GetStoryContent(int storyIndex, int blockIndex)
    {
        return m_DataBase.m_Stories[storyIndex].m_TextBlocks[blockIndex].m_Content;
    }

    public AudioClip GetStoryAudioContent(int storyIndex, int blockIndex)
    {
        return m_DataBase.m_Stories[storyIndex].m_TextBlocks[blockIndex].m_AudioContent;
    }
}
