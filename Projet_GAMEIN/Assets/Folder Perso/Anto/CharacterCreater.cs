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
    //[SerializeField] private RectTransform SkinButton ;
    [SerializeField] private RectTransform SkinPanel ;
    [SerializeField] private RectTransform ContainerSkinPanel ;
    private float SkinPanelHeight = 75f ;
    //[SerializeField] private RectTransform HairButton ;
    [SerializeField] private RectTransform HairPanel ;
    [SerializeField] private RectTransform ContainerHairPanel ;
    private float HairPanelHeight = 125f ;
    //[SerializeField] private RectTransform BodyButton ;
    [SerializeField] private RectTransform BodyPanel ;
    [SerializeField] private RectTransform ContainerBodyPanel ;
    private float BodyPanelHeight = 125f ;
    //[SerializeField] private RectTransform BottomButton ;
    [SerializeField] private RectTransform BottomPanel ;
    [SerializeField] private RectTransform ContainerBottomPanel ;
    private float BottomPanelHeight = 125f ;
    //[SerializeField] private RectTransform ShoeButton ;
    [SerializeField] private RectTransform ShoePanel ;
    [SerializeField] private RectTransform ContainerShoePanel ;
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
            DisplayAvatar.RecupExistSkin();                
        }

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



    // Panel Custom
    public void SkinColor(float SliderValue)
    {   DisplayAvatar.SkinColorPourcentage = SliderValue ;     }
    
    public void ChangeHair(ItemCustomer HairChoice)
    {    DisplayAvatar.HairSprites = HairChoice ;    }
    public void ChangeHairColor(int NumColorList)
    {    DisplayAvatar.HairColor = ColorCustomList[NumColorList] ;  }
    
    public void ChangeBody(ItemCustomer BodyChoice)
    {   DisplayAvatar.BodySprites = BodyChoice ;    }
    public void ChangeBodyColor(int NumColorList)
    {    DisplayAvatar.BodyColor = ColorCustomList[NumColorList] ;   }

    public void ChangeBottom(ItemCustomer BottomChoice)
    {   DisplayAvatar.BottomSprites = BottomChoice ;    }
    public void ChangeBottomColor(int NumColorList)
    {   DisplayAvatar.BottomColor = ColorCustomList[NumColorList] ;     }

    public void ChangeShoe(ItemCustomer ShoeChoice)
    {   DisplayAvatar.ShoeSprites = ShoeChoice ;    }
    public void ChangeShoeColor(int NumColorList)
    {   DisplayAvatar.ShoeColor = ColorCustomList[NumColorList] ;   }




    // Final Button
    public void RandomCharacter()
    {
        // Random Skin
        DisplayAvatar.SkinColorPourcentage = Random.Range(0f, 1f);

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

        SceneManager.LoadScene("SandBox 1");

    }
}
