using System.Collections;
using System.Collections.Generic;
// using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class Customizer : MonoBehaviour
{
    [Header ("Custom Avatar")]
    [SerializeField] private PlayerMovement Player ;
    //[SerializeField] private PlayerDisplayer DisplayAvatar ;
    [SerializeField] private Button RandomButton ;
    [SerializeField] private Button SubmitButton ;

    [Header ("CustomerPanel")]
    [SerializeField] private Slider SkinSlider ;
    public int CurrentCategorie ;

    [SerializeField] private List<Image> ChoiceDisplayer ;
    [SerializeField] private List<Button> AllChoiceColorButton ;

        public List<int> CategoriesStates ;
       /* public int CurrentHairChoiceDisplay = 0 ;
        public int CurrentTopChoiceDisplay = 0 ;
        public int CurrentPantsChoiceDisplay = 0 ;
        public int CurrentShoeChoiceDisplay = 0 ;*/
 

    [Header ("Item Disponible")]
    public List<ItemCustomer> HairChoice ;
    public List<ItemCustomer> TopChoice ;
    public List<ItemCustomer> PantsChoice ;
    public List<ItemCustomer> ShoeChoice ;
    [Space]
    [SerializeField] private Color ColorDoNotUse ;
    [SerializeField] private Color ColorUse ;

    [Header("Custom Color Possible")]
    public List<Color> ColorCustomList ;


    private void Awake() 
    {
        if(GameObject.Find("Player") != null)
        {
            Player = GameObject.Find("Player").GetComponent<PlayerMovement>() ;                    
        }
    }
    
    private void Start() 
    {
        
    }


    void Update()
    {




        // SUbmit Disable Without Name
   /*     if(DisplayAvatar.NameAvatar == "")
        {
            SubmitButton.interactable =false ;
        }
        if(DisplayAvatar.NameAvatar != "")
        {
            SubmitButton.interactable = true ;
        }*/
    }


    // Categorie Num
    // 0 - Skin
    // 1 - Hair
    // 2 - Top
    // 3 - Pant
    // 4 - Shoe
    public void ChoiceDisplay(int CurrentCategorieChoiceDisplay, List<ItemCustomer> CategorieChoice, int CategorieNum)
    {
        // Set sur le Player la sélection
        ChoiceDisplayer[1].sprite = CategorieChoice[CurrentCategorieChoiceDisplay].DisplayCustomisation ;
        Player.Animators[CategorieNum].runtimeAnimatorController = CategorieChoice[CurrentCategorieChoiceDisplay].Animator ;

        // Affiche dans la liste le : dernier PREMIER second
        if(CurrentCategorieChoiceDisplay == 0)
        {
            ChoiceDisplayer[0].sprite = CategorieChoice[CategorieChoice.Count - 1].DisplayCustomisation ;
            ChoiceDisplayer[2].sprite = CategorieChoice[CurrentCategorieChoiceDisplay + 1].DisplayCustomisation ;
        }

        // Affiche dans la liste le : avant-dernier DERNIER premier
        if(CurrentCategorieChoiceDisplay == CategorieChoice.Count - 1)
        {
            ChoiceDisplayer[0].sprite = CategorieChoice[CurrentCategorieChoiceDisplay - 1].DisplayCustomisation ;
            ChoiceDisplayer[2].sprite = CategorieChoice[0].DisplayCustomisation ;
        }

        // Affiche dans la liste le : n-1 N n+1
        if((CurrentCategorieChoiceDisplay != 0) && (CurrentCategorieChoiceDisplay != CategorieChoice.Count - 1))
        {
            ChoiceDisplayer[0].sprite = CategorieChoice[CurrentCategorieChoiceDisplay - 1].DisplayCustomisation ;
            ChoiceDisplayer[2].sprite = CategorieChoice[CurrentCategorieChoiceDisplay + 1].DisplayCustomisation ;
        }
    }

    void ChangeChoice(int ChoiceValueAdd, int CurrentCategorieChoiceDisplay, List<ItemCustomer> CategorieChoice, int CategorieNum)
    {
        CurrentCategorieChoiceDisplay += ChoiceValueAdd ;
        Debug.Log(CurrentCategorieChoiceDisplay);
        if(CurrentCategorieChoiceDisplay >= CategorieChoice.Count)  CurrentCategorieChoiceDisplay = 0 ;
        if(CurrentCategorieChoiceDisplay < 0)  CurrentCategorieChoiceDisplay = CategorieChoice.Count - 1 ;

        ChoiceDisplay(CurrentCategorieChoiceDisplay, CategorieChoice, CategorieNum) ;
    }

    public void ChangeDisplay(int ValueAdd)
    {
        if(CurrentCategorie == 1)
            ChangeChoice(ValueAdd, CurrentCategorie, HairChoice, 1);   
        if(CurrentCategorie == 2)
            ChangeChoice(ValueAdd, CurrentCategorie, TopChoice, 2);
        if(CurrentCategorie == 3)
            ChangeChoice(ValueAdd, CurrentCategorie, PantsChoice, 3);
        if(CurrentCategorie == 4)
            ChangeChoice(ValueAdd, CurrentCategorie, ShoeChoice, 4);
    }



    // Panel Custom
    public void SkinColor(float SliderValue)
    {   /*DisplayAvatar.SkinColorPourcentage = SliderValue ; *   CheckColorUsed();*/ }

    public void ChangeHairColor(int NumColorList)
    {    /*DisplayAvatar.HairColor = ColorCustomList[NumColorList] ;*  CheckColorUsed();*/ }

    public void ChangeBodyColor(int NumColorList)
    {    /*DisplayAvatar.BodyColor = ColorCustomList[NumColorList] ; * CheckColorUsed(); */}

    public void ChangeBottomColor(int NumColorList)
    {   /*DisplayAvatar.BottomColor = ColorCustomList[NumColorList] ;*  CheckColorUsed(); */}

    public void ChangeShoeColor(int NumColorList)
    {   /*DisplayAvatar.ShoeColor = ColorCustomList[NumColorList] ;*  CheckColorUsed(); */}











 /*   // Check Color
    void CheckColorUsed()
    {
        // Check Hair Color
        if(DisplayAvatar.HairSprites != HairChoice[0])
        {
            for (int i = 0; i < AllHairColorButton.Count; i++)
            {
                if(ColorCustomList[i] == DisplayAvatar.HairColor)
                {
                    AllHairColorButton[i].interactable = false ;
                    AllHairColorButton[i].gameObject.GetComponentInChildren<SpriteRenderer>().GetComponent<Image>().color = ColorUse ;
                }
                if(ColorCustomList[i] != DisplayAvatar.HairColor)
                {
                    AllHairColorButton[i].interactable = true ;
                    AllHairColorButton[i].gameObject.GetComponentInChildren<SpriteRenderer>().GetComponent<Image>().color = ColorDoNotUse ;
                }
            }
        }
        if(DisplayAvatar.HairSprites == HairChoice[0])
        {
            for (int i = 0; i < AllHairColorButton.Count; i++)
            {
                AllHairColorButton[i].interactable = true ;
                AllHairColorButton[i].gameObject.GetComponentInChildren<SpriteRenderer>().GetComponent<Image>().color = ColorDoNotUse ;
            }
        }


        // Check Body Color
        if(DisplayAvatar.BodySprites != BodyChoice[0])
        {
            for (int i = 0; i < AllBodyColorButton.Count; i++)
            {
                if(ColorCustomList[i] == DisplayAvatar.BodyColor)
                {
                    AllBodyColorButton[i].interactable = false ;
                    AllBodyColorButton[i].gameObject.GetComponentInChildren<SpriteRenderer>().GetComponent<Image>().color = ColorUse ;
                }
                if(ColorCustomList[i] != DisplayAvatar.BodyColor)
                {
                    AllBodyColorButton[i].interactable = true ;
                    AllBodyColorButton[i].gameObject.GetComponentInChildren<SpriteRenderer>().GetComponent<Image>().color = ColorDoNotUse ;
                }
            }
        }
        if(DisplayAvatar.BodySprites == BodyChoice[0])
        {
            for (int i = 0; i < AllBodyColorButton.Count; i++)
            {
                AllBodyColorButton[i].interactable = true ;
                AllBodyColorButton[i].gameObject.GetComponentInChildren<SpriteRenderer>().GetComponent<Image>().color = ColorDoNotUse ;
            }
        }


        // Check Bottom Color
        if(DisplayAvatar.BottomSprites != BottomChoice[0])
        {
            for (int i = 0; i < AllBottomColorButton.Count; i++)
            {
                if(ColorCustomList[i] == DisplayAvatar.BottomColor)
                {
                    AllBottomColorButton[i].interactable = false ;
                    AllBottomColorButton[i].gameObject.GetComponentInChildren<SpriteRenderer>().GetComponent<Image>().color = ColorUse ;
                }
                if(ColorCustomList[i] != DisplayAvatar.BottomColor)
                {
                    AllBottomColorButton[i].interactable = true ;
                    AllBottomColorButton[i].gameObject.GetComponentInChildren<SpriteRenderer>().GetComponent<Image>().color = ColorDoNotUse ;
                }
            }
        }
        if(DisplayAvatar.BottomSprites == BottomChoice[0])
        {
            for (int i = 0; i < AllBottomColorButton.Count; i++)
            {
                AllBottomColorButton[i].interactable = true ;
                AllBottomColorButton[i].gameObject.GetComponentInChildren<SpriteRenderer>().GetComponent<Image>().color = ColorDoNotUse ;
            }
        }


        // Check Shoe Color
        if(DisplayAvatar.ShoeSprites != ShoeChoice[0])
        {
            for (int i = 0; i < AllShoeColorButton.Count; i++)
            {
                if(ColorCustomList[i] == DisplayAvatar.ShoeColor)
                {
                    AllShoeColorButton[i].interactable = false ;
                    AllShoeColorButton[i].gameObject.GetComponentInChildren<SpriteRenderer>().GetComponent<Image>().color = ColorUse ;
                }
                if(ColorCustomList[i] != DisplayAvatar.ShoeColor)
                {
                    AllShoeColorButton[i].interactable = true ;
                    AllShoeColorButton[i].gameObject.GetComponentInChildren<SpriteRenderer>().GetComponent<Image>().color = ColorDoNotUse ;
                }
            }
        }
        if(DisplayAvatar.ShoeSprites == ShoeChoice[0])
        {
            for (int i = 0; i < AllShoeColorButton.Count; i++)
            {
                AllShoeColorButton[i].interactable = true ;
                AllShoeColorButton[i].gameObject.GetComponentInChildren<SpriteRenderer>().GetComponent<Image>().color = ColorDoNotUse ;
            }
        }
    }
*/




  /*  // Final Button
    public void RandomCharacter()
    {
        // Random Skin
        //DisplayAvatar.SkinColorPourcentage = Random.Range(0f, 1f);
        // SkinSlider.value = DisplayAvatar.SkinColorPourcentage ;

        // Random Hair
        int RandHair = Random.Range(0, HairChoice.Count) ;
        //DisplayAvatar.HairSprites = HairChoice[RandHair] ;
        CurrentHairChoiceDisplay = RandHair ;
        //DisplayAvatar.HairColor = ColorCustomList[Random.Range(0, ColorCustomList.Count)] ;

        // Random Body
        int RandBody = Random.Range(0, TopChoice.Count) ;
        //DisplayAvatar.BodySprites = BodyChoice[RandBody] ;
        CurrentTopChoiceDisplay = RandBody ;
        //DisplayAvatar.BodyColor = ColorCustomList[Random.Range(0, ColorCustomList.Count)] ;

        // Random Bottom
        int RandBottom = Random.Range(0, PantsChoice.Count) ;
        //DisplayAvatar.BottomSprites = BottomChoice[RandBottom] ;
        CurrentPantsChoiceDisplay = RandBottom ;
        //DisplayAvatar.BottomColor = ColorCustomList[Random.Range(0, ColorCustomList.Count)] ;

        // Random Shoe
        int RandShoe = Random.Range(0, ShoeChoice.Count) ;
        //DisplayAvatar.ShoeSprites =  ShoeChoice[RandShoe] ;
        CurrentShoeChoiceDisplay = RandShoe ;
        //DisplayAvatar.ShoeColor = ColorCustomList[Random.Range(0, ColorCustomList.Count)] ;


        //CheckColorUsed();
        //DisplayAvatar.SkinModify();
    }
*/
    public void SubmitCharacter()
    {
        /*CloseAllPanel() ;
        FadeImage.SetActive(true);
        ChangeTitleCategories(6, "");*/

       // DisplayAvatar.gameObject.name = "Player" ;
        
        DontDestroyOnLoad(Player.gameObject);

        if(PlayerPrefs.GetInt("PlayerCustomerAsBeenVisited") == 0)
        {
            PlayerPrefs.SetInt("PlayerCustomerAsBeenVisited", 1);
        }


        StartCoroutine(WaitBeforeChangeScene());
    }

    IEnumerator WaitBeforeChangeScene()
    {
        //FadeImage.SetActive(true);
        //RideauxAnimator.SetBool("Quit Custom ?", true) ;
            yield return new WaitForSeconds(1.75f);
            //FadeImage.GetComponent<Image>().DOFade(1, 1f);
        yield return new WaitForSeconds(1.75f);
        //DisplayAvatar.InCustom = false ;
    //    DisplayAvatar.GetComponent<GridDeplacement>().enabled = true ;
  //      DisplayAvatar.GetComponent<Radio>().enabled = true;
   //         DisplayAvatar.GetComponent<PlayerProvenance>().enabled = true;
    //        DisplayAvatar.GetComponent<PlayerProvenance>().SetAllBoolToFalse();     DisplayAvatar.GetComponent<PlayerProvenance>().ProviensCharacterCustomer = true;
        SceneManager.LoadScene("GAME IN");
    }



}
