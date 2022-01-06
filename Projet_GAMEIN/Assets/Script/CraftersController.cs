using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftersController : MonoBehaviour
{
    public GameObject Etage1;
    public GameObject Etage0;
    public bool IsUp = false;

     void Awake() {
         IsUp = false;
        
    }
    private void Update() {
        if(IsUp == true)
        {
            //Debug.Log("en haut");
            Etage0.SetActive(false);
            Etage1.SetActive(true);
           
        }
         if(IsUp == false)
        {
          
            //Debug.Log("en bas");
            Etage0.SetActive(true);
            Etage1.SetActive(false);
        
        }
    }
    private void OnTriggerExit2D(Collider2D other) 
    {
        
          if(other.gameObject.tag == "Player")
          {
               SwitchStairs(IsUp);
          }
          


    }
    void SwitchStairs(bool StairsTaken){

        if(StairsTaken == false){
                  Debug.Log(" on prend l'escalier vers le haut");
                  IsUp =  true;
              }
        if(StairsTaken == true){
                  Debug.Log(" on prend l'escalier vers le bas");
                  IsUp = false;
    }

     }   
}
