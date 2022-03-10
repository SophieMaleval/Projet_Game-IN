using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Informations Pannel")]
public class PannelENTContainer : ScriptableObject
{
    [Header ("Informations Principale")]
    public string NomEntreprise ;
    public string URLSiteWebDisplay ;
    public string URLSiteWeb ;
    public string ContactEmail ;
    public string URLFacebook ;
    public string URLInstagram ;
    public string URLTwitter ;
    public string URLLinkedIn ;
    public string URLDiscord ;
    public string URLSteam ;
    public string URLTwitch ;
    public string URLYoutube ;


    [Header ("Illustration")]
    public Sprite LogoENT ;
    public Sprite IllustrationDescriptionENT ;
    public Sprite IllustrationValeurENT ;
    public Sprite IllustrationDescriptionDernierProjet ;
    public Sprite IllustrationDescriptionAvantDernierProjet ;


    [Header ("Textes")]
    [TextArea (5,10)] public string DescriptionENTFR ;
    [TextArea (5,10)] public string DescriptionENTEN ;
    [Space(10)]
    public List<ValeurENT> ValeursENT ;
    [Space(10)]
    public string Localisation ;
    public List<string> PersonneJoignable ;
    [TextArea (5,10)] public string DernierProjet ;
    [TextArea (5,10)] public string AvantDernierProjet ;

}



[System.Serializable]
public class ValeurENT
{
    public Sprite IllustrationValeur ;
    public string NomValeurFR ;
    public string NomValeurEN ;
}