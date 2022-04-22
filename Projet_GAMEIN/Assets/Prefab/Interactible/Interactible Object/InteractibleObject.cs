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
    public string NameFR ;
    public string NameEN ;
    public Sprite UISprite ;

    public Sprite unfinishedSprite;
    public Sprite finalSprite;

    [Header("Quantity")]
    public int unité = 1;
    public int valeurMax = 1;

    public bool multipleEntries;


    public void AddEntry()
    {
        if(unité <= valeurMax)
        {
            unité ++;
            SpriteUIManager();
        }
    }

    public void SpriteUIManager()
    {
        if (unité < valeurMax)
        {
            UISprite = unfinishedSprite;
        } else {
            UISprite = finalSprite;
        }
    }
}
