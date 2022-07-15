using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blablabla : MonoBehaviour
{
    public Dialogue dialogue;

    [ContextMenu("TestDialog")]
    public void TestDialog()
    {
        DialogueTextNode node = (DialogueTextNode)dialogue.dialogueGraph.nodes[0];
        node.GetPorts();


    }
}
