using System.Collections;
using System.Collections.Generic;
//using BLINK.RPGBuilder.Logic;
//using BLINK.RPGBuilder.Managers;
using UnityEngine;
using XNode;

[NodeWidth(360)]
[NodeTint(46, 46, 46)]
[CreateNodeMenu("Dialogue Text Node")]
public class DialogueTextNode : Node
{
    #region Fields

    private bool alreadyRead;

    #endregion

    #region UnityInspector

    [Input] public DialogueTextNode previousNode;
	[Output] public DialogueBaseNode nextNodes;

	public enum IdentityType { NPC, Player }
	[NodeEnum] public IdentityType identityType;

	//public string nodeName = "";
	[TextArea]
	public string message = "";

    public bool singleRead;

	public bool hasGameActions;
	public List<GameAction> GameActionsList = new List<GameAction>();
	
	public bool hasRequirements;
    //public List<RequirementsManager.RequirementDATA> RequirementList = new List<RequirementsManager.RequirementDATA>();

    //public bool showSettings;
    //public bool viewedEndless = true, clickedEndless = true;
    //public int viewCountMax, clickCountMax;

    //public bool editorInitialized;

    //public Sprite nodeImage;

    #endregion

    #region Functions

    public bool GetAlreadyRead()
    {
        return alreadyRead;
    }

    public void SetAlreadyReadValue(bool value)
    {
        alreadyRead = value;
    }

    public List<Node> GetOutputsPorts()
    {
        List<Node> nodesChildren = new List<Node>();

        foreach (var item in Ports)
        {
            if(item.direction == NodePort.IO.Output)
            {
                //Debug.Log(item.fieldName + " ");
                for (int i = 0; i < item.ConnectionCount; i++)
                {
                    //Debug.Log(item.GetConnection(i).fieldName + " " + item.GetConnection(i).node.name);
                    nodesChildren.Add(item.GetConnection(i).node);
                }
            }
            
        }

        return nodesChildren;
    }

	public void GetPorts()
    {
		foreach (var item in Ports)
        {
			//Debug.Log(item.fieldName + " ");
            for (int i = 0; i < item.ConnectionCount; i++)
            {
                //Debug.Log(item.GetConnection(i).fieldName + " " + item.GetConnection(i).node.name);
            }
        }
    }

    // GetValue should be overridden to return a value for any specified output port
    public override object GetValue(NodePort port) 
	{

		// Get new a and b values from input connections. Fallback to field values if input is not connected
		DialogueTextNode previousNode = GetInputValue<DialogueTextNode>("previousNode", this.previousNode);

		// After you've gotten your input values, you can perform your calculations and return a value
		return previousNode;
	}

    #endregion
}