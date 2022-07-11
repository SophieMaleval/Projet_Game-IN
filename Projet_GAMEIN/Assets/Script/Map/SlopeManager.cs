using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SlopeManager : MonoBehaviour
{
    #region Fields

    private PlayerMovement PM;

    private bool BottomAsLeft = false ;

    private float SlopeValueNegative ;

    #endregion

    #region UnityInspector

    public  bool TopAsLeft;
    public float SlopeValue = 1.5f;

    public GameObject ColliderBridgelevel;
    public GameObject ColliderPontDessouslevel;

     public GameObject ColliderFalaiseLevel0;
     public GameObject ColliderFalaiseLevel1;

    public GameObject GetSortingroup;
    public int SortingLayerFLoor = -1;

   
    public SpriteRenderer spriteBridge;

    #endregion

    #region Behaviour

    void Awake()
    {
        if(GameObject.Find("FloorNiveau2") != null) GetSortingroup = GameObject.Find("FloorNiveau2");
    
        //SortingLayerFLoor = GetComponent<SortingGroup>().sortingOrder;
        if(spriteBridge != null) spriteBridge.sortingOrder = 1;
        if(GameManager.Instance.player != null)
            PM = GameManager.Instance.player.GetComponent<PlayerMovement>();
    
        SlopeValueNegative = SlopeValue * -1 ;
        BottomAsLeft = !TopAsLeft ;
    }

   void OnTriggerEnter2D(Collider2D other) 
   {
       PlayerScript player = other.GetComponent<PlayerScript>();
       if(player != null)
        {
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
        PlayerScript player = other.GetComponent<PlayerScript>();
        if (player != null)
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
            if(GetSortingroup != null) GetSortingroup.GetComponent<SortingGroup>().sortingOrder = 0;
            if(ColliderBridgelevel != null) ColliderBridgelevel.SetActive(false);
            if(ColliderPontDessouslevel != null) ColliderPontDessouslevel.SetActive(true);
            if(ColliderFalaiseLevel0 != null) ColliderFalaiseLevel0.SetActive(true);
            if(ColliderFalaiseLevel1 != null) ColliderFalaiseLevel1.SetActive(false);

            if(spriteBridge != null) spriteBridge.sortingOrder = -1;

        }
        else
        {
            if(GetSortingroup != null) GetSortingroup.GetComponent<SortingGroup>().sortingOrder = -2;
            if(ColliderBridgelevel != null) ColliderBridgelevel.SetActive(true);
            if(ColliderPontDessouslevel != null) ColliderPontDessouslevel.SetActive(false);
            if(ColliderFalaiseLevel0 != null) ColliderFalaiseLevel0.SetActive(false);
            if(ColliderFalaiseLevel1 != null) ColliderFalaiseLevel1.SetActive(true);

            if(spriteBridge != null) spriteBridge.sortingOrder = -1;

        }


    }

    #endregion
}
