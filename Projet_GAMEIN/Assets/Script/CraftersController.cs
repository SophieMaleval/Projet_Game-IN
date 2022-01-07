using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftersController : MonoBehaviour
{
    public GameObject Etage1;
    public GameObject Etage0;
    public bool IsUp = false;
    // public GameObject CollisionToUp;
    // public GameObject CollisionToDown;

     void Awake() {
         IsUp = false;
        
    }
    private void Update() {
        if(IsUp == true)
        {
            // CollisionToUp.SetActive(false);
            // CollisionToDown.SetActive(true);
            //Debug.Log("en haut");
            Etage0.SetActive(false);
            Etage1.SetActive(true);
           
        }
         if(IsUp == false)
        {
            // CollisionToUp.SetActive(true);
            // CollisionToDown.SetActive(false);
            //Debug.Log("en bas");
            Etage0.SetActive(true);
            Etage1.SetActive(false);
        
        }
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        
          if(other.gameObject.tag == "Player")
          {
               SwitchStairs(IsUp);
          }
          


    }
    void SwitchStairs(bool StairsTaken){

        if(StairsTaken == false){
                  Debug.Log(" on prend l'escalier vers le haut");
                  this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                  this.gameObject.GetComponent<CircleCollider2D>().enabled = true;
                  IsUp =  true;
              }
        if(StairsTaken == true){
                  Debug.Log(" on prend l'escalier vers le bas");
                  this.gameObject.GetComponent<BoxCollider2D>().enabled = true;
                  this.gameObject.GetComponent<CircleCollider2D>().enabled = false;
                  IsUp = false;
    }

     }   
}
