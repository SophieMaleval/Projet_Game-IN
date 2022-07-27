using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AllosiusDev.QuestSystem
{
    [CreateAssetMenu(fileName = "New QuestTask", menuName = "AllosiusDev/Quests/Quest Task")]
    public class QuestTaskData : ScriptableObject
    {
        #region Properties
        public enum TASK_TYPE
        {
            talkToNPC,
        }

        #endregion

        #region UnityInspector

        public string _name;

        public string description;

        public int taskValue = 1;

        #endregion

        #region Functions

        public void updateThis(QuestTaskData newData)
        {
            _name = newData._name;
            description = newData.description;
            taskValue = newData.taskValue;
        }

        #endregion
    }
}