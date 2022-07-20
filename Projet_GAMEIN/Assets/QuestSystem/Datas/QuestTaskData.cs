using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


    //public TASK_TYPE taskType;

    public int taskValue = 1;

    #endregion

    #region Functions

    public void updateThis(QuestTaskData newData)
    {
        _name = newData._name;
        description = newData.description;
        //taskType = newData.taskType;
        taskValue = newData.taskValue;
    }

    #endregion
}
