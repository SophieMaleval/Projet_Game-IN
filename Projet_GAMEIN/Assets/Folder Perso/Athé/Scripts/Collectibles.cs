using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectibles : MonoBehaviour
{
    inventoryController Ic;
       public QuestSystem Questep;

    // Start is called before the first frame update

    public void Start() {

        Questep = GameObject.FindGameObjectWithTag("Player").GetComponent<QuestSystem>();
        Ic = GameObject.Find("InventoryCOntroler").GetComponent<inventoryController>();


    }

    void OnTriggerEnter2D(Collider2D other) 
    {

        if(other.tag == "Player")
        {

            if(this.gameObject.name == "telephone")
            {
                Debug.Log ("yes");
                if (Questep.stepCount == 3)
                {
                     Ic.collectedObject = this.gameObject;
                    this.gameObject.SetActive(false);
                    Questep.stepCount = 4 ;
                }
            } 
            if(this.gameObject.name == "radioAQ")
            {
                if (Questep.stepCount == 7)
                {
                    Ic.collectedObject = this.gameObject;
                    Debug.Log("AA");
                    this.gameObject.SetActive(false);
                    Questep.stepCount = 8 ;
                }  
            }

              if(this.gameObject.name == "banderoles")
            {
                if (Questep.stepCount == 11)
                {
                    Ic.collectedObject = this.gameObject;
                    Debug.Log("AA");
                    this.gameObject.SetActive(false);
                    Questep.stepCount = 12 ;
                }  
            }
            if(this.gameObject.name == "hifi")
            {
                if(Questep.stepCount == 16)
                {
                    Ic.collectedObject = this.gameObject;
                    this.gameObject.SetActive(false);
                    Questep.stepCount = 17;
                }
            }
              
              
        }
    }
}
