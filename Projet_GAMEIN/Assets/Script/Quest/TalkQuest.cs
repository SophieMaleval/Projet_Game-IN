using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkQuest : MonoBehaviour
{
    private QuestSys questSys;

    private void Awake()
    {
        questSys = GameObject.Find("QuestManager").GetComponent<QuestSys>();
    }

    public void TalkedTo()
    {
        questSys.Progression();
        Destroy(this.gameObject, 0.05f);
    }
}
