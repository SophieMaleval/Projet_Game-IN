using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ModifInventaire{
    Ajout,
    Retrait,
    None
}
public class TalkQuest : MonoBehaviour
{
    private QuestSys questSys;
    public ModifInventaire mode;
    public bool multipleObj;

    public Vector2 QuestEtape ;
    public InteractibleObject item ;


    private void Awake()
    {
        questSys = GameObject.Find("QuestManager").GetComponent<QuestSys>();
    }

    public void TalkedTo()
    {
        if(QuestEtape.x == questSys.niveau && QuestEtape.y == questSys.etape)
        {
            if(mode == ModifInventaire.Retrait) GameObject.Find("Player").GetComponent<PlayerScript>().RemoveObject(item);
            if(mode == ModifInventaire.Ajout) GameObject.Find("Player").GetComponent<PlayerScript>().AjoutInventaire(item);
        }


        //questSys.Progression();        
        //Destroy(this.gameObject, 0.05f);
    }
}
