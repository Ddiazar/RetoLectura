using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ChildrenStory 
{
    public string m_Tittle;
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