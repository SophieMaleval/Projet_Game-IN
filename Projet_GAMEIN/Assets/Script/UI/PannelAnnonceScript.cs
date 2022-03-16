using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PannelAnnonceScript : MonoBehaviour
{
    [Header ("Information ENT")]
    public PetiteAnnonceContainer InformationsAnnonces ;
    [HideInInspector] public CSVReader RefTextENT ;    
    private PetiteAnnonceManager BoardAnnonce;
    private GameObject InventoryPanel ;

    private PlayerScript PlayerScript;
    private PlayerMovement PlayerMovement;

    [Header ("Gestion Code")]
    private bool PannelSetUp = false ;
    private bool PlayerArroundPannel = false;    
    [HideInInspector] public bool CanProgress = false ;


    void Awake()
    {
        if(GameObject.Find("Player") != null)
        {
            PlayerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
            PlayerScript = PlayerMovement.GetComponent<PlayerScript>();
            BoardAnnonce = PlayerScript.PannelAnnonceUIIndestructible.GetComponent<PetiteAnnonceManager>() ;
            InventoryPanel = GameObject.Find("Inventory").GetComponent<InventoryScript>().InventoryPanel ;
            
            
        }
    }


    void Update()
    {
        if(PlayerScript.gameObject.transform.position.x < transform.position.x) PlayerScript.InputSpritePos(false);
        if(PlayerScript.gameObject.transform.position.x > transform.position.x) PlayerScript.InputSpritePos(true);
                    
        if (PlayerArroundPannel == true)
        {
            if(PlayerScript.PlayerAsInterract && BoardAnnonce.gameObject.activeSelf == false)
            {
                PlayerScript.PlayerAsInterract = false;
                BoardAnnonce.InfoTableau = InformationsAnnonces ;
                BoardAnnonce.SwitchTogglePannelDisplay();

                if(CanProgress) GameObject.Find("QuestManager").GetComponent<QuestSys>().Progression() ;
            }

            if (PlayerScript.PlayerAsInterract && BoardAnnonce.gameObject.activeSelf == true && InventoryPanel.activeSelf == false)
            {
                PlayerScript.PlayerAsInterract = false;
                BoardAnnonce.SwitchTogglePannelDisplay();
            }
        }                 
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == ("Player"))
        {
            PlayerArroundPannel = true;
            PlayerScript.SwitchInputSprite();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == ("Player"))
        {
            PlayerArroundPannel = false;
            PlayerScript.SwitchInputSprite();
        }
    }
}
