using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyConcept : MonoBehaviour
{
    public Text m_Concept;
    public Image m_ConceptReferenceImage;
    public Text m_Definition;
    public Button m_NextElement;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadKeyConcept(string concept, Sprite referenceImage, string definition)
    {
        m_Concept.text = concept;
        m_ConceptReferenceImage.sprite = referenceImage;
        m_Definition.text = definition;
    }
}
