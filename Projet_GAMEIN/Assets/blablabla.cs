using AllosiusDev.DialogSystem;
using AllosiusDev.QuestSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blablabla : MonoBehaviour
{
    public DialogueGraph dialogue;

    public QuestList questList;
    public QuestData quest;
    public QuestStepData questStep;
    public QuestTaskData questTask;

    public InteractibleObject item;


    private void Start()
    {
        if(questList == null)
        {
            questList = GameManager.Instance.questManager;
        }
    }

    [ContextMenu("TestDialog")]
    public void TestDialog()
    {
        DialogueTextNode node = (DialogueTextNode)dialogue.nodes[0];
        node.GetPorts();
    }

    [ContextMenu("TestAddQuest")]
    public void TestAddQuest()
    {
        questList.AddQuest(quest);
    }

    [ContextMenu("TestCompleteQuestStep")]
    public void TestCompleteQuestStep()
    {
        questList.CompleteQuestStep(quest, questStep);
    }

    [ContextMenu("TestCompleteQuestTask")]
    public void TestCompleteQuestTask()
    {
        questList.CompleteQuestTask(quest, questStep, questTask);
    }


    [ContextMenu("TestAddItem")]
    public void TestAddItem()
    {
        if (!GameManager.Instance.player.ItemChecker(item))
            GameManager.Instance.player.AjoutInventaire(item);
        if (GameManager.Instance.gameCanvasManager.inventory.PopUpManager != null) GameManager.Instance.gameCanvasManager.inventory.PopUpManager.CreatePopUpItem(item, true);
    }

    [ContextMenu("TestRemoveItem")]
    public void TestRemoveItem()
    {
        if (GameManager.Instance.player.ItemChecker(item))
            GameManager.Instance.player.RemoveObject(item);
        if (GameManager.Instance.gameCanvasManager.inventory.PopUpManager != null) GameManager.Instance.gameCanvasManager.inventory.PopUpManager.CreatePopUpItem(item, false);
    }
}
