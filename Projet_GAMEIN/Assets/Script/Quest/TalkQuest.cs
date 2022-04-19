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
    private PlayerScript ScriptPlayer ;
    public ModifInventaire mode;
    public bool multipleObj;

    public Vector2 QuestEtape ;
    public InteractibleObject item ;




    private void Awake()
    {
        questSys = GameObject.Find("QuestManager").GetComponent<QuestSys>();
        ScriptPlayer = GameObject.Find("Player").GetComponent<PlayerScript>() ;
    }

    public void TalkedTo()
    {
        if(QuestEtape.x == questSys.niveau && QuestEtape.y == questSys.etape)
        {
            if(mode == ModifInventaire.Retrait) ScriptPlayer.RemoveObject(item);

            if(mode == ModifInventaire.Ajout)
            {
                if(!ScriptPlayer.ItemChecker(item))
                {
                    ScriptPlayer.AjoutInventaire(item);    
                } else {
                    item.AddEntry();
                }     
            }

        }


        //questSys.Progression();        
        //Destroy(this.gameObject, 0.05f);
    }
}
