using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlopeManager : MonoBehaviour
{
    private PlayerMovement PM;
    public  bool TopAsLeft;
    private bool BottomAsLeft = false ;
    public float SlopeValue = 1.5f;
    private float SlopeValueNegative ;

    public GameObject ColliderBridgelevel;
    public GameObject ColliderFalaiselevel;

   
    public SpriteRenderer spriteBridge;
 
    void Awake()
    {
        spriteBridge.sortingOrder = 1;
        if(GameObject.Find("Player") != null)
            PM = GameObject.Find("Player").GetComponent<PlayerMovement>();
    
        SlopeValueNegative = SlopeValue * -1 ;
        BottomAsLeft = !TopAsLeft ;
    }

   void OnTriggerEnter2D(Collider2D other) 
   {
       if(other.gameObject.tag == "Player")
        {
            Debug.Log(PM.transform.position.x + " " + (transform.position.x + 1f) ); 

            if(TopAsLeft)   // Si le haut de la pente est à Gauche
            {
                if(PM.transform.position.x < (transform.position.x + 1f)) 
                {
                   PM.SlopeParameter(true, SlopeValueNegative, BottomAsLeft, 1); //  Descente
                } else {
                    PM.SlopeParameter(true, SlopeValue, BottomAsLeft, -1); // Monté
                    SwitchColliderPont(true);
                }   
           } else {
                if(PM.transform.position.x > (transform.position.x + 1f)) 
                {
                   PM.SlopeParameter(true, SlopeValueNegative, BottomAsLeft, -1); //  Monté
                   SwitchColliderPont(true);
                } else {
                   PM.SlopeParameter(true, SlopeValue, BottomAsLeft, 1); // Descente
                }   
            }            
        }
  

   }

    void OnTriggerExit2D(Collider2D other) 
   {
        if(other.gameObject.tag == "Player")
        {
           PM.SlopeParameter(false, 0f, BottomAsLeft, 0);

            if(TopAsLeft) 
            {
                if(PM.transform.position.x > (transform.position.x + 1f)) 
                {
                    SwitchColliderPont(false);
                }   
           } else {
                if(PM.transform.position.x < (transform.position.x + 1f)) 
                {
                    SwitchColliderPont(false);
                }   
            }            
        }
   }
   
    void SwitchColliderPont(bool Elevation)
    {
        if(!Elevation )
        {
            ColliderBridgelevel.SetActive(false);
            ColliderFalaiselevel.SetActive(true);
            Debug.Log("niveau0");
            spriteBridge.sortingOrder = 1;

        }
        else
        {
            ColliderBridgelevel.SetActive(true);
            ColliderFalaiselevel.SetActive(false);
            Debug.Log("niveau1");
            spriteBridge.sortingOrder = -1;

        }


    }
}
