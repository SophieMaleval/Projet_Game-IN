using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

[System.Serializable]
public class GameActions
{
    #region UnityInspector

    public List<GameAction> actionsList = new List<GameAction>();

    #endregion

    #region Class

    [System.Serializable]
    public class GameAction
    {
        #region UnityInspector

        public ActionType actionType;

        [Space]
        [Header("Add Quest Properties")]
        [SerializeField] private QuestData questToAdd;

        [Space]
        [Header("Complete Quest Step Properties")]
        [SerializeField] private QuestData questAssociated;
        [SerializeField] private QuestStepData questStepToComplete;

        [Space]

        [Space]
        [Header("Create Popup Scooter Properties")]
        [SerializeField] private Sprite ScooterPopUpImg;

        [Space]

        [Space]
        [Header("Add Item To Inventory Properties")]
        [SerializeField] private InteractibleObject itemToAdd;

        [Space]
        [Header("Remove Item To Inventory Properties")]
        [SerializeField] private InteractibleObject itemToRemove;

        #endregion

        #region Behaviour

        public Node ExecuteReturnMainNodeDialogueAction(DialogueGraph dialogueGraph)
        {
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
            GameManager.Instance.gameCanvasManager.inventory.PopUpManager.CreatePopUpForScooter(ScooterPopUpImg);
        }


        public void ExecuteAddItem()
        {
            GameManager.Instance.player.AjoutInventaire(itemToAdd);
            if (GameManager.Instance.gameCanvasManager.inventory.PopUpManager != null) GameManager.Instance.gameCanvasManager.inventory.PopUpManager.CreatePopUpItem(itemToAdd, true);
        }

        public void ExecuteRemoveItem()
        {
            GameManager.Instance.player.RemoveObject(itemToRemove);
            if (GameManager.Instance.gameCanvasManager.inventory.PopUpManager != null) GameManager.Instance.gameCanvasManager.inventory.PopUpManager.CreatePopUpItem(itemToRemove, false);
        }

        #endregion
    }

    #endregion

    #region Behaviour

    public void ExecuteGameActions()
    {
        if (actionsList.Count > 0)
        {
            DialogueTextNode newNode = null;

            foreach (var item in actionsList)
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

            }

            if(newNode != null)
            {
                GameManager.Instance.player.GetComponent<PlayerConversant>().currentNode = newNode;
            }
        }
    }

    #endregion
}

public enum ActionType
{
    ReturnMainNodeDialogue,
    AddQuest,
    CompleteQuestStep,
    CreatePopupScooter,
    AddItemToInventory,
    RemoveItemToInventory,
}
