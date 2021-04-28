using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class CharacterCreater : MonoBehaviour
{
    [Header ("Custom Avatar")]
    [SerializeField] private PlayerDisplayer DisplayAvatar ;


    [Header ("Rotate Avatar")]
    [SerializeField] private List<GameObject> DifferentPlayerView ;
    private int ActualView = 0 ;
    

    [Header ("CustomerPanel")]
    [SerializeField] private RectTransform SkinPanel ;
    [SerializeField] private RectTransform ContainerSkinPanel ;
    [SerializeField] private Slider SkinSlider ;
    private float SkinPanelHeight = 75f ;
    [Space]
    [SerializeField] private RectTransform HairPanel ;
    [SerializeField] private RectTransform ContainerHairPanel ;
    [SerializeField] private List<Button> AllHairChoiceButton ;
    [SerializeField] private List<Button> AllHairColorButton ;
    [Space]
    private float HairPanelHeight = 125f ;
    [Space]
    [SerializeField] private RectTransform BodyPanel ;
    [SerializeField] private RectTransform ContainerBodyPanel ;
    [SerializeField] private List<Button> AllBodyChoiceButton ;
    [SerializeField] private List<Button> AllBodyColorButton ;
    private float BodyPanelHeight = 125f ;
    [Space]
    [SerializeField] private RectTransform BottomPanel ;
    [SerializeField] private RectTransform ContainerBottomPanel ;
    [SerializeField] private List<Button> AllBottomChoiceButton ;
    [SerializeField] private List<Button> AllBottomColorButton ;
    private float BottomPanelHeight = 125f ;
    [Space]
    [SerializeField] private RectTransform ShoePanel ;
    [SerializeField] private RectTransform ContainerShoePanel ;
    [SerializeField] private List<Button> AllShoeChoiceButton ;
    [SerializeField] private List<Button> AllShoeColorButton ;
    private float ShoePanelHeight = 125f ;

    private float PanelWidth = 445f ;


    [Header ("Item Disponible")]
    [SerializeField] private List<ItemCustomer> HairChoice ;
    [SerializeField] private List<ItemCustomer> BodyChoice ;
    [SerializeField] private List<ItemCustomer> BottomChoice ;
    [SerializeField] private List<ItemCustomer> ShoeChoice ;    


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
    
    private void Start() {
        CheckAllHairCustomButton();
        CheckAllBodyCustomButton();
        CheckAllBottomCustomButton();
        //CheckAllShoeCustomButton();
    }

    void Update()
    {
        // Player Position View
        for (int i = 0; i < DifferentPlayerView.Count; i++)
        {
            if(ActualView == i) DifferentPlayerView[i].SetActive(true);
            else DifferentPlayerView[i].SetActive(false) ;
        }
    }


    void CheckAllHairCustomButton()
    {
        // Interractibility Button Hair
        for (int i = 0; i < AllHairChoiceButton.Count; i++)
        {
            if(HairChoice[i] == DisplayAvatar.HairSprites)
                AllHairChoiceButton[i].interactable = false ;
            if(HairChoice[i] != DisplayAvatar.HairSprites)
                AllHairChoiceButton[i].interactable = true ;
        }

        if(DisplayAvatar.HairSprites == HairChoice[0])
        {
            for (int i = 0; i < AllHairColorButton.Count; i++)
            {   AllHairColorButton[i].interactable = true ; }
        }
        if(DisplayAvatar.HairSprites != HairChoice[0])
        {
            for (int i = 0; i < AllHairColorButton.Count; i++)
            {
                if(ColorCustomList[i] == DisplayAvatar.HairColor)
                    AllHairColorButton[i].interactable = false ;
                if(ColorCustomList[i] != DisplayAvatar.HairColor)
                    AllHairColorButton[i].interactable = true ;
            }
        }
    }


    void CheckAllBodyCustomButton()
    {
        // Interractibility Button Body
        for (int i = 0; i < AllBodyChoiceButton.Count; i++)
        {
            if(BodyChoice[i] == DisplayAvatar.BodySprites)
                AllBodyChoiceButton[i].interactable = false ;
            if(BodyChoice[i] != DisplayAvatar.BodySprites)
                AllBodyChoiceButton[i].interactable = true ;
        }

        if(DisplayAvatar.BodySprites == BodyChoice[0])
        {
            for (int i = 0; i < AllBodyColorButton.Count; i++)
            {   AllBodyColorButton[i].interactable = true ; }
        }
        if(DisplayAvatar.BodySprites != BodyChoice[0])
        {
            for (int i = 0; i < AllBodyColorButton.Count; i++)
            {
                if(ColorCustomList[i] == DisplayAvatar.BodyColor)
                    AllBodyColorButton[i].interactable = false ;
                if(ColorCustomList[i] != DisplayAvatar.BodyColor)
                    AllBodyColorButton[i].interactable = true ;
            }            
        }

    }


    void CheckAllBottomCustomButton()
    {
        // Interractibility Button Bottom
        for (int i = 0; i < AllBottomChoiceButton.Count; i++)
        {
            if(BottomChoice[i] == DisplayAvatar.BottomSprites)
                AllBottomChoiceButton[i].interactable = false ;
            if(BottomChoice[i] != DisplayAvatar.BottomSprites)
                AllBottomChoiceButton[i].interactable = true ;
        }

        if(DisplayAvatar.BottomSprites == BottomChoice[0])
        {
            for (int i = 0; i < AllBottomColorButton.Count; i++)
            {   AllBottomColorButton[i].interactable = true ; }
        }
        if(DisplayAvatar.BottomSprites != BottomChoice[0])
        {
            for (int i = 0; i < AllBottomColorButton.Count; i++)
            {
                if(ColorCustomList[i] == DisplayAvatar.BottomColor)
                    AllBottomColorButton[i].interactable = false ;
                if(ColorCustomList[i] != DisplayAvatar.BottomColor)
                    AllBottomColorButton[i].interactable = true ;
            }
        }
    }


    void CheckAllShoeCustomButton()
    {
        // Interractibility Button Shoe
        for (int i = 0; i < AllShoeChoiceButton.Count; i++)
        {
            if(ShoeChoice[i] == DisplayAvatar.ShoeSprites)
                AllShoeChoiceButton[i].interactable = false ;
            if(ShoeChoice[i] != DisplayAvatar.ShoeSprites)
                AllShoeChoiceButton[i].interactable = true ;
        }

        if(DisplayAvatar.ShoeSprites == ShoeChoice[0])
        {
            for (int i = 0; i < AllShoeColorButton.Count; i++)
            {   AllShoeColorButton[i].interactable = true ; }
        }
        if(DisplayAvatar.ShoeSprites != ShoeChoice[0])
        {
            for (int i = 0; i < AllShoeColorButton.Count; i++)
            {
                if(ColorCustomList[i] == DisplayAvatar.ShoeColor)
                    AllShoeColorButton[i].interactable = false ;
                if(ColorCustomList[i] != DisplayAvatar.ShoeColor)
                    AllShoeColorButton[i].interactable = true ;
            }
        }
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
    public void InterractSkinButton()
    {
        if(SkinPanel.sizeDelta.y != 0)
        {
            SkinPanel.DOSizeDelta(new Vector2(PanelWidth,0f),1f);  
            ContainerSkinPanel.DOScale(Vector3.zero, 1f);    
        }
        if(SkinPanel.sizeDelta.y == 0)
        {
            SkinPanel.DOSizeDelta(new Vector2(PanelWidth, SkinPanelHeight),1f);
            HairPanel.DOSizeDelta(new Vector2(PanelWidth,0f),1f);
            BodyPanel.DOSizeDelta(new Vector2(PanelWidth,0f),1f);
            BottomPanel.DOSizeDelta(new Vector2(PanelWidth,0f),1f);
            ShoePanel.DOSizeDelta(new Vector2(PanelWidth,0f),1f);        
            ContainerSkinPanel.DOScale(Vector3.one, 1f);  
            ContainerHairPanel.DOScale(Vector3.zero, 1f);  
            ContainerBodyPanel.DOScale(Vector3.zero, 1f);  
            ContainerBottomPanel.DOScale(Vector3.zero, 1f);  
            ContainerShoePanel.DOScale(Vector3.zero, 1f);  
        }
    }
    public void InterractHairButton()
    {
        if(HairPanel.sizeDelta.y != 0)
        {
            HairPanel.DOSizeDelta(new Vector2(PanelWidth,0f),1f);    
            ContainerHairPanel.DOScale(Vector3.zero, 1f);  

        }
        if(HairPanel.sizeDelta.y == 0)
        {
            SkinPanel.DOSizeDelta(new Vector2(PanelWidth,0f),1f);
            HairPanel.DOSizeDelta(new Vector2(PanelWidth, HairPanelHeight),1f);
            BodyPanel.DOSizeDelta(new Vector2(PanelWidth,0f),1f);
            BottomPanel.DOSizeDelta(new Vector2(PanelWidth,0f),1f);
            ShoePanel.DOSizeDelta(new Vector2(PanelWidth,0f),1f);

            ContainerSkinPanel.DOScale(Vector3.zero, 1f);  
            ContainerHairPanel.DOScale(Vector3.one, 1f);  
            ContainerBodyPanel.DOScale(Vector3.zero, 1f);  
            ContainerBottomPanel.DOScale(Vector3.zero, 1f);  
            ContainerShoePanel.DOScale(Vector3.zero, 1f);  
        }
    }
    public void InterractBodyButton()
    {
        if(BodyPanel.sizeDelta.y != 0)
        {
            BodyPanel.DOSizeDelta(new Vector2(PanelWidth,0f),1f);   
            ContainerBodyPanel.DOScale(Vector3.zero, 1f);    
        }
        if(BodyPanel.sizeDelta.y == 0)
        {
            SkinPanel.DOSizeDelta(new Vector2(PanelWidth,0f),1f);
            HairPanel.DOSizeDelta(new Vector2(PanelWidth,0f),1f);
            BodyPanel.DOSizeDelta(new Vector2(PanelWidth, BodyPanelHeight),1f);
            BottomPanel.DOSizeDelta(new Vector2(PanelWidth,0f),1f);
            ShoePanel.DOSizeDelta(new Vector2(PanelWidth,0f),1f);

            ContainerSkinPanel.DOScale(Vector3.zero, 1f);  
            ContainerHairPanel.DOScale(Vector3.zero, 1f);  
            ContainerBodyPanel.DOScale(Vector3.one, 1f);  
            ContainerBottomPanel.DOScale(Vector3.zero, 1f);  
            ContainerShoePanel.DOScale(Vector3.zero, 1f);  
        }
    }
    public void InterractBottomButton()
    {
        if(BottomPanel.sizeDelta.y != 0)
        {
            BottomPanel.DOSizeDelta(new Vector2(PanelWidth,0f),1f);     
            ContainerBottomPanel.DOScale(Vector3.zero, 1f);  
        }
        if(BottomPanel.sizeDelta.y == 0)
        {
            SkinPanel.DOSizeDelta(new Vector2(PanelWidth,0f),1f);
            HairPanel.DOSizeDelta(new Vector2(PanelWidth,0f),1f);
            BodyPanel.DOSizeDelta(new Vector2(PanelWidth,0f),1f);
            BottomPanel.DOSizeDelta(new Vector2(PanelWidth, BottomPanelHeight),1f);
            ShoePanel.DOSizeDelta(new Vector2(PanelWidth,0f),1f);

            ContainerSkinPanel.DOScale(Vector3.zero, 1f);  
            ContainerHairPanel.DOScale(Vector3.zero, 1f);  
            ContainerBodyPanel.DOScale(Vector3.zero, 1f);  
            ContainerBottomPanel.DOScale(Vector3.one, 1f);  
            ContainerShoePanel.DOScale(Vector3.zero, 1f);  
        }
    }
    public void InterractShoeButton()
    {
        if(ShoePanel.sizeDelta.y != 0)
        {
            ShoePanel.DOSizeDelta(new Vector2(PanelWidth,0f),1f); 
            ContainerShoePanel.DOScale(Vector3.zero, 1f);   
        }
        if(ShoePanel.sizeDelta.y == 0)
        {
            SkinPanel.DOSizeDelta(new Vector2(PanelWidth,0f),1f);
            HairPanel.DOSizeDelta(new Vector2(PanelWidth,0f),1f);
            BodyPanel.DOSizeDelta(new Vector2(PanelWidth,0f),1f);
            BottomPanel.DOSizeDelta(new Vector2(PanelWidth,0f),1f);
            ShoePanel.DOSizeDelta(new Vector2(PanelWidth, ShoePanelHeight),1f);

            ContainerSkinPanel.DOScale(Vector3.zero, 1f);  
            ContainerHairPanel.DOScale(Vector3.zero, 1f);  
            ContainerBodyPanel.DOScale(Vector3.zero, 1f);  
            ContainerBottomPanel.DOScale(Vector3.zero, 1f);  
            ContainerShoePanel.DOScale(Vector3.one, 1f); 
        }
    }
    public void CloseAllPanel()
    {
            SkinPanel.DOSizeDelta(new Vector2(PanelWidth, 0f),1f);
            HairPanel.DOSizeDelta(new Vector2(PanelWidth,0f),1f);
            BodyPanel.DOSizeDelta(new Vector2(PanelWidth,0f),1f);
            BottomPanel.DOSizeDelta(new Vector2(PanelWidth,0f),1f);
            ShoePanel.DOSizeDelta(new Vector2(PanelWidth,0f),1f);        
            ContainerSkinPanel.DOScale(Vector3.zero, 1f);  
            ContainerHairPanel.DOScale(Vector3.zero, 1f);  
            ContainerBodyPanel.DOScale(Vector3.zero, 1f);  
            ContainerBottomPanel.DOScale(Vector3.zero, 1f);  
            ContainerShoePanel.DOScale(Vector3.zero, 1f);      
    }


    // Panel Custom
    public void SkinColor(float SliderValue)
    {   DisplayAvatar.SkinColorPourcentage = SliderValue ;     }
    
    public void ChangeHair(ItemCustomer HairChoice)
    {    DisplayAvatar.HairSprites = HairChoice ;   CheckAllHairCustomButton();    }
    public void ChangeHairColor(int NumColorList)
    {    DisplayAvatar.HairColor = ColorCustomList[NumColorList] ;  CheckAllHairCustomButton();  }
    
    public void ChangeBody(ItemCustomer BodyChoice)
    {   DisplayAvatar.BodySprites = BodyChoice ;    CheckAllBodyCustomButton();    }
    public void ChangeBodyColor(int NumColorList)
    {    DisplayAvatar.BodyColor = ColorCustomList[NumColorList] ;  CheckAllBodyCustomButton();   }

    public void ChangeBottom(ItemCustomer BottomChoice)
    {   DisplayAvatar.BottomSprites = BottomChoice ;    CheckAllBottomCustomButton();    }
    public void ChangeBottomColor(int NumColorList)
    {   DisplayAvatar.BottomColor = ColorCustomList[NumColorList] ; CheckAllBottomCustomButton();     }

    public void ChangeShoe(ItemCustomer ShoeChoice)
    {   DisplayAvatar.ShoeSprites = ShoeChoice ;    /*CheckAllShoeCustomButton(); */   }
    public void ChangeShoeColor(int NumColorList)
    {   DisplayAvatar.ShoeColor = ColorCustomList[NumColorList] ;   /*CheckAllShoeCustomButton();*/   }




    // Final Button
    public void RandomCharacter()
    {
        // Random Skin
        DisplayAvatar.SkinColorPourcentage = Random.Range(0f, 1f);
        SkinSlider.value = DisplayAvatar.SkinColorPourcentage ;

        // Random Hair
        DisplayAvatar.HairSprites = HairChoice[Random.Range(0, HairChoice.Count)] ;
        DisplayAvatar.HairColor = ColorCustomList[Random.Range(0, ColorCustomList.Count)] ;

        // Random Body
        DisplayAvatar.BodySprites = BodyChoice[Random.Range(0, BodyChoice.Count)] ;
        DisplayAvatar.BodyColor = ColorCustomList[Random.Range(0, ColorCustomList.Count)] ;

        // Random Bottom
        DisplayAvatar.BottomSprites = BottomChoice[Random.Range(0, BottomChoice.Count)] ;
        DisplayAvatar.BottomColor = ColorCustomList[Random.Range(0, ColorCustomList.Count)] ;

        // Random Shoe
        DisplayAvatar.ShoeSprites = ShoeChoice[Random.Range(0, ShoeChoice.Count)] ;
        DisplayAvatar.ShoeColor = ColorCustomList[Random.Range(0, ColorCustomList.Count)] ;


        CheckAllHairCustomButton();
        CheckAllBodyCustomButton();
        CheckAllBottomCustomButton();
        //CheckAllShoeCustomButton();
    }

    public void SubmitCharacter()
    {
        DisplayAvatar.gameObject.name = "Player" ;
        PrefabUtility.SaveAsPrefabAsset(DisplayAvatar.gameObject, "Assets/Final/Prefab/Player.prefab") ;
        DisplayAvatar.GetComponent<GridDeplacement>().enabled = true ;
        DontDestroyOnLoad(DisplayAvatar.gameObject);

        if(PlayerPrefs.GetInt("PlayerCustomerAsBeenVisited") == 0)
        {
            PlayerPrefs.SetInt("PlayerCustomerAsBeenVisited", 1);
        }


        StartCoroutine(WaitBeforeChangeScene());
    }

    IEnumerator WaitBeforeChangeScene()
    {
        yield return new WaitForSeconds(7f);
        SceneManager.LoadScene("SandBox 1");
    }
}
