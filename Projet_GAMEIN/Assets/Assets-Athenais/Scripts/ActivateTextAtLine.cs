using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateTextAtLine : MonoBehaviour {

    public TextAsset theText;

    public int startLine;
    public int endLine;

    public TextBoxManager theTextBox;

    public bool requireButtonPress;
    public bool waitForPress;


    public bool destroyWhenActivated;
	
	public bool txtactif ;

	
	
	
	// Use this for initialization
	void Start () {
        theTextBox = FindObjectOfType<TextBoxManager>();	
	}

	// Update is called once per frame
	void Update () {

        if (waitForPress && Input.GetKeyUp(KeyCode.Return))
        {
            theTextBox.ReloadScript(theText);
            theTextBox.currentLine = startLine;
            theTextBox.endAtLine = endLine;
            theTextBox.EnableTextBox();
            if (destroyWhenActivated)
            {
                Destroy(gameObject);
            }
			txtactif = true;	
        }
		

	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.name == "Player")
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
