using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using XNode;

namespace AllosiusDev.DialogSystem
{
    public class PlayerConversant : MonoBehaviour
    {
        #region Fields

        PlayerScript playerScript;

        DialogueGraph currentDialog;

        NpcConversant currentConversant = null;

        private bool isChoosing = false;

        #endregion

        #region Properties

        public bool canDialog { get; set; }

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

            canDialog = true;
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
        public bool HasNext()
        {
            return currentDialog.GetAllChildren(currentNode).Count() > 0;
        }


        public void StartDialog(NpcConversant conversant, DialogueGraph dialogue)
        {
            //Debug.LogError("Start Dialog");

            dialogue.SetStartNodes();

            playerScript.GetComponent<PlayerMovement>().StartActivity();

            playerScript.PlayerAsInterract = false;
            playerScript.InDiscussion = true;

            currentConversant = conversant;
            //Debug.Log(currentConversant.name);
            currentDialog = dialogue;
            //Debug.Log(currentDialog.name);
            //currentNode = (DialogueTextNode)currentDialog.GetRootNode();

            SetNewCurrentNode();
        }


        public void SelectChoice(DialogueTextNode chosenNode)
        {
            //Debug.Log("SelectChoice");
            currentNode = chosenNode;
            isChoosing = false;
            StartCoroutine(Next());
        }

        public IEnumerator Next()
        {
            //Debug.LogError("Next " + currentNode.name);

            if (canDialog == false)
            {
                yield break;
            }

            if (currentNode.hasGameActions)
            {
                //Debug.LogError("Execute Game Actions");
                currentNode.gameActions.ExecuteGameActions();
            }

            yield return new WaitForSeconds(currentNode.timerBeforeNextNode);

            canDialog = true;

            currentNode.SetAlreadyReadValue(true);

            if (!HasNext())
            {
                //Debug.LogError("Quit Dialog");
                Quit();
                yield break;
            }

            SetNewCurrentNode();

            //Debug.LogError(currentNode.name);
        }

        private void SetNewCurrentNode()
        {
            //Debug.LogError("Set New Current Node");
            int numPlayerResponses = 0;

            if (currentNode == null)
            {
                numPlayerResponses = currentDialog.GetPlayerChoisingChildren().Count();
                Debug.Log(numPlayerResponses);
            }
            else
            {
                numPlayerResponses = currentDialog.GetPlayerChoisingChildren(currentNode).Count();
                Debug.Log(numPlayerResponses);
            }

            if (numPlayerResponses > 0)
            {
                Debug.Log("Player Choice");
                isChoosing = true;
                onConversationUpdated();
                return;
            }

            DialogueTextNode[] children = null;

            if (currentNode == null)
            {
                Debug.Log("Init Node");
                children = currentDialog.GetAiChildren().ToArray();
                Debug.Log(children[0].name);
            }
            else
            {
                Debug.Log("New Current Node");
                children = currentDialog.GetAiChildren(currentNode).ToArray();
                Debug.Log(children[0].name);
            }

            int randomIndex = UnityEngine.Random.Range(0, children.Count());
            Debug.Log(randomIndex);
            currentNode = children[randomIndex];
            onConversationUpdated();
        }

        public void Quit()
        {
            //Debug.Log("Quit Dialog");

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

        #endregion
    }
}
