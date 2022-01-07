using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkQuest : MonoBehaviour
{
    public QuestSys questSys;
    [SerializeField] private PlayerScript PlayerScript;
    public bool playerAround;
    // Start is called before the first frame update
    private void Awake()
    {
        if (GameObject.Find("Player") != null)   // Récupère le player au lancement de la scène
        { PlayerScript = GameObject.Find("Player").GetComponent<PlayerScript>(); }
        questSys = GameObject.Find("QuestManager").GetComponent<QuestSys>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerAround)
        {
            if (PlayerScript.CanCollectObject && PlayerScript.PlayerAsInterract)
            {
                PlayerScript.PlayerAsInterract = false;
                Debug.Log("tu as cliqué mon reuf");
                TalkedTo();
            }
            else
            {
                PlayerScript.PlayerAsInterract = false;
            }
        }
    }

    void TalkedTo()
    {
        questSys.Progression();
        Destroy(this.gameObject, 0.05f);
    }

    public void PlayerCanCollectThisObject(bool Can)
    {
        if (!Can)
        {
            playerAround = false;
        }
        else
        {
            playerAround = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == ("Player"))
        {
            PlayerCanCollectThisObject(true);
            Debug.Log("i'm in");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == ("Player"))
        {
            PlayerCanCollectThisObject(false);
            Debug.Log("i'm out");
        }
    }
}
