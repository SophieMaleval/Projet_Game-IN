using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerGame : MonoBehaviour
{
    public GameObject game;

    private void Awake()
    {
       //game = GameObject.Find("RythmoGamos");
       //game.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
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
}
