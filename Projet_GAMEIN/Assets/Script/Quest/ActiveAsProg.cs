using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum ObjType
{
    PNJ,
    Item,
    PannelAnnonce
}
public class ActiveAsProg : MonoBehaviour
{
    public ObjType progressType;
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
    public Interactible interactible;
    Collider2D detecteur;
    public TalkQuest talkQuest;
    bool gathered = false;
    public PannelAnnonceScript PannelAnnonceGestion ;

    //différent selon la méthode utilisée
    void Awake()
    {

        if(questSys == null)
        {
            questSys = GameObject.Find("QuestManager").GetComponent<QuestSys>();
            if(numeroDeQuete == 1 &&  questSys.firstLvlStep >= etapeDeQuete) {DestroyThis();  } 
            if(numeroDeQuete == 2 &&  questSys.secondLvlStep >= etapeDeQuete) {DestroyThis(); } 
            if(numeroDeQuete == 3 &&  questSys.thirdLvlStep >= etapeDeQuete) {DestroyThis(); }
            if(numeroDeQuete == 4 &&  questSys.fourthLvlStep >= etapeDeQuete) {DestroyThis(); }
        }

        checker = GameObject.Find("Inventory").GetComponent<Checker>();

        if (progressType == ObjType.Item)
        {
            interactible = GetComponent<Interactible>(); //la composante doit, si déterminante pour une quête, être inactive sur l'objet
        }
        
        if (progressType == ObjType.PNJ)
        {
            talkQuest = GetComponent<TalkQuest>(); //la composante doit, si déterminante pour une quête, être inactive sur l'objet
        }

        if(progressType == ObjType.PannelAnnonce)
        {
            PannelAnnonceGestion = GetComponent<PannelAnnonceScript>() ;
        }

        detecteur = GetComponent<Collider2D>();

    }

    void DestroyThis()
    {
        Destroy(this.gameObject);
    }

    void Update()
    {
        if(progressType == ObjType.Item)    InteractMethod();
        if(progressType == ObjType.PNJ)    DiscussionMethod();
        if(progressType == ObjType.PannelAnnonce) PannelViewMethod();
    }

    public void InteractMethod()
    {
        if (questSys.niveau == numeroDeQuete && questSys.etape + 1 == etapeDeQuete)
        {
            interactible.enabled = true;
            detecteur.enabled = true;
            //StrikeThrough();
        } else {
            detecteur.enabled = false;
        }

        if (!gathered)
        {
            GetTitles();
        }
    }

    public void DiscussionMethod()
    {
        if (questSys.niveau == numeroDeQuete && questSys.etape + 1 == etapeDeQuete)
        {
            talkQuest.enabled = true;
            detecteur.enabled = true;
            //StrikeThrough();
        } else {
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

    void PannelViewMethod()
    {
        if (questSys.niveau == numeroDeQuete && questSys.etape + 1 == etapeDeQuete)
        {
            PannelAnnonceGestion.CanProgress = true ;
        } else {
            PannelAnnonceGestion.CanProgress = false ;
        }
    }


}
