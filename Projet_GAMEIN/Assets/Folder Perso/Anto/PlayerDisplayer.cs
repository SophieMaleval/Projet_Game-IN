using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDisplayer : MonoBehaviour
{
    [Header ("Nom")]
    public string NameAvatar ;
    public bool PreniumCount ;

    [Header ("Display Skin")]
    public bool InCustom = false ;
    [SerializeField] private ItemCustomer SkinSprites ;
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
    

   public void RecupExistSkin(Slider SKinSlider) 
   {
       if(PlayerPrefs.GetInt("PlayerCustomerAsBeenVisited") == 1)
       {
           if(GameObject.Find("Player") != null)
           {
               GameObject PlayerCustom = GameObject.Find("Player") ;
               SkinColorPourcentage = PlayerCustom.GetComponent<PlayerDisplayer>().SkinColorPourcentage ;
               SKinSlider.value = SkinColorPourcentage ;

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
        if(SkinRenderer != null)
        {
            /*SkinRenderer[0].sprite = SkinSprites.Face ; 
            SkinRenderer[1].sprite = SkinSprites.Profil ;
            SkinRenderer[2].sprite = SkinSprites.Dos ;
            SkinRenderer[3].sprite = SkinSprites.Profil ;*/

            if(InCustom == false)
            {
                if(GetComponent<GridDeplacement>().IsMoving == false)
                {
                    //SkinRenderer[0].GetComponent<Animator>().runtimeAnimatorController = SkinSprites.IdleFace ;
                    //SkinRenderer[1].GetComponent<Animator>().runtimeAnimatorController = SkinSprites.IdleProfil ;
                    //SkinRenderer[2].GetComponent<Animator>().runtimeAnimatorController = SkinSprites.IdleDos ;
                    //SkinRenderer[3].GetComponent<Animator>().runtimeAnimatorController = SkinSprites.IdleProfil ;
                }

                if(GetComponent<GridDeplacement>().IsMoving == true)
                {
                    //[0].GetComponent<Animator>().runtimeAnimatorController = SkinSprites.MoveFace ;
                    //SkinRenderer[1].GetComponent<Animator>().runtimeAnimatorController = SkinSprites.MoveProfil ;
                    //SkinRenderer[2].GetComponent<Animator>().runtimeAnimatorController = SkinSprites.MoveDos ;
                    //SkinRenderer[3].GetComponent<Animator>().runtimeAnimatorController = SkinSprites.MoveProfil ;
                }                
            }
        }
        for (int i = 0; i < SkinRenderer.Count; i++)
        {   SkinRenderer[i].color = Color.Lerp(SkinColor0, SkinColor1, SkinColorPourcentage) ; }



        // Hair Visuel
        if(HairSprites != null)
        {
            //HairRenderer[0].sprite = HairSprites.Face ;
            //HairRenderer[1].sprite = HairSprites.Profil ;
            //HairRenderer[2].sprite = HairSprites.Dos ;
            //HairRenderer[3].sprite = HairSprites.Profil ;

            if(InCustom == false)
            {
                if(GetComponent<GridDeplacement>().IsMoving == false)
                {
                    //HairRenderer[0].GetComponent<Animator>().runtimeAnimatorController = HairSprites.IdleFace ;
                    //HairRenderer[1].GetComponent<Animator>().runtimeAnimatorController = HairSprites.IdleProfil ;
                    //HairRenderer[2].GetComponent<Animator>().runtimeAnimatorController = HairSprites.IdleDos ;
                    //HairRenderer[3].GetComponent<Animator>().runtimeAnimatorController = HairSprites.IdleProfil ; 
                }

                if(GetComponent<GridDeplacement>().IsMoving == true)
                {
                   // HairRenderer[0].GetComponent<Animator>().runtimeAnimatorController = HairSprites.MoveFace ;
                    //HairRenderer[1].GetComponent<Animator>().runtimeAnimatorController = HairSprites.MoveProfil ;
                    //HairRenderer[2].GetComponent<Animator>().runtimeAnimatorController = HairSprites.MoveDos ;
                    //HairRenderer[3].GetComponent<Animator>().runtimeAnimatorController = HairSprites.MoveProfil ; 
                }                  
            }
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
            //BodyRenderer[0].sprite = BodySprites.Face ;
            //BodyRenderer[1].sprite = BodySprites.Profil ;
            //BodyRenderer[2].sprite = BodySprites.Dos ;
            //BodyRenderer[3].sprite = BodySprites.Profil ;

            if(InCustom == false)
            {
                if(GetComponent<GridDeplacement>().IsMoving == false)
                {
                    //BodyRenderer[0].GetComponent<Animator>().runtimeAnimatorController = BodySprites.IdleFace ;
                    //BodyRenderer[1].GetComponent<Animator>().runtimeAnimatorController = BodySprites.IdleProfil ;
                    //BodyRenderer[2].GetComponent<Animator>().runtimeAnimatorController = BodySprites.IdleDos ;    
                    //BodyRenderer[3].GetComponent<Animator>().runtimeAnimatorController = BodySprites.IdleProfil ;         
                }

                if(GetComponent<GridDeplacement>().IsMoving == true)
                {
                    //BodyRenderer[0].GetComponent<Animator>().runtimeAnimatorController = BodySprites.MoveFace ;
                    //BodyRenderer[1].GetComponent<Animator>().runtimeAnimatorController = BodySprites.MoveProfil ;
                    //BodyRenderer[2].GetComponent<Animator>().runtimeAnimatorController = BodySprites.MoveDos ;    
                    //BodyRenderer[3].GetComponent<Animator>().runtimeAnimatorController = BodySprites.MoveProfil ;      
                }                   
            }     
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
            //BottomRenderer[0].sprite = BottomSprites.Face ;
            //BottomRenderer[1].sprite = BottomSprites.Profil ;
            //BottomRenderer[2].sprite = BottomSprites.Dos ;
            //BottomRenderer[3].sprite = BottomSprites.Profil ;

            if(InCustom == false)
            {
                if(GetComponent<GridDeplacement>().IsMoving == false)
                {
                   // BottomRenderer[0].GetComponent<Animator>().runtimeAnimatorController = BottomSprites.IdleFace ;
                    //BottomRenderer[1].GetComponent<Animator>().runtimeAnimatorController = BottomSprites.IdleProfil ;
                    //BottomRenderer[2].GetComponent<Animator>().runtimeAnimatorController = BottomSprites.IdleDos ;
                    //BottomRenderer[3].GetComponent<Animator>().runtimeAnimatorController = BottomSprites.IdleProfil ;    
                }

                if(GetComponent<GridDeplacement>().IsMoving == true)
                {
                    //BottomRenderer[0].GetComponent<Animator>().runtimeAnimatorController = BottomSprites.MoveFace ;
                    //BottomRenderer[1].GetComponent<Animator>().runtimeAnimatorController = BottomSprites.MoveProfil ;
                    //BottomRenderer[2].GetComponent<Animator>().runtimeAnimatorController = BottomSprites.MoveDos ;
                    //BottomRenderer[3].GetComponent<Animator>().runtimeAnimatorController = BottomSprites.MoveProfil ;    
                }                    
            }    
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
            //ShoeRenderer[0].sprite = ShoeSprites.Face ;
            //ShoeRenderer[1].sprite = ShoeSprites.Profil ;
            //ShoeRenderer[2].sprite = ShoeSprites.Dos ;
            //ShoeRenderer[3].sprite = ShoeSprites.Profil ;

            if(InCustom == false)
            {
                if(GetComponent<GridDeplacement>().IsMoving == false)
                {
                    //ShoeRenderer[0].GetComponent<Animator>().runtimeAnimatorController = ShoeSprites.IdleFace ;
                    //ShoeRenderer[1].GetComponent<Animator>().runtimeAnimatorController = ShoeSprites.IdleProfil ;
                    //ShoeRenderer[2].GetComponent<Animator>().runtimeAnimatorController = ShoeSprites.IdleDos ;
                    //ShoeRenderer[3].GetComponent<Animator>().runtimeAnimatorController = ShoeSprites.IdleProfil ;
                }

                if(GetComponent<GridDeplacement>().IsMoving == true)
                {
                    //ShoeRenderer[0].GetComponent<Animator>().runtimeAnimatorController = ShoeSprites.MoveFace ;
                    //ShoeRenderer[1].GetComponent<Animator>().runtimeAnimatorController = ShoeSprites.MoveProfil ;
                    //ShoeRenderer[2].GetComponent<Animator>().runtimeAnimatorController = ShoeSprites.MoveDos ;
                    //ShoeRenderer[3].GetComponent<Animator>().runtimeAnimatorController = ShoeSprites.MoveProfil ;
                }                       
            }
        }
        if(ShoeSprites == null)
        {
            for (int i = 0; i < ShoeRenderer.Count; i++)
            {   ShoeRenderer[i].sprite = null ;    }
        }
        for (int i = 0; i < ShoeRenderer.Count; i++)
        {   ShoeRenderer[i].color = ShoeColor ; }
    }

  /*  public void SkinModify()
    {
        // Désactive Tout les skin Renderer pour les synchronisé
        SwitchRenderSkin(false);
        SwitchRenderHair(false);
        SwitchRenderBody(false);
        SwitchRenderBottom(false);
        SwitchRenderShoe(false);

        StartCoroutine(EnableAnilator());
    }
    void SwitchRenderSkin(bool Enable) 
    {
        for (int i = 0; i < SkinRenderer.Count; i++)
        {   SkinRenderer[i].GetComponent<Animator>().enabled = Enable ;  }
    }
    void SwitchRenderHair(bool Enable) 
    {
        for (int i = 0; i < HairRenderer.Count; i++)
        {   HairRenderer[i].GetComponent<Animator>().enabled = Enable ;  }
    }
    void SwitchRenderBody(bool Enable) 
    {
        for (int i = 0; i < BodyRenderer.Count; i++)
        {   BodyRenderer[i].GetComponent<Animator>().enabled = Enable ;  }
    }
    void SwitchRenderBottom(bool Enable) 
    {
        for (int i = 0; i < BottomRenderer.Count; i++)
        {   BottomRenderer[i].GetComponent<Animator>().enabled = Enable ;  }
    }
    void SwitchRenderShoe(bool Enable) 
    {
        for (int i = 0; i < ShoeRenderer.Count; i++)
        {   ShoeRenderer[i].GetComponent<Animator>().enabled = Enable ;  }
    }
 */   IEnumerator EnableAnilator()
    {
        yield return new WaitForSeconds(0.01f);
        // Réactive Tout les skin Renderer pour les synchronisé
       /* SwitchRenderSkin(true);
        SwitchRenderHair(true);
        SwitchRenderBody(true);
        SwitchRenderBottom(true);
        SwitchRenderShoe(true);*/
    }

}
