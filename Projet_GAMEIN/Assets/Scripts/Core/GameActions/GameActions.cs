using AllosiusDev.DialogSystem;
using AllosiusDev.QuestSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace AllosiusDev.Core
{
    [System.Serializable]
    public class GameActions
    {
        #region UnityInspector

        [SerializeField] public List<GameAction> actionsList = new List<GameAction>();

        #endregion

        #region Behaviour

        public void ExecuteGameActions()
        {
            if (actionsList.Count > 0)
            {
                DialogueTextNode newNode = null;

                foreach (var item in actionsList)
                {
                    if (item.CheckCondition())
                    {
                        if (item.actionType == ActionType.ReturnMainNodeDialogue)
                        {
                            newNode = (DialogueTextNode)item.ExecuteReturnMainNodeDialogueAction(GameManager.Instance.player.GetComponent<PlayerConversant>().CurrentDialog);
                        }
                        else if (item.actionType == ActionType.AddQuest)
                        {
                            item.ExecuteAddQuest(GameManager.Instance.questManager);
                        }
                        else if (item.actionType == ActionType.CompleteQuestStep)
                        {
                            item.ExecuteCompleteQuestStep(GameManager.Instance.questManager);
                        }
                        else if (item.actionType == ActionType.CreatePopupScooter)
                        {
                            item.ExecuteCreatePopUpScooter();
                        }
                        else if (item.actionType == ActionType.AddItemToInventory)
                        {
                            item.ExecuteAddItem();
                        }
                        else if (item.actionType == ActionType.RemoveItemToInventory)
                        {
                            item.ExecuteRemoveItem();
                        }
                        else if (item.actionType == ActionType.LaunchMiniGame)
                        {
                            item.ExecuteLaunchMiniGame();
                        }
                        else if (item.actionType == ActionType.LaunchFade)
                        {
                            item.ExecuteLaunchFade();
                        }
                        else if (item.actionType == ActionType.LaunchDialogue)
                        {
                            item.ExecuteLaunchDialogue();
                        }
                    }
                }

                if (newNode != null)
                {
                    GameManager.Instance.player.GetComponent<PlayerConversant>().currentNode = newNode;
                }
            }
        }

        #endregion
    }
}

namespace AllosiusDev.Core
{
    [System.Serializable]
    public class GameAction
    {
        #region UnityInspector

        [Header("Generals Properties")]
        [SerializeField] public ActionType actionType;

        [SerializeField] public bool hasCondition;
        [SerializeField] public List<DialogueTextNode> nodesRequiredToRead = new List<DialogueTextNode>();

        [Space]
        [Header("Add Quest Properties")]
        [SerializeField] public QuestData questToAdd;

        [Space]
        [Header("Complete Quest Step Properties")]
        [SerializeField] public QuestData questAssociated;
        [SerializeField] public QuestStepData questStepToComplete;

        [Space]

        [Space]
        [Header("Create Popup Scooter Properties")]
        [SerializeField] public PopUpData ScooterPopUpData;

        [Space]

        [Space]
        [Header("Add Item To Inventory Properties")]
        [SerializeField] public InteractibleObject itemToAdd;

        [Space]
        [Header("Remove Item To Inventory Properties")]
        [SerializeField] public InteractibleObject itemToRemove;

        [Space]

        [Space]
        [Header("Launch Fade Properties")]
        [SerializeField] public float fadeDuration = 0.5f;
        [SerializeField] public float fadeOutSwitchDuration = 1f;

        [Space]

        [Space]
        [Header("Launch Dialogue Properties")]
        [SerializeField] public NpcConversant newConversantToCall;
        [SerializeField] public DialogueGraph dialogueToLaunch;
        [SerializeField] public bool launchDialogueToMainNode;

        #endregion

        #region Behaviour

        public bool CheckCondition()
        {
            if (hasCondition)
            {
                if (nodesRequiredToRead.Count > 0)
                {
                    for (int i = 0; i < nodesRequiredToRead.Count; i++)
                    {
                        if (nodesRequiredToRead[i].GetAlreadyRead() == false)
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        public Node ExecuteReturnMainNodeDialogueAction(DialogueGraph dialogueGraph)
        {
            GameManager.Instance.player.GetComponent<PlayerConversant>().currentNode.SetAlreadyReadValue(true);
            return dialogueGraph.mainNodeParent;
        }

        public void ExecuteAddQuest(QuestList questList)
        {
            questList.AddQuest(questToAdd);
        }

        public void ExecuteCompleteQuestStep(QuestList questList)
        {
            questList.CompleteQuestStep(questAssociated, questStepToComplete);
        }


        public void ExecuteCreatePopUpScooter()
        {
            GameManager.Instance.gameCanvasManager.inventory.PopUpManager.CreatePopUpForScooter(ScooterPopUpData);
        }


        public void ExecuteAddItem()
        {
            if (!GameManager.Instance.player.ItemChecker(itemToAdd))
                GameManager.Instance.player.AjoutInventaire(itemToAdd);
            if (GameManager.Instance.gameCanvasManager.inventory.PopUpManager != null) GameManager.Instance.gameCanvasManager.inventory.PopUpManager.CreatePopUpItem(itemToAdd, true);
        }

        public void ExecuteRemoveItem()
        {
            if (GameManager.Instance.player.ItemChecker(itemToRemove))
                GameManager.Instance.player.RemoveObject(itemToRemove);
            if (GameManager.Instance.gameCanvasManager.inventory.PopUpManager != null) GameManager.Instance.gameCanvasManager.inventory.PopUpManager.CreatePopUpItem(itemToRemove, false);
        }

        public void ExecuteLaunchMiniGame()
        {
            GameCore.Instance.OpenMinigame();
        }

        public void ExecuteLaunchFade()
        {
            GameManager.Instance.player.LaunchFade(fadeDuration, fadeOutSwitchDuration);
            GameManager.Instance.player.GetComponent<PlayerConversant>().currentNode.timerBeforeNextNode = fadeDuration * 2 + fadeOutSwitchDuration;
            GameManager.Instance.player.GetComponent<PlayerConversant>().canDialog = false;
        }

        public void ExecuteLaunchDialogue()
        {
            NpcConversant conversant = null;
            if (newConversantToCall != null)
            {
                conversant = newConversantToCall;
            }
            if (conversant == null)
            {
                conversant = GameManager.Instance.player.GetComponent<PlayerConversant>().CurrentConversant;
            }

            if (GameManager.Instance.player.GetComponent<PlayerConversant>().CurrentDialog != null)
            {
                GameManager.Instance.player.GetComponent<PlayerConversant>().Quit();
            }
            GameManager.Instance.player.GetComponent<PlayerConversant>().StartDialog(conversant, dialogueToLaunch);

            if(launchDialogueToMainNode)
            {
                DialogueTextNode newNode = (DialogueTextNode)ExecuteReturnMainNodeDialogueAction(GameManager.Instance.player.GetComponent<PlayerConversant>().CurrentDialog);

                if (newNode != null)
                {
                    GameManager.Instance.player.GetComponent<PlayerConversant>().currentNode = newNode;
                    GameManager.Instance.player.GetComponent<PlayerConversant>().Next();
                }
            }
        }

        #endregion
    }
}

namespace AllosiusDev.Core
{
    public enum ActionType
    {
        ReturnMainNodeDialogue,
        AddQuest,
        CompleteQuestStep,
        CreatePopupScooter,
        AddItemToInventory,
        RemoveItemToInventory,
        LaunchMiniGame,
        LaunchFade,
        LaunchDialogue,
    }
}
