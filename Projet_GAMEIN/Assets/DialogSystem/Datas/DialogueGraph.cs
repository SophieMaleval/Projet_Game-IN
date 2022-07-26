using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using XNode;

[CreateAssetMenu(fileName = "New DialogueGraph", menuName = "AllosiusDev/Dialogue Graph")]
public class DialogueGraph : NodeGraph {

    [Space]

    //public int ID = -1;
    //public string _name;
    //public string _fileName;
    public string displayName;
    public string description;

    //public bool hasExitNode;
    //public string exitNodeText = "Goodbye";

    [Space]

    public Node mainNodeParent;

    public void updateThis(DialogueGraph newData)
    {
        //ID = newData.ID;
        //_name = newData._name;
        //_fileName = newData._fileName;
        description = newData.description;
        displayName = newData.displayName;
        //hasExitNode = newData.hasExitNode;
        //exitNodeText = newData.exitNodeText;

        mainNodeParent = newData.mainNodeParent;
    }

    public void ResetDialogues()
    {
        foreach (var item in nodes)
        {
            DialogueTextNode node = (DialogueTextNode)item;
            node.SetAlreadyReadValue(false);
            node.timerBeforeNextNode = 0.0f;
        }
    }

    public DialogueTextNode GetRequiredNodes (DialogueTextNode node)
    {
        if (node.hasRequirements)
        {
            if (node.gameRequirements.ExecuteGameRequirements())
                 return node;
        }
        else
        {
             return node;
        }

        return null;
    }

    public IEnumerable<DialogueTextNode> GetAllChildren(DialogueTextNode parentNode)
    {
        foreach (DialogueTextNode child in parentNode.GetOutputsPorts())
        {
            if (child.hasRequirements)
            {
                if (child.gameRequirements.ExecuteGameRequirements())
                    yield return child;
            }
            else
            {
                yield return child;
            }
        }
    }

    public IEnumerable<DialogueTextNode> GetPlayerChoisingChildren()
    {
        foreach (DialogueTextNode node in startNodes)
        {
            if (node.identityType == DialogueTextNode.IdentityType.Player)
            {
                if (node.singleRead == false)
                {
                    yield return node;
                }
                else if (node.singleRead && node.GetAlreadyRead() == false)
                {
                    yield return node;
                }

            }
        }
    }

    public IEnumerable<DialogueTextNode> GetPlayerChoisingChildren(DialogueTextNode currentNode)
    {
        foreach (DialogueTextNode node in GetAllChildren(currentNode))
        {
            if (node.identityType == DialogueTextNode.IdentityType.Player)
            {
                if(node.singleRead == false)
                {
                    yield return node;
                }
                else if(node.singleRead && node.GetAlreadyRead() == false)
                {
                    yield return node;
                }
                
            }
        }
    }

    public IEnumerable<DialogueTextNode> GetAiChildren()
    {
        foreach (DialogueTextNode node in startNodes)
        {
            DialogueTextNode nodeChecked = GetRequiredNodes(node);
            if (nodeChecked != null)
            {
                if (node.identityType == DialogueTextNode.IdentityType.NPC)
                {
                    if (node.singleRead == false)
                    {
                        yield return node;
                    }
                    else if (node.singleRead && node.GetAlreadyRead() == false)
                    {
                        yield return node;
                    }
                }
            }

        }
    }

    public IEnumerable<DialogueTextNode> GetAiChildren(DialogueTextNode currentNode)
    {
        foreach (DialogueTextNode node in GetAllChildren(currentNode))
        {
            if (node.identityType == DialogueTextNode.IdentityType.NPC)
            {
                if (node.singleRead == false)
                {
                    yield return node;
                }
                else if (node.singleRead && node.GetAlreadyRead() == false)
                {
                    yield return node;
                }
            }
        }
    }

    

}