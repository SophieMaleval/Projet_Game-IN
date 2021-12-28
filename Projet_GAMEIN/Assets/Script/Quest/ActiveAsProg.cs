using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveAsProg : MonoBehaviour
{
    //active et desactive des objets en fonction de la progression du joueur
    [HideInInspector]
    public QuestSys questSys;
    public int numeroDeQuete;
    public int etapeDeQuete;
    //public int level; // à titre indicatif uniquement
    //public int step; // à  titre indicatif uniquement
    public Interactible interactible;
    Collider2D detecteur;

    void Start()
    {
        questSys = GameObject.Find("QuestManager").GetComponent<QuestSys>();
        interactible = GetComponent<Interactible>(); //la composante doit, si déterminante pour une quête, être inactive sur l'objet
        detecteur = GetComponent<Collider2D>();
    }

    void Update()
    {
        if (questSys.niveau == numeroDeQuete && questSys.etape + 1 == etapeDeQuete)
        {
            interactible.enabled = true;
            detecteur.enabled = true;
        }
        else
        {
            detecteur.enabled = false;
        }
    }
}
