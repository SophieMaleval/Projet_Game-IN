using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

[System.Serializable]
public class GameAction
{
    public ActionType actionType;

    public Node ExecuteReturnMainNodeDialogueAction(DialogueGraph dialogueGraph)
    {
        return dialogueGraph.mainNodeParent;
    }
}

public enum ActionType
{
    ReturnMainNodeDialogue,
}
