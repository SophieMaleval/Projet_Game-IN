using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using XNode;

public class PlayerConversant : MonoBehaviour
{
    #region Fields

    PlayerScript playerScript;

    DialogueGraph currentDialog;

    NpcConversant currentConversant = null;

    private bool isChoosing = false;

    #endregion

    #region Properties

    public DialogueTextNode currentNode { get; set; }

    public DialogueGraph CurrentDialog => currentDialog;

    public NpcConversant CurrentConversant => currentConversant;

    #endregion

    #region Events

    public event Action onConversationUpdated;

    #endregion

    #region Behaviour

    private void Awake()
    {
        playerScript = GetComponent<PlayerScript>();
    }

    public void StartDialog(NpcConversant conversant, DialogueGraph dialogue)
    {
        dialogue.SetStartNodes();

        playerScript.GetComponent<PlayerMovement>().StartActivity();

        playerScript.PlayerAsInterract = false;
        playerScript.InDiscussion = true;

        currentConversant = conversant;
        currentDialog = dialogue;
        //currentNode = (DialogueTextNode)currentDialog.GetRootNode();

        SetNewCurrentNode();
    }

    public bool IsActive()
    {
        return currentDialog != null;
    }

    public string GetCurrentConversantName()
    {
        return currentConversant.NamePnj;
    }

    public bool IsChoosing()
    {
        return isChoosing;
    }

    public string GetText()
    {
        if (currentNode == null)
        {
            return "";
        }

        return currentNode.message;
    }

    public IEnumerable<DialogueTextNode> GetChoices()
    {
        return currentDialog.GetPlayerChoisingChildren(currentNode);
    }

    public void SelectChoice(DialogueTextNode chosenNode)
    {
        Debug.Log("SelectChoice");
        currentNode = chosenNode;
        isChoosing = false;
        Next();
    }

    public void Next()
    {
        if(currentNode.hasGameActions)
        {
            currentNode.gameActions.ExecuteGameActions();
        }

        currentNode.SetAlreadyReadValue(true);

        if (!HasNext())
        {
            Quit();
            return;
        }

        SetNewCurrentNode();
    }

    public bool HasNext()
    {
        return currentDialog.GetAllChildren(currentNode).Count() > 0;
    }

    public void Quit()
    {
        currentDialog.ResetDialogues();

        playerScript.PlayerAsInterract = false;
        playerScript.InDiscussion = false;

        playerScript.GetComponent<PlayerMovement>().EndActivity();

        currentConversant.PNJTalkAnimation(false);

        currentDialog = null;

        currentNode = null;
        isChoosing = false;
        currentConversant = null;
        onConversationUpdated();

       
    }

    private void SetNewCurrentNode()
    {
        int numPlayerResponses = 0;

        if (currentNode == null)
        {
            numPlayerResponses = currentDialog.GetPlayerChoisingChildren().Count();
        }
        else
        {
            numPlayerResponses = currentDialog.GetPlayerChoisingChildren(currentNode).Count();
        }
        
        if (numPlayerResponses > 0)
        {
            isChoosing = true;
            onConversationUpdated();
            return;
        }

        DialogueTextNode[] children = null;

        if (currentNode == null)
        {
           children = currentDialog.GetAiChildren().ToArray();
        }
        else
        {
            children = currentDialog.GetAiChildren(currentNode).ToArray();
        }
            
        int randomIndex = UnityEngine.Random.Range(0, children.Count());
        currentNode = children[randomIndex];
        onConversationUpdated();
    }

    

    #endregion
}
