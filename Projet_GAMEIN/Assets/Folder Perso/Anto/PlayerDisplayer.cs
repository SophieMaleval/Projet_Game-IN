using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDisplayer : MonoBehaviour
{
    public float SkinColorPourcentage ;

    public ItemCustomer HairSprites ;
    public Color HairColor ;

    public ItemCustomer BodySprites ;
    public Color BodyColor ;

    public ItemCustomer BottomSprites ;
    public Color BottomColor ;

    public ItemCustomer ShoeSprites ;
    public Color ShoeColor ;

    [Header ("Setting Visual")]
    [SerializeField] private Color SkinColor0 ;
    [SerializeField] private Color SkinColor1 ;
    [SerializeField] private List<SpriteRenderer> SkinRenderer ;
    [SerializeField] private List<SpriteRenderer> HairRenderer ;
    [SerializeField] private List<SpriteRenderer> BodyRenderer ;
    [SerializeField] private List<SpriteRenderer> BottomRenderer ;
    [SerializeField] private List<SpriteRenderer> ShoeRenderer ;
    

   public void RecupExistSkin() 
   {
       if(PlayerPrefs.GetInt("PlayerCustomerAsBeenVisited") == 1)
       {
           if(GameObject.Find("Player") != null)
           {
               GameObject PlayerCustom = GameObject.Find("Player") ;
               SkinColorPourcentage = PlayerCustom.GetComponent<PlayerDisplayer>().SkinColorPourcentage ;

               HairSprites = PlayerCustom.GetComponent<PlayerDisplayer>().HairSprites ;
                HairColor = PlayerCustom.GetComponent<PlayerDisplayer>().HairColor ;

               BodySprites = PlayerCustom.GetComponent<PlayerDisplayer>().BodySprites ;
               BodyColor = PlayerCustom.GetComponent<PlayerDisplayer>().BodyColor ;

               BottomSprites = PlayerCustom.GetComponent<PlayerDisplayer>().BottomSprites ;
               BottomColor = PlayerCustom.GetComponent<PlayerDisplayer>().BottomColor ;

               ShoeSprites = PlayerCustom.GetComponent<PlayerDisplayer>().ShoeSprites ;
               ShoeColor = PlayerCustom.GetComponent<PlayerDisplayer>().ShoeColor ;   


               Destroy(PlayerCustom.gameObject);            
           }
       }
   }

    void Update()
    {
        // Skin Collor
        for (int i = 0; i < SkinRenderer.Count; i++)
        {   SkinRenderer[i].color = Color.Lerp(SkinColor0, SkinColor1, SkinColorPourcentage) ; }



        // Hair Visuel
        if(HairSprites != null)
        {
            HairRenderer[0].sprite = HairSprites.Face ;
            HairRenderer[1].sprite = HairSprites.Profil ;
            HairRenderer[2].sprite = HairSprites.Dos ;
            HairRenderer[3].sprite = HairSprites.Profil ;            
        }
        if(HairSprites == null)
        {
            for (int i = 0; i < HairRenderer.Count; i++)
            {   HairRenderer[i].sprite = null ;    }
        }
        for (int i = 0; i < HairRenderer.Count; i++)
        {   HairRenderer[i].color = HairColor ; }



        // Body Visuel
        if(BodySprites != null)
        {
            BodyRenderer[0].sprite = BodySprites.Face ;
            BodyRenderer[1].sprite = BodySprites.Profil ;
            BodyRenderer[2].sprite = BodySprites.Dos ;
            BodyRenderer[3].sprite = BodySprites.Profil ;            
        }
        if(BodySprites == null)
        {
            for (int i = 0; i < BodyRenderer.Count; i++)
            {   BodyRenderer[i].sprite = null ; }
        }
        for (int i = 0; i < BodyRenderer.Count; i++)
        {   BodyRenderer[i].color = BodyColor ; }



        // Bottom Visuel
        if(BottomSprites != null)
        {
            BottomRenderer[0].sprite = BottomSprites.Face ;
            BottomRenderer[1].sprite = BottomSprites.Profil ;
            BottomRenderer[2].sprite = BottomSprites.Dos ;
            BottomRenderer[3].sprite = BottomSprites.Profil ;            
        }
        if(BottomSprites == null)
        {
            for (int i = 0; i < BottomRenderer.Count; i++)
            {   BottomRenderer[i].sprite = null ;   }
        }
        for (int i = 0; i < BottomRenderer.Count; i++)
        {   BottomRenderer[i].color = BottomColor ; }



        // Shoe Visuel
        if(ShoeSprites != null)
        {
            ShoeRenderer[0].sprite = ShoeSprites.Face ;
            ShoeRenderer[1].sprite = ShoeSprites.Profil ;
            ShoeRenderer[2].sprite = ShoeSprites.Dos ;
            ShoeRenderer[3].sprite = ShoeSprites.Profil ;
        }
        if(ShoeSprites == null)
        {
            for (int i = 0; i < ShoeRenderer.Count; i++)
            {   ShoeRenderer[i].sprite = null ;    }
        }
        for (int i = 0; i < ShoeRenderer.Count; i++)
        {   ShoeRenderer[i].color = ShoeColor ; }
    }
}
