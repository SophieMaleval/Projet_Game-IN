using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Log")]
public class LogInInfo : ScriptableObject
{
    [Header ("Log Name")]
    private string NioZebra = "DevCount_EArt_NioZebra" ;

    [Header ("Log PassWord")]
    private string NioZebraPassWord = "xX-CM_269001-Xx" ;

    //[Header ("Display Avatar")]

    public bool CheckName(string NameEnter)
    {
        if(NioZebra == NameEnter)
        {
            return true ;   
        } else {
            return false ;
        }
    }

    public bool CheckPassWord(string NameEnter, string PassWordEnter)
    {
        if(NioZebra == NameEnter && NioZebraPassWord == PassWordEnter)
        {
            return true ;
        } else {
            return false ;
        }
    }
}
