using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI ;

[CreateAssetMenu(fileName = "New PetiteAnnonceContainer", menuName = "Village/Petite Annonce Contenu")]
public class PetiteAnnonceContainer : ScriptableObject
{
    public List<Annonce> AnnonceDisponible = new List<Annonce>() ;
}

[System.Serializable]
public class Annonce
{
    public string EntrepriseName ;

    public Sprite FlyerFR ;
    public Sprite FlyerEN ;
    
    public Sprite FondAnnonceFR ;
    public Sprite FondAnnonceEN ;

    [TextArea(5,10)] public string TextAnnonceFR ;
    [TextArea(5,10)] public string TextAnnonceEN ;

    public Color BackgroundColorAnnonce ;


    public Vector2 SizeAnnonce = new Vector2(200f, 450f);
    public Vector3 PosAndRotation ;
}
