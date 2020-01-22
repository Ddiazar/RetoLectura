using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ChildrenStory 
{
    public string m_Tittle;
    public Sprite m_MainImage;
    public string m_Summary;
    public List<ChildrenStoryMision> m_Misions;
    public List<ChildrenStoryKeyConcept> m_KeyConcepts;
    public List<ChildrenStoryBlock> m_TextBlocks;
}

[System.Serializable]
public class ChildrenStoryBlock
{
    public string m_Content;
    public AudioClip m_AudioContent;
}

[System.Serializable]
public class ChildrenStoryKeyConcept
{
    public string m_Concept;
    public string m_Definition;
    public Sprite m_ConceptImage;
}

[System.Serializable]
public class ChildrenStoryMision
{
    public string m_Mision;
    public string m_Question;
    public List<string> m_Alternatives;
    public int m_CorrectAnswer;
}