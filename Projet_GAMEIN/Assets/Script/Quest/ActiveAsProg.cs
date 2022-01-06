using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ActiveAsProg : MonoBehaviour
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
    public Interactible interactible;
    Collider2D detecteur;
    bool gathered = false;
    public TextMeshProUGUI ecrits;

    void Awake()
    {
        questSys = GameObject.Find("QuestManager").GetComponent<QuestSys>();
        checker = GameObject.Find("Inventory").GetComponent<Checker>();
        interactible = GetComponent<Interactible>(); //la composante doit, si déterminante pour une quête, être inactive sur l'objet
        detecteur = GetComponent<Collider2D>();

    }

    void Update()
    {

        if (questSys.niveau == numeroDeQuete && questSys.etape + 1 == etapeDeQuete)
        {
            interactible.enabled = true;
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
    //prendre les TMPro correspondant et les barrer à chaque incrémentation -1
    public void StrikeThrough()
    {
        if (checker.isOn)
        {
        
                ecrits.text = questSys.quest[numeroDeQuete].questGoal[etapeDeQuete - 1];
               /*if (i >= etapeDeQuete)
               {
                    ecrits.fontStyle = FontStyles.Strikethrough;
               }   */      
        
            
        }

    }
}
