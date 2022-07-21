using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameRequirements
{
    #region UnityInspector

    public List<Requirement> requirementsList = new List<Requirement>();

    #endregion

    #region Class

    [System.Serializable]
    public class Requirement
    {
        #region UnityInspector

        public RequirementType requirementType;

        [Space]
        [Header("Has Quest Properties")]
        [SerializeField] private QuestData questToCheck;
        [SerializeField] private bool questCheckedValueWanted;

        [Space]
        [Header("Quest State Properties")]
        [SerializeField] private QuestData questAssociatedToCheck;
        [SerializeField] private QuestStepData questStepToChecked;
        [SerializeField] private bool questStepCheckedValueWanted;

        [Space]

        [Space]
        [Header("Has Item Properties")]
        [SerializeField] private InteractibleObject itemToCheck;


        #endregion

        #region Behaviour

        public bool CheckHasQuest(QuestList questList)
        {
            bool hasQuest = questList.HasQuest(questToCheck);
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
            bool hasQuest = questList.HasQuest(questAssociatedToCheck);
            if (hasQuest)
            {
                bool hasQuestStep = questAssociatedToCheck.HasQuestStep(questStepToChecked);
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
            if(hasItem)
            {
                return true;
            }
            return false;
        }

        #endregion
    }

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

public enum RequirementType
{
    HasQuest,
    QuestState,
    HasItem,
}