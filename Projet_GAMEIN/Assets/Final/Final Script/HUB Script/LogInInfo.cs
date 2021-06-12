using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Log")]
public class Log
{
    public string ID ;
    public string IDConnexion ;
    public string PassWord ;

    public Log(string NewID, string NewIDConnexion, string NewPassWord)
    {
        ID = NewID ;
        IDConnexion = NewIDConnexion ;
        PassWord = NewPassWord ;
    }  
}


public class LogInInfo : ScriptableObject
{
    private List<Log> LogInfo = new List<Log>();    

    [Header ("Log Name")]   
    // Dev E-Art
    private string AtGreg = "DevCount_EArt_AthGreg" ;    
    private string Azphalt = "DevCount_EArt_Azphalt" ;
    private string Gmcbinho = "DevCount_EArt_Gmcbinho" ;    
    private string Helyria = "DevCount_EArt_Helyria" ;
    private string Kihou = "DevCount_EArt_Kihou" ;
    private string NioZebra = "DevCount_EArt_NioZebra" ;


    [Header ("Log PassWord")]
    // Dev E-Art
    private string AtGregPassWord = "xX-GB_576284-Xx" ;
    private string AzphaltPassWord = "xX-CM_269001-Xx" ;
    private string GmcbinhoPassWord = "xX-CM_269001-Xx" ;
    private string HelyriaPassWord = "xX-CM_269001-Xx" ;
    private string KihouPassWord = "xX-CM_269001-Xx" ;
    private string NioZebraPassWord = "xX-CM_269001-Xx" ;


    public void SetLogList() 
    {
        // Dev E-Art SetUp
        LogInfo.Add(new Log("AtGreg", AtGreg, AtGregPassWord));
        LogInfo.Add(new Log("Azphalt", Azphalt, AzphaltPassWord));
        LogInfo.Add(new Log("Gmcbinho", Gmcbinho, GmcbinhoPassWord));
        LogInfo.Add(new Log("Helyria", Helyria, HelyriaPassWord));
        LogInfo.Add(new Log("Kihou", Kihou, KihouPassWord));
        LogInfo.Add(new Log("NioZebra", NioZebra, NioZebraPassWord));

    }

    public bool CheckID(string NameEnter)
    {
        for (int i = 0; i < LogInfo.Count; i++)
        {
            if(LogInfo[i].IDConnexion == NameEnter)
            {
                return true ;
            }     
        }
        return false ;
    }

    public bool CheckPassWord(string PassWordEnter)
    {
        for (int i = 0; i < LogInfo.Count; i++)
        {
            if(LogInfo[i].PassWord == PassWordEnter)
            {
                return true ;
            }     
        }
        return false ;
    }

    public string CheckName(string NameEnter)
    {
        for (int i = 0; i < LogInfo.Count; i++)
        {
            if(LogInfo[i].IDConnexion == NameEnter)
            {
                return LogInfo[i].ID ;
            }     
        }
        return null ;
    }


    public string GetPreniumID(string NamePlayer)
    {
        for (int i = 0; i < LogInfo.Count; i++)
        {
            if(LogInfo[i].ID == NamePlayer)
            {
                return LogInfo[i].IDConnexion ;
            }  
        }
        return null ;
    }

}
