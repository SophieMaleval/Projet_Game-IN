using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TableauController : MonoBehaviour
{

    public GameObject Board;
    private GameObject SpriteInput;

    public bool InteractingBoard;
    private PlayerScript PS;
    private PlayerMovement PM;
    public bool isReading = false;



    void Awake()
    {
        if(GameObject.Find("Player") != null)
        {
            PM = GameObject.Find("Player").GetComponent<PlayerMovement>();
            PS = PM.GetComponent<PlayerScript>();

            SpriteInput = PS.InterractInputSprite ;

            Board = GameObject.Find("Board");
            InteractingBoard = false;
            // PlayerActionControllers.PlayerInLand.Interact.performed += OnInteract;
            SpriteInput.SetActive(false);
            Board.SetActive(false);            
        }

    }
    void Update()
    {
        
        if (InteractingBoard == true)
        {
            if(PS.PlayerAsInterract && Board.activeSelf == false)
            {
                PS.PlayerAsInterract = false;
                Board.SetActive(true);
                PM.StartActivity();
            }

            if (PS.PlayerAsInterract && Board.activeSelf == true)
            {
                PS.PlayerAsInterract = false;
                Board.SetActive(false);
                PM.EndActivity();
            }
        }       
    }

    public void Closed()
    {
        Board.SetActive(false);
        PM.EndActivity();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == ("Player"))
        {
            InteractingBoard = true;
            SpriteInput.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == ("Player"))
        {
            InteractingBoard = false;
            SpriteInput.SetActive(false);
        }

    }

    public void InteractWithBoard()
    {

        if (isReading)
        {
            Board.SetActive(true);
        }
        else if (!isReading)
        {
            Board.SetActive(false);
        }
    }
}
