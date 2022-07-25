using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameRequirements
{
    #region UnityInspector

    [SerializeField] public List<Requirement> requirementsList = new List<Requirement>();

    #endregion

    #region Behaviour

    public bool ExecuteGameRequirements()
    {
        if (requirementsList.Count > 0)
        {
            foreach (var item in requirementsList)
            {
                if (item.requirementType == RequirementType.HasQuest)
                {
                    if (item.CheckHasQuest(GameManager.Instance.questManager) == false)
                    {
                        //Debug.Log("false");
                        return false;
                    }
                }
                else if (item.requirementType == RequirementType.QuestState)
                {
                    if (item.CheckQuestState(GameManager.Instance.questManager) == false)
                    {
                        //Debug.Log("false");
                        return false;
                    }
                }
                else if (item.requirementType == RequirementType.HasItem)
                {
                    if (item.CheckHasItem(GameManager.Instance.player) == false)
                    {
                        //Debug.Log("false");
                        return false;
                    }
                }

            }
        }

        //Debug.Log("true");
        return true;
    }

    #endregion
}

[System.Serializable]
public class Requirement
{
    #region UnityInspector

    public RequirementType requirementType;

    [Space]
    [Header("Has Quest Properties")]
    [SerializeField] public QuestData questToCheck;
    [SerializeField] public bool questToCheckMustBeCompleted;
    [SerializeField] public bool questCheckedValueWanted;

    [Space]
    [Header("Quest State Properties")]
    [SerializeField] public QuestData questAssociatedToCheck;
    [SerializeField] public bool questAssociatedToCheckMustBeCompleted;
    [SerializeField] public QuestStepData questStepToCheck;
    [SerializeField] public bool questStepToCheckMustBeCompleted;
    [SerializeField] public bool questStepCheckedValueWanted;

    [Space]

    [Space]
    [Header("Has Item Properties")]
    [SerializeField] public InteractibleObject itemToCheck;


    #endregion

    #region Behaviour

    public bool CheckHasQuest(QuestList questList)
    {
        bool hasQuest = questList.HasQuest(questToCheck, questToCheckMustBeCompleted);
        if (hasQuest == questCheckedValueWanted)
        {
            //Debug.Log("true");
            return true;
        }
        //Debug.Log("false");
        return false;
    }

    public bool CheckQuestState(QuestList questList)
    {
        bool hasQuest = questList.HasQuest(questAssociatedToCheck, questAssociatedToCheckMustBeCompleted);
        if (hasQuest)
        {
            bool hasQuestStep = questAssociatedToCheck.HasQuestStep(questStepToCheck);
            if (hasQuestStep == questStepCheckedValueWanted)
            {
                return true;
            }
        }

        return false;
    }

    public bool CheckHasItem(PlayerScript player)
    {
        bool hasItem = player.ItemChecker(itemToCheck);
        if (hasItem)
        {
            return true;
        }
        return false;
    }

    #endregion
}

public enum RequirementType
{
    HasQuest,
    QuestState,
    HasItem,
}