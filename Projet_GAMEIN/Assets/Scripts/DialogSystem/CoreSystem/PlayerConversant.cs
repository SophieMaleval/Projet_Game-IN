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

        public string GetKeyText()
        {
            if (currentNode == null)
            {
                return "";
            }

            string playerMessage = LangueManager.Instance.Translate(currentNode.keyText);

            return playerMessage;
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
            Debug.Log("Start Dialog");

            GameManager.Instance.player.debug.transform.localScale = Vector3.one;

            dialogue.SetStartNodes();

            playerScript.GetComponent<PlayerMovement>().StartActivity();

            playerScript.PlayerAsInterract = false;
            playerScript.InDiscussion = true;

            currentConversant = conversant;
            Debug.Log(currentConversant.name);
            currentDialog = dialogue;
            Debug.Log(currentDialog.name);
            //currentNode = (DialogueTextNode)currentDialog.GetRootNode();

            GameManager.Instance.player.textDebug1.text = currentConversant.name + " " + currentDialog.name;

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
            Debug.LogError("Next " + currentNode.name);

            if (canDialog == false)
            {
                yield break;
            }

            if (currentNode.hasGameActions)
            {
                Debug.Log("Execute Game Actions");
                currentNode.gameActions.ExecuteGameActions();
            }

            yield return new WaitForSeconds(currentNode.timerBeforeNextNode);

            canDialog = true;

            currentNode.SetAlreadyReadValue(true);

            if (!HasNext())
            {
                Debug.Log("Quit Dialog");
                Quit();
                yield break;
            }

            SetNewCurrentNode();

            Debug.Log(currentNode.name);
        }

        private void SetNewCurrentNode()
        {
            Debug.LogError("Set New Current Node");
            int numPlayerResponses = 0;

            GameManager.Instance.player.debug.color = Color.green;

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
                GameManager.Instance.player.debug.transform.localScale = new Vector3(3, 3, 1);
                isChoosing = true;
                onConversationUpdated.Invoke();
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
            onConversationUpdated.Invoke();
        }

        public void Quit()
        {
            Debug.Log("Quit Dialog");

            GameManager.Instance.player.debug.color = Color.red;
            GameManager.Instance.player.debug.transform.localScale = new Vector3(6.6f, 3.23f, 1f);
            GameManager.Instance.player.textDebug1.text = "None";
            GameManager.Instance.player.textDebug2.text = "None";

            currentDialog.ResetDialogues();

            playerScript.PlayerAsInterract = false;
            playerScript.InDiscussion = false;

            playerScript.GetComponent<PlayerMovement>().EndActivity();

            currentConversant.PNJTalkAnimation(false);

            currentDialog = null;

            currentNode = null;
            isChoosing = false;
            currentConversant = null;
            onConversationUpdated.Invoke();


        }

        #endregion
    }
}
