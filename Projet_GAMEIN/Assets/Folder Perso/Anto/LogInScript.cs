using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogInScript : MonoBehaviour
{
    [SerializeField] private InputField NameInputArea ;
    [SerializeField] private LogInInfo Info ;
    [SerializeField] private GameObject PassWordEmpty ;
    [SerializeField] private InputField PassWordInputArea ;
    [SerializeField] private GameObject PassWordErrorText ;

    [SerializeField] private PlayerDisplayer Player ;


    private void Awake() 
    {
        Info.SetLogList();
        NameInputArea.GetComponentInChildren<Text>().horizontalOverflow = HorizontalWrapMode.Wrap ;
        PassWordInputArea.GetComponentInChildren<Text>().horizontalOverflow = HorizontalWrapMode.Wrap ;

        if(Player.PreniumCount == false)
        {    NameInputArea.text = Player.NameAvatar ; }
        if(Player.PreniumCount == true)
        {   NameInputArea.text = Info.GetPreniumID(Player.NameAvatar); }

    }

    public void NameChoice()
    {
        if(Info.CheckID(NameInputArea.text))
        {
            PassWordEmpty.SetActive(true);
        } else {
            PassWordErrorText.SetActive(false);  
            PassWordInputArea.text = "" ;
            PassWordEmpty.SetActive(false);              
            // Debug.Log("Welcome : " + NameInputArea.text);      // Référencer dans le player      
            Player.NameAvatar = NameInputArea.text ;
            Player.PreniumCount = false ;
        }
    }

    public void PassWord()
    {
        if(Info.CheckPassWord(PassWordInputArea.text))
        {
            PassWordErrorText.SetActive(false);            
            // Debug.Log("Welcome : " + Info.CheckName(NameInputArea.text)) ; // Référencer dans le player  
            Player.NameAvatar = Info.CheckName(NameInputArea.text) ;
            Player.PreniumCount = true ;
        } else {
            PassWordErrorText.SetActive(true);
            Player.PreniumCount = false ;
        }
    }



}
