using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBoxManager : MonoBehaviour {

    public GameObject textBox;

    public Text theText;

    public TextAsset thetext;
    public string[] textLines;

    public int currentLine;
    public int endAtLine;

    public bool isActive;

    public bool stopPlayerMovement;

    public PlayerController player;
	public string theVoice;
	public float speedtalk;
	
	public bool waitendtxt = true;
	
	private int j;

    public bool passText;

    // Use this for initialization
    void Start()
    {

        player = FindObjectOfType<PlayerController>();

        if (thetext != null)
        {
            textLines = (thetext.text.Split('\n'));
        }

        if (endAtLine == 0)
        {
            endAtLine = textLines.Length - 1;
        }


        if (isActive)
        {
            EnableTextBox();
        }
        else
        {
            DisableTextBox();
        }
    }

    void Update()
    {
        if (!isActive)
        {
            return;
        }


        //theText.text = textLines[currentLine];
		
         if (Input.GetKeyDown(KeyCode.Space) && waitendtxt == false)
        {
			
            passText = true;

        }
        
        if (Input.GetKeyDown(KeyCode.Space) && waitendtxt == true)
        {
			StartCoroutine(AnimateText());
            currentLine += 1;

        }
       

        if(currentLine == endAtLine)
        {
            DisableTextBox();
        }
    }


    public void EnableTextBox()
    {
		StartCoroutine(AnimateText());
        textBox.SetActive(true);
        isActive = true;

        if (stopPlayerMovement)
        {
            player.canMove = false;
        }

    }


    public void DisableTextBox()
    {
        textBox.SetActive(false);
        isActive = false;

        player.canMove = true;

    }

    public void ReloadScript(TextAsset theText)
    {
        if(theText != null)
        {
            textLines = new string[1];
            textLines = (theText.text.Split('\n'));

        }

    }
	
	IEnumerator AnimateText(){
     
     for (int i = 0; i < (textLines[currentLine].Length+1); i++)
     {
		 j = j + 1;
		 if (j >= (textLines[currentLine].Length)){
			j = 0; 
		 }

         if(passText == true){

            i = (textLines[currentLine].Length);
            j = (textLines[currentLine].Length-1);
            passText = false;
         }
		 
         theText.text = textLines[currentLine].Substring(0, i);
		 
		 if(textLines[currentLine][j] != ' ' && textLines[currentLine][j] != '\n'){
			FindObjectOfType<AudioManager>().Play(theVoice);
		 }
		 if (i == textLines[currentLine].Length){
			 waitendtxt = true;
		 }
		 else
		 {
			waitendtxt = false; 
		 }
         yield return new WaitForSeconds(speedtalk);
     }
	 j = -1;

	}
	
}
