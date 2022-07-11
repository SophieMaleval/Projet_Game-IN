using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AaPTalk : MonoBehaviour
{
    #region Fields

    Collider2D detecteur;
    bool gathered = false;

    #endregion

    #region UnityInspector

    //active et desactive des objets en fonction de la progression du joueur
    [HideInInspector]
    public QuestSys questSys;
    [HideInInspector]
    public Checker checker;
    //[HideInInspector]
    public QuestSync questSync;
    public int numeroDeQuete;
    public int etapeDeQuete;
    //public int level; // à titre indicatif uniquement
    //public int step; // à  titre indicatif uniquement
    public TalkQuest talkQuest;

    #endregion

    #region Behaviour

    void Awake()
    {
        questSys = GameManager.Instance.gameCanvasManager.questManager;
        checker = GameManager.Instance.gameCanvasManager.inventory.GetComponent<Checker>();
        talkQuest = GetComponent<TalkQuest>(); //la composante doit, si déterminante pour une quête, être inactive sur l'objet
        detecteur = GetComponent<Collider2D>();
    }

    void Update()
    {

        if (questSys.niveau == numeroDeQuete && questSys.etape + 1 == etapeDeQuete)
        {
            talkQuest.enabled = true;
            detecteur.enabled = true;
            //StrikeThrough();
        }
        else
        {
            detecteur.enabled = false;
        }
        if (!gathered)
        {
            GetTitles();
        }


    }

    public void GetTitles()
    {
        if (checker.isOn)
        {
            checker.isOn = false;
            questSync = GameObject.Find("Quest" + numeroDeQuete).GetComponent<QuestSync>();
            //ecrits.text = questSys.quest[numeroDeQuete].questGoal[etapeDeQuete - 1];
            gathered = true;
        }
    }

    #endregion
}
