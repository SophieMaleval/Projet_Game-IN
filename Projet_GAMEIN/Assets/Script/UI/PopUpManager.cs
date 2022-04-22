using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PopUpManager : MonoBehaviour
{
    [SerializeField] private GameObject PrefabPopUp ;
    [SerializeField] private CSVReader PopUpUIText ;

    void Awake() 
    {
        if(GameObject.Find("Player Backpack") != null) PopUpUIText = GameObject.Find("Player Backpack").GetComponent<CSVReader>();
    }

    public void CreatePopUpItem(InteractibleObject ItemModify, bool IsAdded)
    {
        GameObject PopUPInstantiate = Instantiate(PrefabPopUp, transform);
        PopUpScript ScriptPopUP = PopUPInstantiate.GetComponent<PopUpScript>();  

        ScriptPopUP.ImgDisplay = ItemModify.UISprite ;

        if(!IsAdded)
        {
            ScriptPopUP.TitlePopUPFR = ItemModify.NameFR + " " + PopUpUIText.UIText.PopUpFR[1];
            ScriptPopUP.TitlePopUPEN = ItemModify.NameEN + " " + PopUpUIText.UIText.PopUpEN[1];

            ScriptPopUP.DescripPopUPFR = ItemModify.DescriptionItemRemoveFR ;
            ScriptPopUP.DescripPopUPEN = ItemModify.DescriptionItemRemoveEN ;
        } else {
            ScriptPopUP.TitlePopUPFR = ItemModify.NameFR + " " + PopUpUIText.UIText.PopUpFR[2];
            ScriptPopUP.TitlePopUPEN = ItemModify.NameEN + " " + PopUpUIText.UIText.PopUpEN[2];

            ScriptPopUP.DescripPopUPFR = ItemModify.DescriptionItemAddFR ;
            ScriptPopUP.DescripPopUPEN = ItemModify.DescriptionItemAddEN ;
        }

        SetHeightPopUpDisplayer();
    }

    public void CreatePopUpForScooter(Sprite ImgScoot)
    {
        GameObject PopUPInstantiate = Instantiate(PrefabPopUp, transform);
        PopUpScript ScriptPopUP = PopUPInstantiate.GetComponent<PopUpScript>();  

        ScriptPopUP.ImgDisplay = ImgScoot ;

        ScriptPopUP.TitlePopUPFR = PopUpUIText.UIText.PopUpFR[3] + " " + PopUpUIText.UIText.PopUpFR[0];
        ScriptPopUP.TitlePopUPEN = PopUpUIText.UIText.PopUpEN[3] + " " + PopUpUIText.UIText.PopUpEN[0];

        ScriptPopUP.DescripPopUPFR = PopUpUIText.UIText.PopUpFR[4];
        ScriptPopUP.DescripPopUPEN = PopUpUIText.UIText.PopUpEN[4];

        SetHeightPopUpDisplayer();
    }

    public void SetHeightPopUpDisplayer()
    {
        DOTween.Kill(this.GetComponent<RectTransform>());

        GetComponent<RectTransform>().DOSizeDelta(new Vector2(GetComponent<RectTransform>().sizeDelta.x, transform.childCount * 150f), 1f) ;
    }

    public void SetHeightPopUpAgain()
    {
        StartCoroutine(WaitAndSetHeight());
    }

    IEnumerator WaitAndSetHeight()
    {
        yield return new WaitForSeconds(1.05f);
        SetHeightPopUpDisplayer();
    }
}
