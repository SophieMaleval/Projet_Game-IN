using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcConversant : MonoBehaviour
{

    #region Fields

    private bool canTalk = true;

    private bool PlayerAround = false;

    #endregion

    #region Properties

    public string NamePnj => namePNJ;

    #endregion

    #region UnityInspector

    [Header("PNJ Information")]

    [SerializeField] private string namePNJ;
    [SerializeField] private int pnjInENT;

    [SerializeField] private DialogueGraph npcDialogue;

    #endregion

    #region Behaviour

    private void Awake()
    {
        InitAnimator();
    }

    private void Update()
    {
        if (PlayerAround && !GameManager.Instance.player.QCMPanelUIIndestructible.activeSelf)
        {
            if (GameManager.Instance.player.PlayerAsInterract && !GameManager.Instance.player.InDiscussion)
            {
                Debug.Log("Launch Discussion");
                StartDialog();
            }
        }
    }

    public void InitAnimator()
    {
        GetComponent<Animator>().SetInteger("PNJ Need", pnjInENT);
    }

    public void StartDialog()
    {
        if(npcDialogue == null || canTalk == false)
        {
            return;
        }

        GameManager.Instance.player.GetComponent<PlayerConversant>().StartDialog(this, npcDialogue);

        PNJTalkAnimation(true);
    }

    public void PNJTalkAnimation(bool IsTalking)
    {
        GetComponent<Animator>().SetBool("Talk", IsTalking);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerScript player = other.GetComponent<PlayerScript>();
        if (player != null)
        {
            PlayerAround = true;
            GameManager.Instance.player.SwitchInputSprite();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        PlayerScript player = other.GetComponent<PlayerScript>();
        if (player != null)
        {
            PlayerAround = false;
            GameManager.Instance.player.SwitchInputSprite();
        }
    }

    #endregion
}
