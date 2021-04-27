using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeadSelector : MonoBehaviour {

	public TextAsset textIDHead;
    public string[] textLines;
	public int currentLine;
	public string currentID;

	//None---------------------------
	public Sprite None;

	public Sprite Test1;
	public Sprite Test2;
	
	public TextBoxManager txtboxmang;
	
	// Use this for initialization
	void Awake () {
		
		txtboxmang = FindObjectOfType<TextBoxManager>();

		
        if(textIDHead != null)
        {
            textLines = (textIDHead.text.Split('\n'));
        }
		currentID = textLines[currentLine];
	}
	
	// Update is called once per frame
	void Update () {

		currentID = textLines[currentLine];
			
		currentLine = txtboxmang.currentLine;
		
		if(currentID.Substring(0, 4) == "0100"){
			this.gameObject.GetComponent<Image>().sprite = Test1;
		}
		if(currentID.Substring(0, 4) == "0101"){
			this.gameObject.GetComponent<Image>().sprite = Test2;
		}
	
		//None----------------------------------------------------------------------
		if(currentID.Substring(0, 4) == "0000"){
			this.gameObject.GetComponent<Image>().sprite = None;
		}
		//---------------------------------------------------------------------------
		
	}
}
