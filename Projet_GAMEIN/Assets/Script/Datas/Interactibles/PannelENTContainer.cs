using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New PannelENTContainer", menuName = "Village/Informations Pannel")]
public class PannelENTContainer : ScriptableObject
{
    #region UnityInspector

    [Header ("Entreprise")]
    public string NomEntreprise ;    
    public Sprite LogoENT ;
    public Sprite IllustrationDescriptionENT ;
    [TextArea (5,10)] public string DescriptionENTFR ;
    [TextArea (5,10)] public string DescriptionENTEN ;
    public float HeightDescription ;
    public List<ValeurENT> ValeursENT ;

    [Header ("Lien")]
    public string URLSiteWebDisplay ;
    public string URLSiteWeb ;
    [Space(5)]    
    public string ContactEmail ;
    public string URLFacebook ;
    public string URLInstagram ;
    public string URLTwitter ;
    public string URLLinkedIn ;
    public string URLDiscord ;
    public string URLSteam ;
    public string URLTwitch ;
    public string URLYoutube ;


    [Header ("Coordonné")]
    public string Localisation ;
    public List<string> PersonneJoignable ;

    [Header ("Activité")]
    public List<ActivitéENT> ListActivité = new List<ActivitéENT>();

    #endregion
}



[System.Serializable]
public class ValeurENT
{
    public Sprite IllustrationValeur ;
    public string NomValeurFR ;
    public string NomValeurEN ;
}

[System.Serializable]
public class ActivitéENT
{
    public string NameActivitéFR ;
    public string NameActivitéEN ;
    public Sprite IllustrationActivité ;
    [TextArea (5,10)] public string TextActivtéFR;
    [TextArea (5,10)] public string TextActivtéEN ;
    public float HeightArticle = 200f;
}