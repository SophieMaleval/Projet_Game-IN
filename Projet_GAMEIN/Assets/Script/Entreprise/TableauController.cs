using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;




public class TableauController : MonoBehaviour
{
    #region Fields

    private PannelENTManager Board;
    private GameObject InventoryPanel ;
    private SpriteRenderer TableauSpriteRenderer ;

    private PlayerScript PlayerScript;
    private PlayerMovement PlayerMovement;

    [Header ("Gestion Code")]
    private bool PannelSetUp = false ;
    private bool PlayerArroundPannel = false;

    #endregion

    #region UnityInspector

    [Header ("Information ENT")]
    public PannelENTContainer InformationsPrincipaleENT ;
    public Sprite TableauENTNormal;
    public Sprite TableauENTHighlighted;
    [HideInInspector] public CSVReader RefTextENT ;

    #endregion

    #region Behaviour

    void Awake()
    {
        if(GameManager.Instance.player != null)
        {
            PlayerMovement = GameManager.Instance.player.GetComponent<PlayerMovement>();
            PlayerScript = PlayerMovement.GetComponent<PlayerScript>();
            Board = PlayerScript.PannelENTUIIndestructible.GetComponent<PannelENTManager>();
            Board.InformationENT = InformationsPrincipaleENT ;

            RefTextENT = GameManager.Instance.player.playerBackpack.GetComponent<CSVReader>() ;
            InventoryPanel = GameManager.Instance.gameCanvasManager.inventory.InventoryPanel ;
            TableauSpriteRenderer = transform.GetComponentInChildren<SpriteRenderer>();
            

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


    void Update()
    {
        if(PannelSetUp)
        {
            if(PlayerScript.gameObject.transform.position.x < transform.position.x) PlayerScript.InputSpritePos(false);
            if(PlayerScript.gameObject.transform.position.x > transform.position.x) PlayerScript.InputSpritePos(true);
                    
            if (PlayerArroundPannel == true)
            {
                TableauSpriteRenderer.sprite = TableauENTHighlighted ;

                if(PlayerScript.PlayerAsInterract && Board.gameObject.activeSelf == false)
                {
                    PlayerScript.PlayerAsInterract = false;
                    Board.SwitchTogglePannelDisplay();
                }

                if (PlayerScript.PlayerAsInterract && Board.gameObject.activeSelf == true && InventoryPanel.activeSelf == false)
                {
                    PlayerScript.PlayerAsInterract = false;
                    Board.SwitchTogglePannelDisplay();
                }
            } else {
                TableauSpriteRenderer.sprite = TableauENTNormal ;
            }              
        }


  
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerScript player = other.GetComponent<PlayerScript>();
        if (player != null)
        {
            PlayerArroundPannel = true;
            PlayerScript.SwitchInputSprite();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        PlayerScript player = other.GetComponent<PlayerScript>();
        if (player != null)
        {
            PlayerArroundPannel = false;
            PlayerScript.SwitchInputSprite();
        }
    }

    #endregion
}
