using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateTextAtLine : MonoBehaviour {

    public TextAsset theText;

    private int startLine;
    private int endLine;

    public DialogRange[] dialogR;

    public TextBoxManager theTextBox;

    public bool requireButtonPress;
    public bool waitForPress;

    


    public bool destroyWhenActivated;
	
	public bool txtactif;

    public int nbDialog, currentDialog;

	
	
	
	// Use this for initialization
	void Start () {
        theTextBox = FindObjectOfType<TextBoxManager>();
        currentDialog = 1;	
	}

	// Update is called once per frame
	void Update () {

        if (waitForPress && Input.GetKeyUp(KeyCode.Return) && !theTextBox.oneDialogue)
        { 
            theTextBox.oneDialogue = true;
                int i = 0;
                foreach (DialogRange dr in dialogR)
		        {
                    i ++;
                    if(i == currentDialog){
                        startLine = dr.SLine;
                        endLine = dr.ELine;
                    }

                }

            theTextBox.ReloadScript(theText);
            theTextBox.currentLine = startLine;
            theTextBox.endAtLine = endLine;
            theTextBox.EnableTextBox();
            if (destroyWhenActivated)
            {
                Destroy(gameObject);
            }
			txtactif = true;

            if(nbDialog != 1){

                currentDialog += 1;
                if(currentDialog > nbDialog){

                    currentDialog = nbDialog;
                }
            }

        }	

	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {

            if (requireButtonPress)
            {
                waitForPress = true;
                return;
            }

            theTextBox.ReloadScript(theText);
            theTextBox.currentLine = startLine;
            theTextBox.endAtLine = endLine;
            theTextBox.EnableTextBox();

            if (destroyWhenActivated)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
     if (other.name == "Player")
        {
            waitForPress = false;
			txtactif = false;
        }
    }
}
