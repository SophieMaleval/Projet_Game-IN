using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Log")]
public class Log
{
    public string ID ;
    public string IDConnexion ;
    public string PassWord ;
    public Sprite PreniumSkin ;

    public Log(string NewID, string NewIDConnexion, string NewPassWord, Sprite NewPreniumApparence)
    {
        ID = NewID ;
        IDConnexion = NewIDConnexion ;
        PassWord = NewPassWord ;
        PreniumSkin = NewPreniumApparence ;
    }  
}


public class LogInInfo : ScriptableObject
{
    private List<Log> LogInfo = new List<Log>();    

    [Header ("Log Name")]  
    // Big Boss
    private string GMLaurent = "BigBoss_LaurentGame.In" ;

    // Dev E-Art
    private string AtGreg = "DevCount_EArt_AthGreg" ;    
    private string Azphalt = "DevCount_EArt_Azphalt" ;
    private string Gmcbinho = "DevCount_EArt_Gmcbinho" ;    
    private string Helyria = "DevCount_EArt_Helyria" ;
    private string Kihou = "DevCount_EArt_Kihou" ;
    private string NioZebra = "DevCount_EArt_NioZebra" ;


    [Header ("Log PassWord")]
    // Big Boss
    public string GMLaurentPassWord = "LoveMyJob" ;

    // Dev E-Art
    private string AtGregPassWord = "xX-GB_576284-Xx" ;
    private string AzphaltPassWord = "xX-CM_269001-Xx" ;
    private string GmcbinhoPassWord = "xX-CM_269001-Xx" ;
    private string HelyriaPassWord = "xX-CM_269001-Xx" ;
    private string KihouPassWord = "xX-CM_269001-Xx" ;
    private string NioZebraPassWord = "xX-CM_269001-Xx" ;


    [Header ("Apparence")]
    // Big Boss
    public Sprite GMLaurentSkin ;

    [Space]

    // Dev E-Art
    public Sprite AtGregSkin ;
    public Sprite AzphaltSkin ;
    public Sprite GmcbinhoSkin ;
    public Sprite HelyriaSkin ;
    public Sprite KihouSkin ;
    public Sprite NioZebraSkin ;


    public void SetLogList() 
    {
        // BigBoss
        LogInfo.Add(new Log("Laurent", GMLaurent, GMLaurentPassWord, GMLaurentSkin));

        // Dev E-Art SetUp
        LogInfo.Add(new Log("AtGreg", AtGreg, AtGregPassWord, AtGregSkin));
        LogInfo.Add(new Log("Azphalt", Azphalt, AzphaltPassWord, AzphaltSkin));
        LogInfo.Add(new Log("Gmcbinho", Gmcbinho, GmcbinhoPassWord, GmcbinhoSkin));
        LogInfo.Add(new Log("Helyria", Helyria, HelyriaPassWord, HelyriaSkin));
        LogInfo.Add(new Log("Kihou", Kihou, KihouPassWord, KihouSkin));
        LogInfo.Add(new Log("NioZebra", NioZebra, NioZebraPassWord, NioZebraSkin));

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



    public Sprite ReturnSKin(string NameEnter)
    {
        for (int i = 0; i < LogInfo.Count; i++)
        {
            if(LogInfo[i].IDConnexion == NameEnter)
            {
                return LogInfo[i].PreniumSkin ;
            }     
        }
        return null ;
    }

}
