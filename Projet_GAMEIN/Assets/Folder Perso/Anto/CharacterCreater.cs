using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class CharacterCreater : MonoBehaviour
{
    [Header ("Custom Avatar")]
    [SerializeField] private PlayerDisplayer DisplayAvatar ;
    [SerializeField] private Button SubmitButton ;

    [Header ("Rotate Avatar")]
    [SerializeField] private List<GameObject> DifferentPlayerView ;
    private int ActualView = 0 ;

    [Header ("Autre Parameter")]
    [SerializeField] private GameObject FadeImage ;
    [SerializeField] private Animator RideauxAnimator ;
    

    [Header ("CustomerPanel")]
    [SerializeField] private RectTransform SkinGroup ;
    [SerializeField] private RectTransform SkinPanel ;
    [SerializeField] private RectTransform ContainerSkinPanel ;
    [SerializeField] private RectTransform FondContainerSkinPanel ;
    [SerializeField] private Slider SkinSlider ;
    private float SkinPanelHeight = 130f ;
    private float SkinPanelFondHeight = 80f ;
    
    [Space(10)]

    [SerializeField] private RectTransform HairGroup ;
    [SerializeField] private RectTransform HairPanel ;
    [SerializeField] private RectTransform ContainerHairPanel ;
    [SerializeField] private RectTransform FondContainerHairPanel ;
    [SerializeField] private List<Image> AllHairChoiceDisplayer ;
        private int CurrentHairChoiceDisplay = 0 ;
    [SerializeField] private List<Button> AllHairColorButton ;
    
    [Space(10)]

    [SerializeField] private RectTransform BodyGroup ;
    [SerializeField] private RectTransform BodyPanel ;
    [SerializeField] private RectTransform ContainerBodyPanel ;
    [SerializeField] private RectTransform FondContainerBodyPanel ;
    [SerializeField] private List<Image> AllBodyChoiceDisplayer ;
        private int CurrentBodyChoiceDisplay = 0 ;
    [SerializeField] private List<Button> AllBodyColorButton ;
    
    [Space(10)]

    [SerializeField] private RectTransform BottomGroup ;
    [SerializeField] private RectTransform BottomPanel ;
    [SerializeField] private RectTransform ContainerBottomPanel ;
    [SerializeField] private RectTransform FondContainerBottomPanel ;
    [SerializeField] private List<Image> AllBottomChoiceDisplayer ;
        private int CurrentBottomChoiceDisplay = 0 ;
    [SerializeField] private List<Button> AllBottomColorButton ;
    
    [Space(10)]

    [SerializeField] private RectTransform ShoeGroup ;
    [SerializeField] private RectTransform ShoePanel ;
    [SerializeField] private RectTransform ContainerShoePanel ;
    [SerializeField] private RectTransform FondContainerShoePanel ;
    [SerializeField] private List<Image> AllShoeChoiceDisplayer ;
        private int CurrentShoeChoiceDisplay = 0 ;
    [SerializeField] private List<Button> AllShoeColorButton ;

    [Space(10)]
    [SerializeField] private TextMeshProUGUI TitleCategories ;


    private float OpenSkinGroupHeight = 165f ;
    private float OpenChoiceGroupHeight = 195f ;
    private float CloseGroupHeight = 70f ;

    private float ChoicePanelHeight = 160f ;
    private float ChoicePanelFondHeight = 111f ;   

        private float PanelAnimationSpeed = 1f ;
        private float ClosePanelSpeed = 0.5f ;
        private float OpenPanelSpeed = 1.5f ;


    private float PanelWidth = 445f ;


    [Header ("Item Disponible")]
    [SerializeField] private List<ItemCustomer> HairChoice ;
    [SerializeField] private List<ItemCustomer> BodyChoice ;
    [SerializeField] private List<ItemCustomer> BottomChoice ;
    [SerializeField] private List<ItemCustomer> ShoeChoice ;
    [Space]
    [SerializeField] private Color ColorDoNotUse ;
    [SerializeField] private Color ColorUse ;


    enum Categories {Skin, Hair, Body, Bottom, Shoe}
    Categories Cat ; 

    [Header("Custom Color Possible")]
    public List<Color> ColorCustomList ;


    private void Awake() 
    {
        if(GameObject.Find("Player") != null)
        {
            GameObject.Find("Player").GetComponent<GridDeplacement>().enabled = false ;     
            GameObject.Find("Player").GetComponent<PlayerDisplayer>().enabled = false ;     
            DisplayAvatar.RecupExistSkin(SkinSlider);                
        }

    }
    
    private void Start() 
    {
        FadeImage.SetActive(false);
        DisplayAvatar.InCustom = true ;
    }

    void Update()
    {
        // Player Position View
        for (int i = 0; i < DifferentPlayerView.Count; i++)
        {
            if(ActualView == i) DifferentPlayerView[i].SetActive(true);
            else DifferentPlayerView[i].SetActive(false) ;
        }

        // Customisation Carrousel
        HairDisplay();
        BodyDisplay();
        BottomDisplay();
        ShoeDisplay();

        CheckColorUsed();



        // SUbmit Disable Without Name
        if(DisplayAvatar.NameAvatar == "")
        {
            SubmitButton.interactable =false ;
        }
        if(DisplayAvatar.NameAvatar != "")
        {
            SubmitButton.interactable = true ;
        }

        // Anti-Bug
     /*   if(SkinPanel.sizeDelta.y >= 69.75f && SkinPanel.sizeDelta.y <= 70.25f)
            ContainerSkinPanel.DOKill();
        if(HairPanel.sizeDelta.y >= 69.75f && HairPanel.sizeDelta.y <= 70.25f)
            ContainerHairPanel.DOKill();
        if(BodyPanel.sizeDelta.y >= 69.75f && BodyPanel.sizeDelta.y <= 70.25f)
            ContainerBodyPanel.DOKill();
        if(BottomPanel.sizeDelta.y >= 69.75f && BottomPanel.sizeDelta.y <= 70.25f)
            ContainerBottomPanel.DOKill();
        if(ShoePanel.sizeDelta.y >= 69.75f && ShoePanel.sizeDelta.y <= 70.25f)
            ContainerShoePanel.DOKill();*/
        //TitleCategories.gameObject.transform.parent.GetComponent<Image>().enabled = true ;
    }

    
    // Rotate Avatar Function
    public void PreviousProfilView()
    {
        if(ActualView == 0)
            ActualView = 3 ;
        else
            ActualView -- ;
    }
    public void NextProfilView()
    {
        if(ActualView == 3)
            ActualView = 0 ;
        else
            ActualView ++ ;
    }



    // Animation Panel
    public void InterractSkinButton(int StateChoice)
    {
        if(StateChoice == 0)
        {
            SkinPanel.DOSizeDelta(new Vector2(PanelWidth,0f), PanelAnimationSpeed);  
            FondContainerSkinPanel.DOSizeDelta(new Vector2(144f, 0f), PanelAnimationSpeed).OnComplete(() => {FondContainerSkinPanel.sizeDelta = new Vector2(FondContainerSkinPanel.sizeDelta.x, 20f);});
            SkinGroup.DOSizeDelta(new Vector2(SkinGroup.sizeDelta.x, CloseGroupHeight), PanelAnimationSpeed);
            ContainerSkinPanel.DOKill();
            ContainerSkinPanel.DOScale(Vector3.zero, ClosePanelSpeed);                
        }
        if(StateChoice == 1)
        {
            if(SkinPanel.sizeDelta.y > 70f)
            {
                ChangeTitleCategories(0);   
                InterractSkinButton(0);
            } else {
                ChangeTitleCategories(1);   
                SkinPanel.DOSizeDelta(new Vector2(PanelWidth, SkinPanelHeight), PanelAnimationSpeed);
                FondContainerSkinPanel.DOSizeDelta(new Vector2(144f,SkinPanelFondHeight), PanelAnimationSpeed);
                SkinGroup.DOSizeDelta(new Vector2(SkinGroup.sizeDelta.x, OpenSkinGroupHeight), PanelAnimationSpeed).OnComplete(() => {SkinGroup.transform.SetSiblingIndex(5); });
                ContainerSkinPanel.DOKill();
                ContainerSkinPanel.DOScale(Vector3.one, OpenPanelSpeed);   
            }

            InterractHairButton(0);
            InterractBodyButton(0);
            InterractBottomButton(0);
            InterractShoeButton(0);
        }
    }
    public void InterractHairButton(int StateChoice)
    {
        if(StateChoice == 0)
        {
            HairPanel.DOSizeDelta(new Vector2(PanelWidth,0f), PanelAnimationSpeed);  
            FondContainerHairPanel.DOSizeDelta(new Vector2(144f, 0f), PanelAnimationSpeed).OnComplete(() => {FondContainerHairPanel.sizeDelta = new Vector2(FondContainerHairPanel.sizeDelta.x, 20f);});
            HairGroup.DOSizeDelta(new Vector2(HairGroup.sizeDelta.x, CloseGroupHeight), PanelAnimationSpeed);
            ContainerHairPanel.DOKill();
            ContainerHairPanel.DOScale(Vector3.zero, ClosePanelSpeed);                 
        }
        if(StateChoice == 1)
        {
            if(HairPanel.sizeDelta.y > 70)
            {
                ChangeTitleCategories(0);   
                InterractHairButton(0);
            } else {
                ChangeTitleCategories(2);   
                HairPanel.DOSizeDelta(new Vector2(PanelWidth, ChoicePanelHeight), PanelAnimationSpeed);
                FondContainerHairPanel.DOSizeDelta(new Vector2(144f, ChoicePanelFondHeight), PanelAnimationSpeed);
                HairGroup.DOSizeDelta(new Vector2(HairGroup.sizeDelta.x, OpenChoiceGroupHeight), PanelAnimationSpeed).OnComplete(() => {HairGroup.transform.SetSiblingIndex(5); });
                ContainerHairPanel.DOKill();
                ContainerHairPanel.DOScale(Vector3.one, OpenPanelSpeed);            
            }
            
            InterractSkinButton(0);

            InterractBodyButton(0);
            InterractBottomButton(0);
            InterractShoeButton(0);
        }
    }
    public void InterractBodyButton(int StateChoice)
    {
        if(StateChoice == 0)
        {
            BodyPanel.DOSizeDelta(new Vector2(PanelWidth, 0f), PanelAnimationSpeed);  
            FondContainerBodyPanel.DOSizeDelta(new Vector2(144f, 0f), PanelAnimationSpeed).OnComplete(() => {FondContainerBodyPanel.sizeDelta = new Vector2(FondContainerBodyPanel.sizeDelta.x, 20f);}); 
            BodyGroup.DOSizeDelta(new Vector2(BodyGroup.sizeDelta.x, CloseGroupHeight), PanelAnimationSpeed);
            ContainerBodyPanel.DOKill();
            ContainerBodyPanel.DOScale(Vector3.zero, ClosePanelSpeed);                
        }
        if(StateChoice == 1)
        {
            if(BodyPanel.sizeDelta.y > 70)
            {
                ChangeTitleCategories(0);   
                InterractBodyButton(0);
            } else {
                ChangeTitleCategories(3);   
                BodyPanel.DOSizeDelta(new Vector2(PanelWidth, ChoicePanelHeight),PanelAnimationSpeed);
                FondContainerBodyPanel.DOSizeDelta(new Vector2(144f, ChoicePanelFondHeight), PanelAnimationSpeed); 
                BodyGroup.DOSizeDelta(new Vector2(BodyGroup.sizeDelta.x, OpenChoiceGroupHeight), PanelAnimationSpeed).OnComplete(() => {BodyGroup.transform.SetSiblingIndex(5); });
                ContainerBodyPanel.DOKill();
                ContainerBodyPanel.DOScale(Vector3.one, OpenPanelSpeed);                
            }

            InterractSkinButton(0);
            InterractHairButton(0);

            InterractBottomButton(0);
            InterractShoeButton(0);
        }
    }
    public void InterractBottomButton(int StateChoice)
    {
        if(StateChoice == 0)
        {
            BottomPanel.DOSizeDelta(new Vector2(PanelWidth,0f), PanelAnimationSpeed);   
            FondContainerBottomPanel.DOSizeDelta(new Vector2(144f, 0f), PanelAnimationSpeed).OnComplete(() => {FondContainerBottomPanel.sizeDelta = new Vector2(FondContainerBottomPanel.sizeDelta.x, 20f);});
            BottomGroup.DOSizeDelta(new Vector2(BottomGroup.sizeDelta.x, CloseGroupHeight), PanelAnimationSpeed);
            ContainerBottomPanel.DOKill();
            ContainerBottomPanel.DOScale(Vector3.zero, ClosePanelSpeed);                 
        }
        if(StateChoice == 1)
        {
            if(BottomPanel.sizeDelta.y > 70)
            {
                ChangeTitleCategories(0);   
                InterractBottomButton(0);
            } else {
                ChangeTitleCategories(4);   
                BottomPanel.DOSizeDelta(new Vector2(PanelWidth, ChoicePanelHeight), PanelAnimationSpeed);
                FondContainerBottomPanel.DOSizeDelta(new Vector2(144f, ChoicePanelFondHeight), PanelAnimationSpeed); 
                BottomGroup.DOSizeDelta(new Vector2(BottomGroup.sizeDelta.x, OpenChoiceGroupHeight), PanelAnimationSpeed).OnComplete(() => {BottomGroup.transform.SetSiblingIndex(5); });
                ContainerBottomPanel.DOKill();
                ContainerBottomPanel.DOScale(Vector3.one, OpenPanelSpeed);      
            }

            InterractSkinButton(0);
            InterractHairButton(0);
            InterractBodyButton(0);

            InterractShoeButton(0);
        }
    }
    public void InterractShoeButton(int StateChoice)
    {
        if(StateChoice == 0)
        {
            ShoePanel.DOSizeDelta(new Vector2(PanelWidth,0f), PanelAnimationSpeed); 
            FondContainerShoePanel.DOSizeDelta(new Vector2(144f, 0f), PanelAnimationSpeed).OnComplete(() => {FondContainerShoePanel.sizeDelta = new Vector2(FondContainerShoePanel.sizeDelta.x, 20f);});  
            ShoeGroup.DOSizeDelta(new Vector2(ShoeGroup.sizeDelta.x, CloseGroupHeight), PanelAnimationSpeed);
            ContainerShoePanel.DOKill();
            ContainerShoePanel.DOScale(Vector3.zero, ClosePanelSpeed);               
        }
        if(StateChoice == 1)
        {
            if(ShoePanel.sizeDelta.y > 70)
            {
                ChangeTitleCategories(0);                
                InterractShoeButton(0);
            } else {
                ChangeTitleCategories(5);   
                ShoePanel.DOSizeDelta(new Vector2(PanelWidth, ChoicePanelHeight), PanelAnimationSpeed);
                FondContainerShoePanel.DOSizeDelta(new Vector2(144f, ChoicePanelFondHeight), PanelAnimationSpeed); 
                ShoeGroup.DOSizeDelta(new Vector2(ShoeGroup.sizeDelta.x, OpenChoiceGroupHeight), PanelAnimationSpeed).OnComplete(() => {ShoeGroup.transform.SetSiblingIndex(5); });
                ContainerShoePanel.DOKill();
                ContainerShoePanel.DOScale(Vector3.one, OpenPanelSpeed);  
            }

            InterractSkinButton(0);
            InterractHairButton(0);
            InterractBodyButton(0);
            InterractBottomButton(0);
        }
    }
    void ChangeTitleCategories(int NextCat)
    {
        TitleCategories.GetComponent<RectTransform>().DOScaleY(0, PanelAnimationSpeed/4)
            .OnComplete(() => {
                if(NextCat == 0)
                    {TitleCategories.text = "Choissisez une section" ;}
                if(NextCat == 1)
                    {TitleCategories.text = "Couleur de peau" ;}
                if(NextCat == 2)
                    {TitleCategories.text = "Cheveux" ;}
                if(NextCat == 3)
                    {TitleCategories.text = "Hauts" ;}
                if(NextCat == 4)
                    {TitleCategories.text = "Bas" ;}
                if(NextCat == 5)
                    {TitleCategories.text = "Chaussures" ;}
                if(NextCat == 6)
                    {TitleCategories.text = "Aurevoir" ;}

                TitleCategories.GetComponent<RectTransform>().DOScaleY(0.75f, PanelAnimationSpeed/4);
            });
    }

    public void CloseAllPanel()
    {
            InterractSkinButton(0);
            InterractHairButton(0);
            InterractBodyButton(0);
            InterractBottomButton(0);
            InterractShoeButton(0);
    }


    // Panel Custom
    public void SkinColor(float SliderValue)
    {   DisplayAvatar.SkinColorPourcentage = SliderValue ;    CheckColorUsed(); }

    public void ChangeHairColor(int NumColorList)
    {    DisplayAvatar.HairColor = ColorCustomList[NumColorList] ;  CheckColorUsed(); }

    public void ChangeBodyColor(int NumColorList)
    {    DisplayAvatar.BodyColor = ColorCustomList[NumColorList] ;  CheckColorUsed(); }

    public void ChangeBottomColor(int NumColorList)
    {   DisplayAvatar.BottomColor = ColorCustomList[NumColorList] ;  CheckColorUsed(); }

    public void ChangeShoeColor(int NumColorList)
    {   DisplayAvatar.ShoeColor = ColorCustomList[NumColorList] ;   CheckColorUsed(); }


    void HairDisplay()
    {
        if(CurrentHairChoiceDisplay == 0)
        {
            AllHairChoiceDisplayer[0].sprite = HairChoice[HairChoice.Count - 1].DisplayCustomisation ;
            AllHairChoiceDisplayer[2].sprite = HairChoice[CurrentHairChoiceDisplay + 1].DisplayCustomisation ;
        }

        AllHairChoiceDisplayer[1].sprite = HairChoice[CurrentHairChoiceDisplay].DisplayCustomisation ;

        if(CurrentHairChoiceDisplay == HairChoice.Count - 1)
        {
            AllHairChoiceDisplayer[0].sprite = HairChoice[CurrentHairChoiceDisplay - 1].DisplayCustomisation ;
            AllHairChoiceDisplayer[2].sprite = HairChoice[0].DisplayCustomisation ;
        }

        if((CurrentHairChoiceDisplay != 0) && (CurrentHairChoiceDisplay != HairChoice.Count - 1))
        {
            AllHairChoiceDisplayer[0].sprite = HairChoice[CurrentHairChoiceDisplay - 1].DisplayCustomisation ;
            AllHairChoiceDisplayer[2].sprite = HairChoice[CurrentHairChoiceDisplay + 1].DisplayCustomisation ;
        }

        DisplayAvatar.HairSprites = HairChoice[CurrentHairChoiceDisplay] ;
    }
    public void NextHair()
    {
        CurrentHairChoiceDisplay ++ ;
        if(CurrentHairChoiceDisplay >= HairChoice.Count)
        CurrentHairChoiceDisplay = 0 ;
        DisplayAvatar.SkinModify();
    }
    public void PreviousHair()
    {
        CurrentHairChoiceDisplay -- ;
        if(CurrentHairChoiceDisplay < 0)
        CurrentHairChoiceDisplay = HairChoice.Count - 1 ;
        DisplayAvatar.SkinModify();
    }



    void BodyDisplay()
    {
        if(CurrentBodyChoiceDisplay == 0)
        {
            AllBodyChoiceDisplayer[0].sprite = BodyChoice[BodyChoice.Count - 1].DisplayCustomisation ;
            AllBodyChoiceDisplayer[2].sprite = BodyChoice[CurrentBodyChoiceDisplay + 1].DisplayCustomisation ;
        }

        AllBodyChoiceDisplayer[1].sprite = BodyChoice[CurrentBodyChoiceDisplay].DisplayCustomisation ;

        if(CurrentBodyChoiceDisplay == BodyChoice.Count - 1)
        {
            AllBodyChoiceDisplayer[0].sprite = BodyChoice[CurrentBodyChoiceDisplay - 1].DisplayCustomisation ;
            AllBodyChoiceDisplayer[2].sprite = BodyChoice[0].DisplayCustomisation ;
        }

        if((CurrentBodyChoiceDisplay != 0) && (CurrentBodyChoiceDisplay != BodyChoice.Count - 1))
        {
            AllBodyChoiceDisplayer[0].sprite = BodyChoice[CurrentBodyChoiceDisplay - 1].DisplayCustomisation ;
            AllBodyChoiceDisplayer[2].sprite = BodyChoice[CurrentBodyChoiceDisplay + 1].DisplayCustomisation ;
        }

        DisplayAvatar.BodySprites = BodyChoice[CurrentBodyChoiceDisplay] ;
    }
    public void NextBody()
    {
        CurrentBodyChoiceDisplay ++ ;
        if(CurrentBodyChoiceDisplay >= BodyChoice.Count)
        CurrentBodyChoiceDisplay = 0 ;
        DisplayAvatar.SkinModify();
    }
    public void PreviousBody()
    {
        CurrentBodyChoiceDisplay -- ;
        if(CurrentBodyChoiceDisplay < 0)
        CurrentBodyChoiceDisplay = BodyChoice.Count - 1 ;
        DisplayAvatar.SkinModify();
    }



    void BottomDisplay()
    {
        if(CurrentBottomChoiceDisplay == 0)
        {
            AllBottomChoiceDisplayer[0].sprite = BottomChoice[BottomChoice.Count - 1].DisplayCustomisation ;
            AllBottomChoiceDisplayer[2].sprite = BottomChoice[CurrentBottomChoiceDisplay + 1].DisplayCustomisation ;
        }

        AllBottomChoiceDisplayer[1].sprite = BottomChoice[CurrentBottomChoiceDisplay].DisplayCustomisation ;

        if(CurrentBottomChoiceDisplay == BottomChoice.Count - 1)
        {
            AllBottomChoiceDisplayer[0].sprite = BottomChoice[CurrentBottomChoiceDisplay - 1].DisplayCustomisation ;
            AllBottomChoiceDisplayer[2].sprite = BottomChoice[0].DisplayCustomisation ;
        }

        if((CurrentBottomChoiceDisplay != 0) && (CurrentBottomChoiceDisplay != BottomChoice.Count - 1))
        {
            AllBottomChoiceDisplayer[0].sprite = BottomChoice[CurrentBottomChoiceDisplay - 1].DisplayCustomisation ;
            AllBottomChoiceDisplayer[2].sprite = BottomChoice[CurrentBottomChoiceDisplay + 1].DisplayCustomisation ;
        }

        DisplayAvatar.BottomSprites = BottomChoice[CurrentBottomChoiceDisplay] ;
    }
    public void NextBottom()
    {
        CurrentBottomChoiceDisplay ++ ;
        if(CurrentBottomChoiceDisplay >= BottomChoice.Count)
        CurrentBottomChoiceDisplay = 0 ;
        DisplayAvatar.SkinModify();
    }
    public void PreviousBottom()
    {
        CurrentBottomChoiceDisplay -- ;
        if(CurrentBottomChoiceDisplay < 0)
        CurrentBottomChoiceDisplay = BottomChoice.Count - 1 ;
        DisplayAvatar.SkinModify();
    }



    void ShoeDisplay()
    {
        if(CurrentShoeChoiceDisplay == 0)
        {
            AllShoeChoiceDisplayer[0].sprite = ShoeChoice[ShoeChoice.Count - 1].DisplayCustomisation ;
            AllShoeChoiceDisplayer[2].sprite = ShoeChoice[CurrentShoeChoiceDisplay + 1].DisplayCustomisation ;
        }

        AllShoeChoiceDisplayer[1].sprite = ShoeChoice[CurrentShoeChoiceDisplay].DisplayCustomisation ;

        if(CurrentShoeChoiceDisplay == ShoeChoice.Count - 1)
        {
            AllShoeChoiceDisplayer[0].sprite = ShoeChoice[CurrentShoeChoiceDisplay - 1].DisplayCustomisation ;
            AllShoeChoiceDisplayer[2].sprite = ShoeChoice[0].DisplayCustomisation ;
        }

        if((CurrentShoeChoiceDisplay != 0) && (CurrentShoeChoiceDisplay != ShoeChoice.Count - 1))
        {
            AllShoeChoiceDisplayer[0].sprite = ShoeChoice[CurrentShoeChoiceDisplay - 1].DisplayCustomisation ;
            AllShoeChoiceDisplayer[2].sprite = ShoeChoice[CurrentShoeChoiceDisplay + 1].DisplayCustomisation ;
        }

        DisplayAvatar.ShoeSprites = ShoeChoice[CurrentShoeChoiceDisplay] ;
    }
    public void NextShoe()
    {
        CurrentShoeChoiceDisplay ++ ;
        if(CurrentShoeChoiceDisplay >= ShoeChoice.Count)
        CurrentShoeChoiceDisplay = 0 ;
        DisplayAvatar.SkinModify();
    }
    public void PreviousShoe()
    {
        CurrentShoeChoiceDisplay -- ;
        if(CurrentShoeChoiceDisplay < 0)
        CurrentShoeChoiceDisplay = ShoeChoice.Count - 1 ;
        DisplayAvatar.SkinModify();
    }







    // Check Color
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





    // Final Button
    public void RandomCharacter()
    {
        // Random Skin
        DisplayAvatar.SkinColorPourcentage = Random.Range(0f, 1f);
        SkinSlider.value = DisplayAvatar.SkinColorPourcentage ;

        // Random Hair
        int RandHair = Random.Range(0, HairChoice.Count) ;
        DisplayAvatar.HairSprites = HairChoice[RandHair] ;
        CurrentHairChoiceDisplay = RandHair ;
        DisplayAvatar.HairColor = ColorCustomList[Random.Range(0, ColorCustomList.Count)] ;

        // Random Body
        int RandBody = Random.Range(0, BodyChoice.Count) ;
        DisplayAvatar.BodySprites = BodyChoice[RandBody] ;
        CurrentBodyChoiceDisplay = RandBody ;
        DisplayAvatar.BodyColor = ColorCustomList[Random.Range(0, ColorCustomList.Count)] ;

        // Random Bottom
        int RandBottom = Random.Range(0, BottomChoice.Count) ;
        DisplayAvatar.BottomSprites = BottomChoice[RandBottom] ;
        CurrentBottomChoiceDisplay = RandBottom ;
        DisplayAvatar.BottomColor = ColorCustomList[Random.Range(0, ColorCustomList.Count)] ;

        // Random Shoe
        int RandShoe = Random.Range(0, ShoeChoice.Count) ;
        DisplayAvatar.ShoeSprites =  ShoeChoice[RandShoe] ;
        CurrentShoeChoiceDisplay = RandShoe ;
        DisplayAvatar.ShoeColor = ColorCustomList[Random.Range(0, ColorCustomList.Count)] ;


        CheckColorUsed();
        DisplayAvatar.SkinModify();
    }

    public void SubmitCharacter()
    {
        CloseAllPanel() ;
        FadeImage.SetActive(true);
        ChangeTitleCategories(6);

        DisplayAvatar.gameObject.name = "Player" ;
        PrefabUtility.SaveAsPrefabAsset(DisplayAvatar.gameObject, "Assets/Final/Prefab/Player.prefab") ;
        DontDestroyOnLoad(DisplayAvatar.gameObject);

        if(PlayerPrefs.GetInt("PlayerCustomerAsBeenVisited") == 0)
        {
            PlayerPrefs.SetInt("PlayerCustomerAsBeenVisited", 1);
        }


        StartCoroutine(WaitBeforeChangeScene());
    }

    IEnumerator WaitBeforeChangeScene()
    {
        FadeImage.SetActive(true);
        RideauxAnimator.SetBool("Quit Custom ?", true) ;
        yield return new WaitForSeconds(1.75f);
        DisplayAvatar.InCustom = false ;
        DisplayAvatar.GetComponent<GridDeplacement>().enabled = true ;
        SceneManager.LoadScene("Main");
    }
}
