using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AaPTalk : MonoBehaviour
{
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
    Collider2D detecteur;
    bool gathered = false;

    void Awake()
    {
        questSys = GameObject.Find("QuestManager").GetComponent<QuestSys>();
        checker = GameObject.Find("Inventory").GetComponent<Checker>();
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
}
