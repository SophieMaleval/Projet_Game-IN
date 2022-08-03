using AllosiusDev.DialogSystem;
using AllosiusDev.QuestSystem;
using HeXa;
using System;
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

    public string textTest;
    public string keyTextTest;

    public LocalisationManager.Langage langageTest;

    public PannelENTContainer PannelENTContainerTest;


    private void Start()
    {
        if(questList == null)
        {
            questList = GameManager.Instance.questManager;
        }

        LangueManager.Instance.onLangageUpdated += TestTranslate;
    }

    [ContextMenu("TestPannelENTDescriptions")]
    public void TestPannelENTDescriptions()
    {
        List<string> keys = new List<string>();
        string temp = "";

        for (int i = 0; i < PannelENTContainerTest.descriptionsENTTranslationKeys.Length; i++)
        {
            if(temp.Length >= 1)
            {
                temp += PannelENTContainerTest.descriptionsENTTranslationKeys[i];

                if(PannelENTContainerTest.descriptionsENTTranslationKeys[i] == '}')
                {
                    temp = temp.Remove(0, 1);
                    temp = temp.Remove(temp.Length - 1, 1);
                    keys.Add(temp);
                    temp = "";
                }
            }

            if(PannelENTContainerTest.descriptionsENTTranslationKeys[i] == '{')
            {
                temp = "";
                temp += PannelENTContainerTest.descriptionsENTTranslationKeys[i];
            }

        }

        for (int i = 0; i < keys.Count; i++)
        {
            Debug.Log(keys[i]);
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


    [ContextMenu("TestTranslate")]
    public void TestTranslate()
    {
        textTest = LangueManager.Instance.Translate(keyTextTest);
    }

    [ContextMenu("TestChangeLangage")]
    public void TestChangeLangage()
    {
        LangueManager.Instance.ChangeLangage(langageTest);
    }
}
