using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SlopeManager : MonoBehaviour
{
    private PlayerMovement PM;
    public  bool TopAsLeft;
    private bool BottomAsLeft = false ;
    public float SlopeValue = 1.5f;
    private float SlopeValueNegative ;

    public GameObject ColliderBridgelevel;
    public GameObject ColliderPontDessouslevel;

     public GameObject ColliderFalaiseLevel0;
     public GameObject ColliderFalaiseLevel1;

    public  GameObject GetSortingroup;
    public int SortingLayerFLoor = -1;

   
    public SpriteRenderer spriteBridge;
 
    void Awake()
    {
        GetSortingroup = GameObject.Find("FloorNiveau2");
    
        //SortingLayerFLoor = GetComponent<SortingGroup>().sortingOrder;
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
            GetSortingroup.GetComponent<SortingGroup>().sortingOrder = 0;
            ColliderBridgelevel.SetActive(false);
            ColliderPontDessouslevel.SetActive(true);
            ColliderFalaiseLevel0.SetActive(true);
            ColliderFalaiseLevel1.SetActive(false);
            Debug.Log("niveau0");
            spriteBridge.sortingOrder = 1;

        }
        else
        {
            GetSortingroup.GetComponent<SortingGroup>().sortingOrder = -2;
            ColliderBridgelevel.SetActive(true);
            ColliderPontDessouslevel.SetActive(false);
            ColliderFalaiseLevel0.SetActive(false);
            ColliderFalaiseLevel1.SetActive(true);

            Debug.Log("niveau1");
            spriteBridge.sortingOrder = -1;

        }


    }
}
