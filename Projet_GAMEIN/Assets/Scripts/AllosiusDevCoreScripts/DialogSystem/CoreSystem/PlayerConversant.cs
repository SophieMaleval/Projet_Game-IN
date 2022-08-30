using AllosiusDev.TranslationSystem;
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
        private bool canNext = true;

        private List<Node> _currentStartNodes = new List<Node>();

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
            return currentConversant.NpcData.nameNpc;
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

            string playerMessage = LangueManager.Instance.Translate(currentNode.keyText, TypeDictionary.Dialogues);

            string _text = playerMessage;
            if (currentNode.texteType == TexteType.Lower)
            {
                _text = playerMessage.ToLower();
            }
            else if (currentNode.texteType == TexteType.Upper)
            {
                _text = playerMessage.ToUpper();
            }

            return _text;
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

            canNext = true;

            GameManager.Instance.player.debug.transform.localScale = Vector3.one;
            GameManager.Instance.player.debug.color = Color.magenta;

            currentDialog = dialogue;
            Debug.Log(currentDialog.name);

            if (conversant != null)
            {
                currentConversant = conversant;
                Debug.Log(currentConversant.name);
                currentConversant.PNJTalkAnimation(true);

                GameManager.Instance.player.textDebug1.text = currentConversant.name + " " + currentDialog.name;
            }

            //currentNode = (DialogueTextNode)currentDialog.GetRootNode();


            //GameManager.Instance.player.textDebug1.text = currentDialog.name + " " + currentDialog.startNodes.Count + "Before Start Nodes";

            //currentDialog.SetStartNodes();
            _currentStartNodes.Clear();

            for (int i = 0; i < currentDialog.nodes.Count; i++)
            {
                Debug.Log(currentDialog.nodes[i].name);
                if (currentDialog.nodes[i].GetInputsPorts().Count == 0)
                {
                    _currentStartNodes.Add(currentDialog.nodes[i]);
                    //Debug.Log("Start Nodes Add " + currentDialog.nodes[i].name);
                }
            }

            if (_currentStartNodes.Count > 0)
            {
                GameManager.Instance.player.textDebug1.text = currentDialog.name + " " + _currentStartNodes.Count + " " + _currentStartNodes[0].name;
            }
            else
            {
                GameManager.Instance.player.debug.color = Color.yellow;
                GameManager.Instance.player.textDebug1.text = currentDialog.name + " " + _currentStartNodes.Count + "After Start Nodes";
               
            }

            SetNewCurrentNode();

            if(_currentStartNodes.Count <= 0)
            {
                currentNode = (DialogueTextNode)currentDialog.GetRootNode();
                GameManager.Instance.player.textDebug1.text = "Force Init Node " + currentNode.name;
                GameManager.Instance.player.textDebug2.text = "Force Init Node " + currentNode.name ;
            }

            playerScript.GetComponent<PlayerMovement>().StartActivity();

            playerScript.PlayerAsInterract = false;
            playerScript.InDiscussion = true;
        }

        public IEnumerable<DialogueTextNode> GetPlayerChoisingChildren()
        {
            //Debug.Log("Get Player Choicsing Children");

            foreach (DialogueTextNode node in _currentStartNodes)
            {
                //Debug.Log(node.name);

                if (node.identityType == DialogueTextNode.IdentityType.Player)
                {
                    if (node.singleRead == false)
                    {
                        yield return node;
                    }
                    else if (node.singleRead && node.GetAlreadyRead() == false)
                    {
                        yield return node;
                    }

                }
            }
        }

        public IEnumerable<DialogueTextNode> GetAiChildren()
        {
            //Debug.Log("Get AI Children");

            foreach (DialogueTextNode node in _currentStartNodes)
            {
                //Debug.Log(node.name);

                DialogueTextNode nodeChecked = currentDialog.GetRequiredNodes(node);
                if (nodeChecked != null)
                {
                    if (node.identityType == DialogueTextNode.IdentityType.NPC)
                    {
                        if (node.singleRead == false)
                        {
                            yield return node;
                        }
                        else if (node.singleRead && node.GetAlreadyRead() == false)
                        {
                            yield return node;
                        }
                    }
                }

            }
        }

        public void SelectChoice(DialogueTextNode chosenNode)
        {
            //Debug.Log("SelectChoice");
            currentNode = chosenNode;
            isChoosing = false;
            Next();
        }

        public void Next()
        {
            if(canNext)
                StartCoroutine(NextCoroutine());
        }

        public IEnumerator NextCoroutine()
        {
            Debug.Log("Next " + currentNode.name);

            canNext = false;

            if (canDialog == false)
            {
                yield break;
            }

            if (currentNode.hasGameActions)
            {
                //Debug.Log("Execute Game Actions");
                currentNode.gameActions.ExecuteGameActions();
            }

            yield return new WaitForSeconds(currentNode.timerBeforeNextNode);


            canDialog = true;

            currentNode.SetAlreadyReadValue(true);

            if (!HasNext())
            {
                //Debug.Log("Quit Dialog");
                Quit();
                yield break;
            }

            SetNewCurrentNode();
            canNext = true;

            //Debug.Log(currentNode.name);
        }

        private void SetNewCurrentNode()
        {
            //Debug.LogError("Set New Current Node");
            int numPlayerResponses = 0;

            GameManager.Instance.player.debug.color = Color.green;

            if (currentNode == null)
            {
                numPlayerResponses = GetPlayerChoisingChildren().Count();
                Debug.Log(numPlayerResponses);
            }
            else
            {
                numPlayerResponses = currentDialog.GetPlayerChoisingChildren(currentNode).Count();
                Debug.Log(numPlayerResponses);
            }

            if (numPlayerResponses > 0)
            {
                //Debug.Log("Player Choice");
                //GameManager.Instance.player.debug.transform.localScale = new Vector3(3, 3, 1);
                isChoosing = true;
                GameManager.Instance.player.textDebug2.text = "Player Choices " + numPlayerResponses;
                onConversationUpdated.Invoke();
                return;
            }

            DialogueTextNode[] children = null;

            if (currentNode == null)
            {
                //Debug.Log("Init Node");
                children = GetAiChildren().ToArray();
                Debug.Log(children[0].name);
            }
            else
            {
                //Debug.Log("New Current Node");
                children = currentDialog.GetAiChildren(currentNode).ToArray();
                Debug.Log(children[0].name);
            }

            int randomIndex = UnityEngine.Random.Range(0, children.Count());
            Debug.Log(randomIndex);

            currentNode = children[randomIndex];
            GameManager.Instance.player.textDebug2.text = currentNode.name;

            StartCoroutine(CoroutineEnterNodeActions());
        }

        private IEnumerator CoroutineEnterNodeActions()
        {
            if (currentNode.hasEnterNodeActions)
            {
                currentNode.enterNodeActions.ExecuteGameActions();
            }

            yield return new WaitForSeconds(currentNode.timerBeforeNextNode);

            onConversationUpdated.Invoke();
        }

        public void Quit()
        {
            Debug.Log("Quit Dialog");

            GameManager.Instance.player.debug.color = Color.red;
            GameManager.Instance.player.debug.transform.localScale = new Vector3(6.6f, 3.23f, 1f);
            GameManager.Instance.player.textDebug1.text = "None";
            GameManager.Instance.player.textDebug2.text = "None";

            if(currentDialog != null)
                currentDialog.ResetDialogues();

            playerScript.PlayerAsInterract = false;
            playerScript.InDiscussion = false;

            playerScript.GetComponent<PlayerMovement>().EndActivity();

            if (currentConversant != null)
            {
                currentConversant.PNJTalkAnimation(false);
                StartCoroutine(currentConversant.ResetCanTalk());
            }

            currentDialog = null;

            currentNode = null;
            isChoosing = false;
            currentConversant = null;
            onConversationUpdated.Invoke();


        }

        #endregion
    }
}
