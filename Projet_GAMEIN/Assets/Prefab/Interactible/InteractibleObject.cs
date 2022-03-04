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

    public Sprite unfinishedSprite;
    public Sprite finalSprite;

    [Header("Quantity")]
    [HideInInspector] public int unité = 0;
    public int valeurMax = 1;

    public bool multipleEntries;

    public void AddEntry()
    {
        if(unité <= valeurMax)
        {
            unité++;
            SpriteUIManager();
        }
    }

    public void SpriteUIManager()
    {
        if (unité <= valeurMax)
        {
            UISprite = unfinishedSprite;
        } else {
            UISprite = finalSprite;
        }
    }
}
