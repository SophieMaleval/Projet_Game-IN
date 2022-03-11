using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PannelAnnonceScript : MonoBehaviour
{
    [Header ("Information ENT")]
    public PannelENTContainer InformationsPrincipaleENT ;
    [HideInInspector] public CSVReader RefTextENT ;    
    private GameObject Board;
    private GameObject InventoryPanel ;

    private PlayerScript PlayerScript;
    private PlayerMovement PlayerMovement;

    [Header ("Gestion Code")]
    private bool PannelSetUp = false ;
    private bool PlayerArroundPannel = false;    


    void Awake()
    {
        if(GameObject.Find("Player") != null)
        {
            PlayerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
            PlayerScript = PlayerMovement.GetComponent<PlayerScript>();

            InventoryPanel = GameObject.Find("Inventory").GetComponent<InventoryScript>().InventoryPanel ;
            
            
        }
    }


    void Update()
    {
        if(PlayerScript.gameObject.transform.position.x < transform.position.x) PlayerScript.InputSpritePos(false);
        if(PlayerScript.gameObject.transform.position.x > transform.position.x) PlayerScript.InputSpritePos(true);
                    
        if (PlayerArroundPannel == true)
        {
            if(PlayerScript.PlayerAsInterract && Board.gameObject.activeSelf == false)
            {
                PlayerScript.PlayerAsInterract = false;
                //Board.SwitchTogglePannelDisplay();
            }

            if (PlayerScript.PlayerAsInterract && Board.gameObject.activeSelf == true && InventoryPanel.activeSelf == false)
            {
                PlayerScript.PlayerAsInterract = false;
                //Board.SwitchTogglePannelDisplay();
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
