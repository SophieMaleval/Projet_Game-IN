using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestCt
{
    public int questCode;
    [SerializeField] [TextArea] public string questTitle;   
    [TextArea(3, 7)] public string[] questGoal;   
}
