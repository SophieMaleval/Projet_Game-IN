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

  

         void Awake() 
        {
           
            InteractingBoard = false;
           // PlayerActionControllers.PlayerInLand.Interact.performed += OnInteract;
            SpriteInput.SetActive(false); 
            Board.SetActive(false);      
        }
        void Update() 
        {
            if(InteractingBoard ==  true && PS.PlayerAsInterract)
            {
                Debug.Log("er");
                InteractWithBoard();
            }

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
            if(Board.activeInHierarchy == false)
            {
                Board.SetActive(true);
            }

            if(Board.activeInHierarchy == true)
            {
                Board.SetActive(false);
            }
           
        }







}
