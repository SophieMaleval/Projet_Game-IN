using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PopUpManager : MonoBehaviour
{
    #region UnityInspector

    [SerializeField] private GameObject PrefabPopUp ;

    [SerializeField] private string removeInventoryTranslationKey;
    [SerializeField] private string addInventoryTranslationKey;

    #endregion

    #region Behaviour

    public void CreatePopUpItem(InteractibleObject ItemModify, bool IsAdded)
    {
        GameObject PopUPInstantiate = Instantiate(PrefabPopUp, transform);
        PopUpScript ScriptPopUP = PopUPInstantiate.GetComponent<PopUpScript>();  

        ScriptPopUP.ImgDisplay = ItemModify.UISprite ;

        if(!IsAdded)
        {
            string itemName = LangueManager.Instance.Translate(ItemModify.translationKey);
            string removeInventory = LangueManager.Instance.Translate(removeInventoryTranslationKey);
            ScriptPopUP.titlePopUp = itemName + " " + removeInventory;

            string descriptionItemRemove = LangueManager.Instance.Translate(ItemModify.descriptionItemRemoveTranslationKey);
            ScriptPopUP.descriptionPopUp = descriptionItemRemove;
        } 
        else 
        {
            string itemName = LangueManager.Instance.Translate(ItemModify.translationKey);
            string addInventory = LangueManager.Instance.Translate(addInventoryTranslationKey);
            ScriptPopUP.titlePopUp = itemName + " " + addInventory;

            string descriptionItemAdd = LangueManager.Instance.Translate(ItemModify.descriptionItemAddTranslationKey);
            ScriptPopUP.descriptionPopUp = descriptionItemAdd;
        }

        SetHeightPopUpDisplayer();
    }

    public void CreatePopUpForScooter(PopUpData popUpData)
    {
        GameObject PopUPInstantiate = Instantiate(PrefabPopUp, transform);
        PopUpScript ScriptPopUP = PopUPInstantiate.GetComponent<PopUpScript>();  

        ScriptPopUP.ImgDisplay = popUpData.spriteToDisplay ;

        string title = LangueManager.Instance.Translate(popUpData.titleTranslationKey);
        ScriptPopUP.titlePopUp = title;

        string description = LangueManager.Instance.Translate(popUpData.descriptionTranslationKey);
        ScriptPopUP.descriptionPopUp = description;

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

    #endregion
}
