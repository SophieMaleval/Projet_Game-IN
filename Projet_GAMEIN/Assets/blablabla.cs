using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blablabla : MonoBehaviour
{
    public DialogueGraph dialogue;

    [ContextMenu("TestDialog")]
    public void TestDialog()
    {
        DialogueTextNode node = (DialogueTextNode)dialogue.nodes[0];
        node.GetPorts();


    }
}
