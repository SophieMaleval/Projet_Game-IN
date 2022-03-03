using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Objet Inventaire Interactible")]
public class InteractibleObject : ScriptableObject
{
    [Header ("Sprites Display in Game")]
    public Sprite NormalSprite ;
    public Sprite HighlightSprite ;

    [Header ("UI")]
    public string Name ;
    public Sprite UISprite ;

    [Header("Quantity")]
    public int unité = 1;
    public int valeurMax = 1;
}
