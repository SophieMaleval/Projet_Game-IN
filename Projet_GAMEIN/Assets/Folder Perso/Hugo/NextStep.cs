using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextStep : MonoBehaviour
{
    QuestSystem qS;
    public int NewStepInt;
    public GameObject thisG;
    public GameObject player;
 

    void Start() 
    {
        qS = player.GetComponent<QuestSystem>();
    }
    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Player")
        {
            Debug.Log("Etape actuelle = " + qS.stepCount + " Etape Suivante = " + thisG.GetComponent<NextStep>().NewStepInt);
            if(qS.stepCount == thisG.GetComponent<NextStep>().NewStepInt -1)
            {
                Debug.Log("+1");
                qS.stepCount = NewStepInt;
            }
        }
    }
}
