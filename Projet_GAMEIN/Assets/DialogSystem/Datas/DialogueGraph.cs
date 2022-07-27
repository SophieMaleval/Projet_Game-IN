using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using XNode;

namespace AllosiusDev.DialogSystem
{
    [CreateAssetMenu(fileName = "New DialogueGraph", menuName = "AllosiusDev/Dialogue Graph")]
    public class DialogueGraph : NodeGraph
    {

        [Space]

        public string displayName;
        public string description;

        [Space]

        public Node mainNodeParent;

        public void updateThis(DialogueGraph newData)
        {
            description = newData.description;
            displayName = newData.displayName;

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

        public DialogueTextNode GetRequiredNodes(DialogueTextNode node)
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
}