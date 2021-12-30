using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TableauController : MonoBehaviour
{

    public GameObject Board;
    public GameObject SpriteInput;

    public bool InteractingBoard;
    public PlayerScript PS;
    PlayerMovement pM;
    public bool isReading = false;



    void Awake()
    {
        pM = PS.GetComponent<PlayerMovement>();
        Board = GameObject.Find("Board");
        InteractingBoard = false;
        // PlayerActionControllers.PlayerInLand.Interact.performed += OnInteract;
        SpriteInput.SetActive(false);
        Board.SetActive(false);
    }
    void Update()
    {
        
        if (InteractingBoard == true)
        {
            if(PS.PlayerAsInterract && Board.activeSelf == false)
            {
                PS.PlayerAsInterract = false;
                Board.SetActive(true);
                pM.StartDialog();
            }

            if (PS.PlayerAsInterract && Board.activeSelf == true)
            {
                PS.PlayerAsInterract = false;
                Board.SetActive(false);
                pM.EndDialog();
            }

        }       
    }

    public void Closed()
    {
        Board.SetActive(false);
        pM.EndDialog();
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
