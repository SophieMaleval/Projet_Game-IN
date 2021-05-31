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

    public void NameChoice()
    {
        if(Info.CheckName(NameInputArea.text))
        {
            PassWordEmpty.SetActive(true);
        } else {
            Debug.Log("Name Choice : " +  NameInputArea.text);            
        }
    }

    public void PassWord()
    {
        if(Info.CheckPassWord(NameInputArea.text, PassWordInputArea.text))
        {
            Debug.Log("Access Authorized !") ;
        } else {
            Debug.Log("ERROR !!");            
        }
    }



}
