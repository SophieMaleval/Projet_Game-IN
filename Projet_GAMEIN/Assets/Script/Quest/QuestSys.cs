using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;


public class QuestSys : MonoBehaviour
{
    /*[Header("Sans Quête")]
    public string roamingTitle;
    [TextArea] public string roamingGoal;*/

    [Header("Quest Manager")]
    public TextMeshProUGUI title;
    public TextMeshProUGUI titleEffect;
    public TextMeshProUGUI contenu;
    public List<QuestCt> quest = new List<QuestCt>();
    public int niveau = 0;
    QuestCt questCount;
    public int etape = 0;
    int sizeOfList;

    [Header("Animation")]
    public CanvasGroup animTitle;
    public CanvasGroup animContent;
    public float duration;


    private void Start()
    {
        title = GameObject.Find("Intitulé").GetComponent<TextMeshProUGUI>();
        titleEffect = GameObject.Find("Ombre").GetComponent<TextMeshProUGUI>();
        contenu = GameObject.Find("Description").GetComponent<TextMeshProUGUI>();
        sizeOfList = quest.Count;
        quest[niveau].questCode = niveau; 
        animTitle = GameObject.Find("AnimTitle").GetComponent<CanvasGroup>();
        animContent = GameObject.Find("AnimContent").GetComponent<CanvasGroup>();
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
            StartCoroutine(FadeAllOut());
        }

        else
        {
            StartCoroutine(FadeContentOut());
        }
    }
    
    public void NextStep()
    {
        etape++;
    }

    public void NextLevel()
    {
        niveau++;
        etape = 0;
        if(niveau > sizeOfList)
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

    //fadeOut
    IEnumerator FadeAllOut()
    {
        animContent.DOFade(0f, 0.3f);
        animTitle.DOFade(0f, 0.3f);
        Debug.Log("Fade tout out");
        yield return new WaitForSeconds(duration);
        NextLevel();
        //niveau++;
        //etape = 0;
        if (niveau > sizeOfList - 1)
        {
            //Roaming();
            FadeAllIn();
        }
        else
        {
            FadeAllIn();
        }        
    }



    IEnumerator FadeAllOutB()
    {
        animContent.DOFade(0f, 0.3f);
        animTitle.DOFade(0f, 0.3f);
        Roaming();
        Debug.Log("Fade + va en balade");
        yield return new WaitForSeconds(duration);
        //Roaming();
        FadeAllIn();
    }

    IEnumerator FadeContentOut()
    {
        animContent.DOFade(0f, 0.3f);
        Debug.Log("Fade le contenu out");
        yield return new WaitForSeconds(duration);
        NextStep();
        //etape++;
        FadeContentIn();           
    }

    //fadeIn
    void FadeAllIn()
    {
        animContent.DOFade(1f, 0.3f);
        animTitle.DOFade(1f, 0.3f);
        //yield return new WaitForSeconds(duration);
    }

    void FadeContentIn()
    {
        animContent.DOFade(1f, 0.3f);
        //yield return new WaitForSeconds(duration);
    }
}
