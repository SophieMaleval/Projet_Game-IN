using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class PopUpScript : MonoBehaviour
{
    #region UnityInspector

    [Header ("Parameter")]
    [SerializeField] private Image ImgPopUP ;
    [SerializeField] private TextMeshProUGUI TitlePopUP ;
    [SerializeField] private TextMeshProUGUI DescriptionPopUP ;

    [Header ("Information")]
    [HideInInspector] public Sprite ImgDisplay ;
    [HideInInspector] public string TitlePopUPFR;
    [HideInInspector] public string TitlePopUPEN;
    [HideInInspector] public string DescripPopUPFR;
    [HideInInspector] public string DescripPopUPEN;

    #endregion

    #region Behaviour

    // Start is called before the first frame update
    void Start()
    {
        SetPopUP();
        StartCoroutine(TimeDisplay());
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerPrefs.GetInt("Langue") == 0 && TitlePopUP.text != TitlePopUPFR)   SetPopUP();
        if(PlayerPrefs.GetInt("Langue") == 1 && TitlePopUP.text != TitlePopUPEN)   SetPopUP();

    }

    void SetPopUP()
    {
        SetImgSlotPopUp(ImgPopUP.transform.parent.GetComponent<RectTransform>(), ImgPopUP, ImgDisplay);

        if(PlayerPrefs.GetInt("Langue") == 0)
        {
            TitlePopUP.text = TitlePopUPFR ;
            DescriptionPopUP.text = DescripPopUPFR ;
        }

        if(PlayerPrefs.GetInt("Langue") == 1)
        {
            TitlePopUP.text = TitlePopUPEN ;
            DescriptionPopUP.text = DescripPopUPEN ;
        }
    }


    void SetImgSlotPopUp(RectTransform ContourImg, Image ImgSlot, Sprite SpriteDisplay)
    {
        ImgSlot.sprite = SpriteDisplay ;

        Vector2 SizeSprite = SpriteDisplay.bounds.size ;
        SizeSprite *= 100f ;
        float DivisionSpriteSize = 1f ;

        while(!SizeIsGood(SizeSprite, ContourImg.sizeDelta, DivisionSpriteSize))
        {
            DivisionSpriteSize += 0.1f ;
        } 

        ImgSlot.GetComponent<RectTransform>().sizeDelta = new Vector2(SizeSprite.x / DivisionSpriteSize, SizeSprite.y / DivisionSpriteSize);

        ImgSlot.enabled = true ;
    }

    bool SizeIsGood(Vector2 RefSizeSprite, Vector2 ContourImgSizeDelta , float DivisionSpriteValue)
    {
        ContourImgSizeDelta -= new Vector2(15f, 15f);
        if(RefSizeSprite.x >= RefSizeSprite.y)
        {
            if (((RefSizeSprite.x / DivisionSpriteValue) > ContourImgSizeDelta.x) )
            {
                return false ;
            } else {
                return true ;
            }            
        } else {
            if (((RefSizeSprite.y / DivisionSpriteValue) > ContourImgSizeDelta.y) )
            {
                return false ;
            } else {
                return true ;
            }   
        }
    }




    IEnumerator TimeDisplay()
    {
        yield return new WaitForSeconds(7.5f);
        transform.GetChild(0).GetComponent<RectTransform>().DOAnchorPosX(transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition.x + 500f, 0.95f);
        GetComponentInParent<PopUpManager>().SetHeightPopUpAgain();
        Destroy(gameObject, 1f);
    }

    #endregion
}
