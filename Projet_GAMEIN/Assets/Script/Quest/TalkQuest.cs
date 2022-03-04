using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkQuest : MonoBehaviour
{
    private QuestSys questSys;

    public Vector2 QuestEtape ;
    public bool RemoveObjectFromInventory ;
    public InteractibleObject ObjectAsRemove ;


    private void Awake()
    {
        questSys = GameObject.Find("QuestManager").GetComponent<QuestSys>();
    }

    public void TalkedTo()
    {
        if(RemoveObjectFromInventory && QuestEtape.x == questSys.niveau && QuestEtape.y == questSys.etape)
        {
            GameObject.Find("Player").GetComponent<PlayerScript>().RemoveObject(ObjectAsRemove);
        }


        questSys.Progression();        
        //Destroy(this.gameObject, 0.05f);
    }
}
