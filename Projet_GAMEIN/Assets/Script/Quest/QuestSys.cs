using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class QuestSys : MonoBehaviour
{
    /*[Header("Sans Quête")]
    public string roamingTitle;
    [TextArea] public string roamingGoal;*/

    [Header("Quest Manager")]
    [SerializeField] TextMeshProUGUI title;
    [SerializeField] TextMeshProUGUI titleEffect;
    [SerializeField] TextMeshProUGUI contenu;
    public List<QuestCt> quest = new List<QuestCt>();
    int niveau = 0;
    QuestCt questCount;
    int etape = 0;
    int sizeOfList;

    public Animator animTitle;
    public Animator animContent;


    private void Start()
    {
        title = GameObject.Find("Intitulé").GetComponent<TextMeshProUGUI>();
        titleEffect = GameObject.Find("Ombre").GetComponent<TextMeshProUGUI>();
        contenu = GameObject.Find("Description").GetComponent<TextMeshProUGUI>();
        sizeOfList = quest.Count;
        animTitle = GameObject.Find("AnimTitle").GetComponent<Animator>();
        animContent = GameObject.Find("AnimContent").GetComponent<Animator>();
    }

    private void Update()
    {
        title.text = quest[niveau].questTitle;
        titleEffect.text = quest[niveau].questTitle;
        contenu.text = quest[niveau].questGoal[etape];
        quest[niveau].questCode = niveau;
    }

    public void Progression()
    {
        if (etape > quest[niveau].questGoal.Length - 2)
        {
            animTitle.SetTrigger("Done");
            niveau++;
            etape = 0;        
        }
        else
        {
            animContent.SetTrigger("Done");
            etape++;
           
        }

        if (niveau > sizeOfList - 1)
        {
            Roaming();
        }
    }

    public void Roaming()
    {
        etape = 0;
        niveau = 0;
        title.text = quest[0].questTitle;
        titleEffect.text = quest[0].questTitle;
        contenu.text = quest[0].questGoal[0];
    }
}
