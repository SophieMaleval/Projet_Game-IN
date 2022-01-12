using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TableauController : MonoBehaviour
{

    private PannelENTManager Board;

    public bool PlayerArroundPannel = false;
    private PlayerScript PlayerScript;
    private PlayerMovement PlayerMovement;
    public bool isReading = false;



    void Awake()
    {
        if(GameObject.Find("Player") != null)
        {
            PlayerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
            PlayerScript = PlayerMovement.GetComponent<PlayerScript>();

            Board = GameObject.Find("Board").GetComponent<PannelENTManager>();
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

    private void OnTriggerStay2D(Collider2D other)
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
