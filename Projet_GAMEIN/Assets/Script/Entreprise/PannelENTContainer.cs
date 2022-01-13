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

    [Header ("Illustration")]
    public Sprite LogoENT ;
    public Sprite IllustrationDescriptionENT ;
    public Sprite IllustrationValeurENT ;
    public Sprite IllustrationDescriptionDernierProjet ;
    public Sprite IllustrationDescriptionAvantDernierProjet ;

}
