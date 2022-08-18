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

    [SerializeField] private InteractableElement interactableElement;
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

            InventoryPanel = GameManager.Instance.gameCanvasManager.inventory.InventoryPanel ;
            TableauSpriteRenderer = transform.GetComponentInChildren<SpriteRenderer>();
            
            PannelSetUp = true ;
        }
    }


    void Update()
    {
        if(PannelSetUp)
        {
            //if(PlayerScript.gameObject.transform.position.x < transform.position.x) PlayerScript.InputSpritePos(false);
            //if(PlayerScript.gameObject.transform.position.x > transform.position.x) PlayerScript.InputSpritePos(true);
                    
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
            PlayerScript.SwitchInputSprite(transform, interactableElement.interactableSpritePosOffset);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        PlayerScript player = other.GetComponent<PlayerScript>();
        if (player != null)
        {
            PlayerArroundPannel = false;
            PlayerScript.SwitchInputSprite(transform, interactableElement.interactableSpritePosOffset);
        }
    }

    #endregion

    #region Gizmos

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.position + interactableElement.interactableSpritePosOffset, interactableElement.collisionRadius);
    }

    #endregion
}
