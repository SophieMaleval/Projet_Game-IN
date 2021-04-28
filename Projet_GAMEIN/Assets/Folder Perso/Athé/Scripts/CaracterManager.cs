using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CaracterManager : MonoBehaviour {

	public HeadSelector HeadSelect;
	public TextBoxManager theTextBox;
	public ActivateTextAtLine Acttxtline;
	
	
	// Use this for initialization
	void Awake () {
		HeadSelect = FindObjectOfType<HeadSelector>();
		theTextBox = FindObjectOfType<TextBoxManager>();
		Acttxtline = FindObjectOfType<ActivateTextAtLine>();
	}
	
	// Update is called once per frame
	void Update () {

		if(Acttxtline.txtactif == true){
		if (HeadSelect.currentID.Substring(0, 2) == "01"){
			name = "jennys";
			theTextBox.speedtalk = 0.05f;
			theTextBox.theVoice = "VoiceJenny";
			theTextBox.theText.color = Color.white;
			theTextBox.theText.font = Resources.Load<Font>("Poker Dogs");
			
			if(theTextBox.currentLine == 96){
		theTextBox.theVoice = "VoiceFloweyMad";
		}
		if(theTextBox.currentLine == 65){
		theTextBox.theVoice = "VoiceFloweyMad";
		}
		if(theTextBox.currentLine == 98){
		theTextBox.theVoice = "VoiceFloweyMad";
		}
		if(theTextBox.currentLine == 67){
		theTextBox.theVoice = "VoiceFloweyMad";	
		}
		
		}
		
		if (HeadSelect.currentID.Substring(0, 2) == "02"){
			name = "sans";
			theTextBox.theVoice = "VoiceSans";
			theTextBox.speedtalk = 0.07f;
			theTextBox.theText.color = Color.red;
			theTextBox.theText.font = Resources.Load<Font>("Comic Sans MS pixel rus eng");
		}
		if (HeadSelect.currentID.Substring(0, 2) == "03"){
			name = "grillby";
			theTextBox.theVoice = "VoiceGrillby";
			theTextBox.speedtalk = 0.08f;
			theTextBox.theText.color = Color.white;
			theTextBox.theText.font = Resources.Load<Font>("chp-fire");
		}
		if (HeadSelect.currentID.Substring(0, 2) == "00"){
			name = "none";
			theTextBox.theVoice = "GameVoice";
			theTextBox.speedtalk = 0.05f;
			theTextBox.theText.color = Color.white;
			theTextBox.theText.font = Resources.Load<Font>("DTM-Sans");
		}
		
		if (HeadSelect.currentID.Substring(0, 2) == "04"){
			name = "papyrus";
			theTextBox.theVoice = "VoicePapyrus";
			theTextBox.speedtalk = 0.05f;
			theTextBox.theText.color = Color.red;
			theTextBox.theText.font = Resources.Load<Font>("papyrus-text");
		}
		
		if (HeadSelect.currentID.Substring(0, 2) == "99"){
			name = "Save";
			theTextBox.theVoice = "VoiceFloweyMad";
			theTextBox.speedtalk = 0.05f;
			theTextBox.theText.color = Color.red;
			theTextBox.theText.font = Resources.Load<Font>("DTM-Sans");
		}
		
		if (HeadSelect.currentID.Substring(0, 2) == "22"){
			name = "Sans";
			theTextBox.theVoice = "VoiceSans";
			theTextBox.speedtalk = 0.05f;
			theTextBox.theText.color = Color.white;
			theTextBox.theText.font = Resources.Load<Font>("DTM-Sans");
		}
		
		if (HeadSelect.currentID.Substring(0, 2) == "11"){
			name = "jennysFlower";
			theTextBox.theVoice = "VoiceJenny";
			theTextBox.speedtalk = 0.05f;
			theTextBox.theText.color = Color.white;
			theTextBox.theText.font = Resources.Load<Font>("DTM-Sans");
		}
		
		if (HeadSelect.currentID.Substring(0, 2) == "55"){
			name = "FloweyFlower";
			theTextBox.theVoice = "VoiceFloweyMad";
			theTextBox.speedtalk = 0.05f;
			theTextBox.theText.color = Color.white;
			theTextBox.theText.font = Resources.Load<Font>("DTM-Sans");
		}
		
		}
		
		
		
	}
}
