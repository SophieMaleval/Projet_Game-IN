using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;




public class TableauController : MonoBehaviour
{
    [Header ("Information ENT")]
    public PannelENTContainer InformationsPrincipaleENT ;
    [HideInInspector] public CSVReader RefTextENT ;    
    private PannelENTManager Board;

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
            Board = PlayerScript.PannelENTUIIndestructible.GetComponent<PannelENTManager>();
            Board.InformationENT = InformationsPrincipaleENT ;

            RefTextENT = GameObject.Find("Player Backpack").GetComponent<CSVReader>() ;
            

            // Récupérer les paragraphes de l'entreprise
            for (int NumTextENT = 0; NumTextENT < RefTextENT.TextUIPanneauxENT.Count; NumTextENT++)
            {
                if(RefTextENT.TextUIPanneauxENT[NumTextENT].NomEntrprise.Substring(0, RefTextENT.TextUIPanneauxENT[NumTextENT].NomEntrprise.Length - 3) == InformationsPrincipaleENT.NomEntreprise)
                {
                    if(RefTextENT.TextUIPanneauxENT[NumTextENT].NomEntrprise.Substring(RefTextENT.TextUIPanneauxENT[NumTextENT].NomEntrprise.Length - 2) == "FR") Board.InformationPannelENTFR = RefTextENT.TextUIPanneauxENT[NumTextENT] ;
                    if(RefTextENT.TextUIPanneauxENT[NumTextENT].NomEntrprise.Substring(RefTextENT.TextUIPanneauxENT[NumTextENT].NomEntrprise.Length - 2) == "EN") Board.InformationPannelENTEN = RefTextENT.TextUIPanneauxENT[NumTextENT] ;
                }                
            }
            
            PannelSetUp = true ;
        }
    }

    string GetLastCharactere(string StringSource, int NumberOfChars)
    {
        if(NumberOfChars >= StringSource.Length)
            return StringSource ;
        return StringSource.Substring(StringSource.Length - NumberOfChars);
    }

    void Update()
    {
        if(PannelSetUp)
        {
            if(PlayerScript.gameObject.transform.position.x < transform.position.x) PlayerScript.InputSpritePos(false);
            if(PlayerScript.gameObject.transform.position.x > transform.position.x) PlayerScript.InputSpritePos(true);
                    
            if (PlayerArroundPannel == true)
            {
                if(PlayerScript.PlayerAsInterract && Board.gameObject.activeSelf == false)
                {
                    PlayerScript.PlayerAsInterract = false;
                    Board.SwitchTogglePannelDisplay();
                    PlayerMovement.StartActivity();
                }

                if (PlayerScript.PlayerAsInterract && Board.gameObject.activeSelf == true)
                {
                    PlayerScript.PlayerAsInterract = false;
                    Board.SwitchTogglePannelDisplay();
                    PlayerMovement.EndActivity();
                }
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
