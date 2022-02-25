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
    public int lvlTracker = 0;
    int firstLvlStep, secondLvlStep, thirdLvlStep, fourthLvlStep = 0;
    
    [Header("Quest Manager")]
    public TextMeshProUGUI title;
    public TextMeshProUGUI titleEffect;
    public TextMeshProUGUI contenu;

    [HideInInspector] public List<QuestCt> QuestFR = new List<QuestCt>() ;
    [HideInInspector] public List<QuestCt> QuestEN = new List<QuestCt>() ;
    public List<QuestCt> quest = new List<QuestCt>();
    
    [HideInInspector]
    public int niveau = 0;
    
    QuestCt questCount;
    
    [HideInInspector]
    public int etape = 0;
    public int sizeOfList;
    public int globalSteps = 0;

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
        lvlTracker = niveau;
        animTitle = GameObject.Find("AnimTitle").GetComponent<CanvasGroup>();
        animContent = GameObject.Find("AnimContent").GetComponent<CanvasGroup>();
        niveau = PlayerPrefs.GetInt("Niveau");
    }

    private void Update()
    {
        if(PlayerPrefs.GetInt("Langue") == 0 && quest != QuestFR) SetLanguage() ;
        if(PlayerPrefs.GetInt("Langue") == 1 && quest != QuestEN) SetLanguage() ;


        title.text = quest[niveau].questTitle;
        titleEffect.text = quest[niveau].questTitle; //fond ombre
        contenu.text = quest[niveau].questGoal[etape];
        quest[niveau].questCode = niveau;
        if (niveau == 0)
        {
            etape = 0;
        }
        if (niveau == 1)
        {
            firstLvlStep = etape;
        }
        if (niveau == 2)
        {
            secondLvlStep = etape;
        }
        if (niveau == 3)
        {
            thirdLvlStep = etape;
        }
    }

    private void SetLanguage() 
    {
        if(PlayerPrefs.GetInt("Langue") == 0) quest = QuestFR ;
        if(PlayerPrefs.GetInt("Langue") == 1) quest = QuestEN ;
    }

    //gestion progression des niveaux en jeu
    public void Progression()
    {
        globalSteps++;
        if (etape > quest[niveau].questGoal.Count - 2)
        {
            StartCoroutine(FadeAllOut());
            lvlTracker++;
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


    
    
    // Tracking de la progression des niveaux sur boutons
    
    public void LevelZero()
    {
        if (niveau == 0)
        {
            etape = 0;
        }
    }
    public void LevelOne()
    {
        if (niveau == 1)
        {
            etape = firstLvlStep;
        }
    }

    public void LevelTwo()
    {
        if (niveau == 2)
        {
             etape = secondLvlStep;
        }
    }

    public void LevelThree()
    {
        if (niveau == 3)
        {
             etape = thirdLvlStep;
        }
    }
    //animation UI
    IEnumerator FadeAllOut()
    {
        animContent.DOFade(0f, 0.3f);
        animTitle.DOFade(0f, 0.3f);
        yield return new WaitForSeconds(duration);
        NextLevel();
        if (niveau > sizeOfList - 1)
        {
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
        FadeAllIn();
    }

    IEnumerator FadeContentOut()
    {
        animContent.DOFade(0f, 0.3f);
        Debug.Log("Fade le contenu out");
        yield return new WaitForSeconds(duration);
        NextStep();
        FadeContentIn();           
    }

    //fadeIn
    void FadeAllIn()
    {
        animContent.DOFade(1f, 0.3f);
        animTitle.DOFade(1f, 0.3f);
    }

    void FadeContentIn()
    {
        animContent.DOFade(1f, 0.3f);
    }
}
