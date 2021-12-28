using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestCt
{
    //[HideInInspector]
    public string intitule;
    [HideInInspector]
    public int questCode;
    [SerializeField] [TextArea] public string questTitle;   
    [TextArea(3, 7)] public string[] questGoal;   
}
