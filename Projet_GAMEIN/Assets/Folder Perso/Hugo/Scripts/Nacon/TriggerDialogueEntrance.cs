using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDialogueEntrance : MonoBehaviour
{
    public GameObject dialogue;

    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Player")
        {
            dialogue.SetActive(true);
        }
    }
    void OnTriggerExit2D(Collider2D other) 
    {
        if(other.tag == "Player")
        {
            dialogue.SetActive(false);
        }
    }
}
