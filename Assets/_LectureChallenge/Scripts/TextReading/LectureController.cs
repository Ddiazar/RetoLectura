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

    public GameObject m_StartPanel;

    public GameObject m_Menu;

    public KeyConcept m_KeyConcept;
    private int m_KeyConceptIndex = 0;

    public GameObject m_Summary;
    public Image m_SummaryImage;
    public Text m_SummaryText;
    public Button m_SummaryNext;

    public GameObject m_Misions;
    public Transform m_MisionsReference;
    public GameObject m_MisionPrefab;
    public Button m_StartGame;

    public GameObject m_Story;
    private string inputText = "";
    public Button m_NextBlock;
    public Button m_LectureGuide;

    public GameObject m_Trivial;
    public Text m_Question;
    public Transform m_AlternativeReference;
    public GameObject m_AlternativePrefab;
    private List<GameObject> m_ListOfCurrentAlternatives;
    public Image m_CorrectAlternative;
    public Image m_IncorrectAlternative;

    public GameObject m_Results;
    public Text m_FinalScore;
    public List<GameObject> m_Stars;

    public TimerController m_Timer;
    private int m_Score = 0;

    // Start is called before the first frame update
    void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
        m_StartGame.onClick.AddListener(()=> LoadStory(0));
        m_NextBlock.onClick.AddListener(() => LoadNextStoryBlock());
        m_LectureGuide.onClick.AddListener(() => HightLightByLetters());
        m_ListOfCurrentAlternatives = new List<GameObject>();
        //LoadStory(0);
        //LoadKeyConcept(0);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.D))
        {
            LoadNextStoryBlock();
        }
        
    }

    public void LoadMenu()
    {
        m_StartPanel.SetActive(false);
        m_Menu.SetActive(true);
    }

    public void LoadStory(int storyIndex)
    {        
        m_StoriesIndex = storyIndex;
        m_StoriesBlock = 0;

        m_Story.SetActive(true);
        m_Misions.SetActive(false);
        inputText = GetStoryContent(m_StoriesIndex, m_StoriesBlock);
        m_Tittle.text = GetTittle(m_StoriesIndex);
        m_AudioSource.clip = GetStoryAudioContent(m_StoriesIndex, m_StoriesBlock);
        //m_Timer.StartTimer(m_AudioSource.clip.length * 1.2f);
        m_TimeToHighLight = (m_AudioSource.clip.length / inputText.ToCharArray().Length) * 0.75f;

        m_SimpleText.text = inputText;
        m_LettersArray = inputText.ToCharArray();
        //HightLightByLetters();
    }

    public void LoadNextStoryBlock()
    {
        //if(m_StoriesBlock < m_DataBase.m_Stories[m_StoriesIndex].m_TextBlocks.Count - 1)
        if(m_StoriesBlock < 2)
        {
            m_StoriesBlock++;
            m_HighLightedText.text = "";
            m_AudioSource.Stop();

            inputText = GetStoryContent(m_StoriesIndex, m_StoriesBlock);
            m_AudioSource.clip = GetStoryAudioContent(m_StoriesIndex, m_StoriesBlock);
            m_SimpleText.text = inputText;
            m_LettersArray = inputText.ToCharArray();
            //HightLightByLetters();
            m_NextBlock.gameObject.SetActive(false);
            m_LectureGuide.gameObject.SetActive(true);
        }        
        else
        {
            m_Story.SetActive(false);
            m_Trivial.SetActive(true);
            LoadTrivial(0, m_KeyConceptIndex);
        }
    }

    private void HightLightByLetters()
    {
        //m_SimpleText.text = inputText;
        //m_LettersArray = inputText.ToCharArray();
        m_AudioSource.Play();
        m_LectureGuide.gameObject.SetActive(false);
        StartCoroutine(ShowHighLightedWords());
        //StartCoroutine(ShowHighLightedLetters());
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
        m_NextBlock.gameObject.SetActive(true);
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
        m_NextBlock.gameObject.SetActive(true);
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

    public void LoadKeyConcept(int storyIndex)
    {
        m_Menu.SetActive(false);
        m_KeyConcept.gameObject.SetActive(true);
        m_KeyConcept.m_NextElement.onClick.AddListener(() => LoadNextKeyConcept(storyIndex));
        m_SummaryNext.onClick.AddListener(() => LoadMision(storyIndex));

        m_KeyConcept.LoadKeyConcept(m_DataBase.m_Stories[storyIndex].m_KeyConcepts[m_KeyConceptIndex].m_Concept,
            m_DataBase.m_Stories[storyIndex].m_KeyConcepts[m_KeyConceptIndex].m_ConceptImage,
            m_DataBase.m_Stories[storyIndex].m_KeyConcepts[m_KeyConceptIndex].m_Definition);
    }

    public void LoadNextKeyConcept(int storyIndex)
    {
        if(m_KeyConceptIndex < m_DataBase.m_Stories[storyIndex].m_KeyConcepts.Count - 1)
        {
            m_KeyConceptIndex++;
            m_KeyConcept.LoadKeyConcept(m_DataBase.m_Stories[storyIndex].m_KeyConcepts[m_KeyConceptIndex].m_Concept,
            m_DataBase.m_Stories[storyIndex].m_KeyConcepts[m_KeyConceptIndex].m_ConceptImage,
            m_DataBase.m_Stories[storyIndex].m_KeyConcepts[m_KeyConceptIndex].m_Definition);
        }
        else
        {
            m_KeyConceptIndex = 0;
            m_KeyConcept.gameObject.SetActive(false);
            LoadSummary(storyIndex);
        }
    }

    public void LoadSummary(int storyIndex)
    {
        m_Summary.SetActive(true);
        m_SummaryImage.sprite = m_DataBase.m_Stories[storyIndex].m_MainImage;
        m_SummaryText.text = m_DataBase.m_Stories[storyIndex].m_Summary;
    }

    public void LoadMision(int storyIndex)
    {
        m_Summary.SetActive(false);
        m_Misions.SetActive(true);
        
        for(int i = 0; i < m_DataBase.m_Stories[storyIndex].m_Misions.Count; i++)
        {
            GameObject mision = (GameObject)Instantiate(m_MisionPrefab, m_MisionsReference);
            mision.GetComponent<Text>().text = (i + 1).ToString() + ".-  " + m_DataBase.m_Stories[storyIndex].m_Misions[i].m_Mision;
            mision.GetComponent<RectTransform>().localPosition += new Vector3(0, i * - 40.0f, 0);
        }
    }

    public void LoadTrivial(int storyIndex, int misionIndex)
    {
        m_Question.text = m_DataBase.m_Stories[storyIndex].m_Misions[misionIndex].m_Question;
        //m_KeyConceptIndex = 0;
        for (int i = 0; i < m_DataBase.m_Stories[storyIndex].m_Misions[misionIndex].m_Alternatives.Count; i++)
        {
            GameObject alternative = (GameObject)Instantiate(m_AlternativePrefab, m_AlternativeReference);
            alternative.GetComponentInChildren<Text>().text = m_DataBase.m_Stories[storyIndex].m_Misions[misionIndex].m_Alternatives[i];
            alternative.GetComponent<RectTransform>().localPosition += new Vector3(0, i * -60.0f, 0);
            alternative.GetComponent<TrivialAlternative>().m_Index = i;
            alternative.GetComponentInChildren<Button>().onClick.AddListener(() => VerifyAlternative(storyIndex, misionIndex, alternative.GetComponent<TrivialAlternative>().m_Index));
            m_ListOfCurrentAlternatives.Add(alternative);
        }
    }

    public void VerifyAlternative(int storyIndex, int misionIndex, int alternativeIndex)
    {
        Debug.Log(m_DataBase.m_Stories[storyIndex].m_Misions[misionIndex].m_CorrectAnswer + "   " + alternativeIndex);

        for (int i = 0; i < m_ListOfCurrentAlternatives.Count; i++)
        {
            m_ListOfCurrentAlternatives[i].GetComponentInChildren<Button>().enabled = false;
        }

        if (m_DataBase.m_Stories[storyIndex].m_Misions[misionIndex].m_CorrectAnswer == alternativeIndex)
        {
            m_Score++;
            StartCoroutine(ShowAnswerState(true));
        }
        else
        {
            StartCoroutine(ShowAnswerState(false));
        }
        
    }

    IEnumerator ShowAnswerState(bool isCorrectAnswer)
    {
        if(isCorrectAnswer)
        {
            m_CorrectAlternative.gameObject.SetActive(true);
            m_IncorrectAlternative.gameObject.SetActive(false);
        }
        else
        {
            m_CorrectAlternative.gameObject.SetActive(false);
            m_IncorrectAlternative.gameObject.SetActive(true);
        }
        yield return new WaitForSeconds(1.0f);
        m_CorrectAlternative.gameObject.SetActive(false);
        m_IncorrectAlternative.gameObject.SetActive(false);
        NextTrivialQuestion();
    }

    public void NextTrivialQuestion()
    {
        for (int i = 0; i < m_ListOfCurrentAlternatives.Count; i++)
        {
            Destroy(m_ListOfCurrentAlternatives[i].gameObject);
        }
        m_ListOfCurrentAlternatives.Clear();

        if (m_KeyConceptIndex < m_DataBase.m_Stories[m_StoriesIndex].m_Misions.Count - 1)
        {
            m_KeyConceptIndex++;            
            LoadTrivial(0, m_KeyConceptIndex);
        }
        else
        {
            m_KeyConceptIndex = 0;
            m_Trivial.SetActive(false);
            //m_Results.SetActive(true);
            LoadResults();
        }
    }

    public void LoadResults()
    {
        string starsValuePref = m_StoriesIndex < 10 ? "Level0" + m_StoriesIndex.ToString() : "Level" + m_StoriesIndex.ToString();
        print(starsValuePref);
        PlayerPrefs.SetInt(starsValuePref, m_Score);

        for (int i = 0; i < m_Stars.Count; i++)
        {
            m_Stars[i].SetActive(false);
        }

        m_Results.SetActive(true);
        m_FinalScore.text = m_Score.ToString();
        
        for (int i = 0; i < m_Score; i++)
        {
            m_Stars[i].SetActive(true);
        }
    }

    public void BackToMenu()
    {
        m_Results.SetActive(false);
        m_Menu.SetActive(true);

        foreach(var level in FindObjectsOfType<LevelIndicator>())
        {
            level.InitializeValues();
        }
    }
}
