using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveAsProg : MonoBehaviour
{
    public QuestSys questSys;
    public int numeroDeQuete;
    public int etapeDeQuete;
    public int level;
    public int step;
    public Interactible interactible;
    Collider2D detecteur;

    void Start()
    {
        questSys = GameObject.Find("QuestManager").GetComponent<QuestSys>();
        interactible = GetComponent<Interactible>(); //la composante doit, si déterminante pour une quête, être inactive sur l'objet
        detecteur = GetComponent<Collider2D>();

    }

    private void OnEnable()
    {
        
    }

    void Update()
    {
        if (questSys.niveau + 1 == numeroDeQuete && questSys.etape + 1 == etapeDeQuete)
        {
            interactible.enabled = true;
            detecteur.enabled = true;
        }
        else
        {
            detecteur.enabled = false;
        }
        step = questSys.niveau;
        level = questSys.etape;
    }
}
