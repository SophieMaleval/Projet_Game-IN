using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerGame : MonoBehaviour
{
    #region UnityInspector

    public GameObject game;

    #endregion

    #region Behaviour

    private void Awake()
    {
       //game = GameObject.Find("RythmoGamos");
       //game.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerScript player = other.GetComponent<PlayerScript>();
        if(player != null)
        {
            game.SetActive(true);
            other.gameObject.GetComponent<PlayerMovement>().StartActivity();
        }
    }

    /*private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            game.SetActive(false);
            other.gameObject.GetComponent<PlayerMovement>().EndActivity();
        }
    }*/

    #endregion
}
