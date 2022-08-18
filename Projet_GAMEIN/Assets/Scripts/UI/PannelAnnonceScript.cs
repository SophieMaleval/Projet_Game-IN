﻿using AllosiusDev.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PannelAnnonceScript : MonoBehaviour
{
    #region Fields


    private SpriteRenderer TableauSpriteRenderer ;

    private PetiteAnnonceManager BoardAnnonce;
    private GameObject InventoryPanel ;

    private PlayerScript PlayerScript;
    private PlayerMovement PlayerMovement;

    private bool PlayerArroundPannel = false;

    private bool hasExecuted;

    #endregion

    #region UnityInspector

    [Header ("Information ENT")]
    public PetiteAnnonceContainer InformationsAnnonces ;
    
    public Sprite TableauPANormal;
    public Sprite TableauPAHighlighted;

    [Header ("Gestion Code")]
    [HideInInspector] public bool CanProgress = false ;

    [SerializeField] private InteractableElement interactableElement;
    [Space]

    [SerializeField] GameActions gameActions;

    #endregion

    #region Behaviour

    void Awake()
    {
        if(GameManager.Instance.player != null)
        {
            PlayerMovement = GameManager.Instance.player.GetComponent<PlayerMovement>();
            PlayerScript = GameManager.Instance.player;
            BoardAnnonce = PlayerScript.PannelAnnonceUIIndestructible.GetComponent<PetiteAnnonceManager>() ;
            InventoryPanel = GameManager.Instance.gameCanvasManager.inventory.InventoryPanel ;
            TableauSpriteRenderer = transform.GetComponentInChildren<SpriteRenderer>();
            
        }
    }


    void Update()
    {
        //if(PlayerScript.gameObject.transform.position.x < transform.position.x) PlayerScript.InputSpritePos(false);
        //if(PlayerScript.gameObject.transform.position.x > transform.position.x) PlayerScript.InputSpritePos(true);
                    
        if (PlayerArroundPannel == true)
        {
            TableauSpriteRenderer.sprite = TableauPAHighlighted ;
    
            if(PlayerScript.PlayerAsInterract && BoardAnnonce.gameObject.activeSelf == false)
            {
                PlayerScript.PlayerAsInterract = false;
                BoardAnnonce.InfoTableau = InformationsAnnonces ;
                BoardAnnonce.SwitchTogglePannelDisplay();

                if (!hasExecuted)
                {
                    Debug.Log("Execute Actions");
                    gameActions.ExecuteGameActions();
                    hasExecuted = true;
                }
            }

            if (PlayerScript.PlayerAsInterract && BoardAnnonce.gameObject.activeSelf == true && InventoryPanel.activeSelf == false)
            {
                PlayerScript.PlayerAsInterract = false;
                BoardAnnonce.SwitchTogglePannelDisplay();
            }
        } else {
            TableauSpriteRenderer.sprite = TableauPANormal ;
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

    private void OnDestroy()
    {
        //Debug.Log(gameObject.name + "is Destroy");
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
