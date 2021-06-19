using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextStep : MonoBehaviour
{
    QuestSystem qS;
    public int NewStepInt;
    GameObject thisG;
 

    void Start() 
    {
        qS = GameObject.Find("Player").GetComponent<QuestSystem>();
        thisG = this.gameObject;
    }
    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Player")
        {
            /*if(qS.stepCount == NewStepInt -1)
            {
                qS.stepCount = NewStepInt;
            }*/
            if(qS.stepCount < thisG.GetComponent<NextStep>().NewStepInt)
            {
                Debug.Log("marche et ajoute 1");
                qS.stepCount ++;
            } else { Debug.Log(thisG.GetComponent<NextStep>().NewStepInt);}
            
        }
    }
}
