using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AllosiusDev.QuestSystem
{
    [CreateAssetMenu(fileName = "New QuestStep", menuName = "AllosiusDev/Quests/Quest Step")]
    public class QuestStepData : ScriptableObject
    {
        #region Properties
        public enum QuestObjectiveType
        {
            Default,
            WithTasks,
        }

        #endregion

        #region UnityInspector

        public string _name;

        public string description;

        public QuestObjectiveType objectiveType;

        /*public List<RequirementsManager.RequirementDATA>
            questRequirements = new List<RequirementsManager.RequirementDATA>();*/


        [AllosiusDevDataList] public List<QuestObjectiveDATA> tasksObjectives = new List<QuestObjectiveDATA>();

        #endregion

        #region Class

        [Serializable]
        public class QuestObjectiveDATA
        {
            public QuestTaskData taskREF;
        }

        #endregion

        #region Functions

        public void updateThis(QuestStepData newItemDATA)
        {
            _name = newItemDATA._name;
            description = newItemDATA.description;
            //questRequirements = newItemDATA.questRequirements;
            tasksObjectives = newItemDATA.tasksObjectives;
        }

        public bool HasQuestTask(QuestTaskData objectiveRef)
        {
            foreach (QuestObjectiveDATA item in tasksObjectives)
            {
                if (item.taskREF == objectiveRef)
                {
                    return true;
                }
            }

            return false;
        }

        #endregion
    }
}
