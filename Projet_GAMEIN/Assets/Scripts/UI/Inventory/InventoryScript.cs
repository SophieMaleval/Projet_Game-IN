﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro ;
using DG.Tweening;
using AllosiusDev.QuestSystem;
using AllosiusDev.DialogSystem;
using AllosiusDev.TranslationSystem;
using AllosiusDev.Audio;
using Village.EncyclopaediaMenu;

public class InventoryScript : MonoBehaviour
{
    #region Fields

    private int CarrousselSlotCurrentValue = 1 ;

    #endregion

    #region Properties

    public PlayerScript PlayerScript { get; set; }

    public QuestUI QuestUi => questUi;
    public QuestTrackingUI QuestTrackingUi => questTrackingUi;

    //public PopUpManager PopUpManager => popUpManager;

    public InventoryButtonCtrl InventoryButton => inventoryButton;
    public InventoryButtonCtrl ScooterButton => scooterButton;
    public InventoryButtonCtrl DezoomButton => dezoomButton;
    public bool canInteract { get; set; }

    //public Image DarkScreen => darkScreen;

    #endregion

    #region UnityInspector

    public GameObject InventoryPanel ;
    public GameObject PannelENTCanvas ;

    public EncyclopaediaMenu encyclopaediaMenu;

    public GameObject InventorySlotPannel ;
    public GameObject SlotInventoryPrefab ;
    public List<GameObject> InventorySlotInstantiate = new List<GameObject>() ;


    public GameObject ButtonPreviousSlot ;
    public GameObject ButtonNextSlot ;

    [SerializeField] private ToTranslateObject questTitle;
    [SerializeField] private ToTranslateObject inventoryTitle;
    [SerializeField] private ToTranslateObject mapTitle;

    [SerializeField] private ToTranslateObject textInventoryEmpty;

    [SerializeField] private ToTranslateObject mapUnavailable;

    //[SerializeField] private Image darkScreen;

    [Header("Quests")]

    [SerializeField] private QuestUI questUi;
    [SerializeField] private QuestTrackingUI questTrackingUi;

    [Header("Generals")]

    [SerializeField] private RectTransform SettingPanel ;
    [SerializeField] private RectTransform ControlsPanel;
    [SerializeField] private RectTransform CreditsPanel;

    //[SerializeField] private PopUpManager popUpManager;

    [SerializeField] private InventoryButtonCtrl inventoryButton;
    [SerializeField] private InventoryButtonCtrl scooterButton;
    [SerializeField] private InventoryButtonCtrl dezoomButton;

    [Header("Sounds")]

    [SerializeField] private AudioData sfxOpenInventory;
    [SerializeField] private AudioData sfxCloseInventory;

    #endregion

    #region Behaviour

    private void Awake()
    {
        if (GameManager.Instance.player != null)   // Récupère le player au lancement de la scène
        {
            PlayerScript = GameManager.Instance.player;
        }
        else
        {
            Debug.LogWarning("Player is null");
        }

        //darkScreen.gameObject.SetActive(false);
        canInteract = true;

        GameManager.Instance.locationsList.OnUpdate += encyclopaediaMenu.InitEncyclopaediaMenu;
        GameManager.Instance.questManager.OnUpdate += encyclopaediaMenu.InitEncyclopaediaMenu;
    }

    public void SwitchToggleInventoryDisplay()
    {
        if (!canInteract)
            return;

        SetInventoryCount();
        SetUIText();
        InventoryPanel.SetActive(!InventoryPanel.activeSelf);

        if(InventoryPanel.activeSelf)
        {
            AudioController.Instance.PlayAudio(sfxOpenInventory);

            transform.SetSiblingIndex(transform.parent.childCount - 2);

            PlayerScript.GetComponent<PlayerMovement>().StartActivity();

            encyclopaediaMenu.InitEncyclopaediaMenu();
        }
        else
        {
            AudioController.Instance.PlayAudio(sfxCloseInventory);

            transform.SetSiblingIndex(transform.parent.childCount - 2);

            if (PannelENTCanvas.activeSelf == false && GameManager.Instance.gameCanvasManager.dialogCanvas.gameObject.activeSelf == false) PlayerScript.GetComponent<PlayerMovement>().EndActivity();

            GameManager.Instance.CheckEventSystemState();

            GameManager.Instance.gameCanvasManager.questUi.UpdateQuestsItemsUI();
        }
    }

    public void SwitchScooterState()
    {
        PlayerMovement playerMovement = GameManager.Instance.player.GetComponent<PlayerMovement>();
        playerMovement.ChangeScootState(!playerMovement.OnScooter);
    }

    public void SwitchDezoomState()
    {
        if (GameCore.Instance != null && GameManager.Instance.zoomActive == false)
        {
            GameCore.Instance.Zoom(GameCore.Instance.dezoomTouchActive);
        }
    }

    public void OpenSetting()
    {
        StartCoroutine(AnimationPanels(true, SettingPanel));
    }
    public void CloseSetting()
    {
        StartCoroutine(AnimationPanels(false, SettingPanel));
    }

    public void OpenControls()
    {
        StartCoroutine(AnimationPanels(true, ControlsPanel));
    }
    public void CloseControls()
    {
        StartCoroutine(AnimationPanels(false, ControlsPanel));
    }

    public void OpenCredits()
    {
        StartCoroutine(AnimationPanels(true, CreditsPanel));
    }
    public void CloseCredits()
    {
        StartCoroutine(AnimationPanels(false, CreditsPanel));
    }

    IEnumerator AnimationPanels(bool OpenSettings, RectTransform PanelAnimate)
    {
        if (OpenSettings)
        {
            PanelAnimate.DOAnchorPosY(1500, 0.01f);
            yield return new WaitForSeconds(0.01f);
            PanelAnimate.gameObject.SetActive(true);
            PanelAnimate.GetComponent<Image>().DOFade(0.75f, 1f);
            PanelAnimate.DOAnchorPosY(0, 1f);
        }
        else
        {
            PanelAnimate.DOAnchorPosY(-50, 0.1f);
            yield return new WaitForSeconds(0.1f);
            PanelAnimate.GetComponent<Image>().DOFade(0f, 1f);
            PanelAnimate.DOAnchorPosY(1500, 1f);
            yield return new WaitForSeconds(1f);

            PanelAnimate.gameObject.SetActive(false);
        }
    }

    void SetUIText()
    {
        questTitle.Translation();
        inventoryTitle.Translation();
        mapTitle.Translation();
        textInventoryEmpty.Translation();
        mapUnavailable.Translation();

        for (int ISI = 0; ISI < InventorySlotInstantiate.Count; ISI++)
        {
            SetNameLangueObj(InventorySlotInstantiate[ISI].GetComponent<InventorySlot>(), PlayerScript.Inventaire[ISI]);
        }
    }


    public void SetInventoryCount()
    {
        InventorySlotInstantiate.Clear();
        ButtonPreviousSlot.SetActive(false);
        ButtonNextSlot.SetActive(false);
        CarrousselSlotCurrentValue = 1;

        foreach (Transform Child in InventorySlotPannel.transform)
        {
            Destroy(Child.gameObject);
        }

        if (PlayerScript.Inventaire.Count == 0)
        {
            textInventoryEmpty.gameObject.SetActive(true);
        }
        else
        {
            textInventoryEmpty.gameObject.SetActive(false);
            for (int Ic = 0; Ic < PlayerScript.Inventaire.Count; Ic++)
            {
                GameObject InventroySlotInstant = Instantiate(SlotInventoryPrefab, InventorySlotPannel.transform);
                InventorySlotInstantiate.Add(InventroySlotInstant);
                if (Ic >= 3) InventroySlotInstant.SetActive(false);
            }
        }

        if (InventorySlotInstantiate.Count > 3) ButtonNextSlot.SetActive(true);
        SetDisplayInventory();
    }


    void SetDisplayInventory()
    {
        for (int ISI = 0; ISI < InventorySlotInstantiate.Count; ISI++)
        {
            // Met le bon sprite
            SetImgSlotInventaire(InventorySlotInstantiate[ISI].GetComponent<InventorySlot>().Border, InventorySlotInstantiate[ISI].GetComponent<InventorySlot>().ObjectImage, PlayerScript.Inventaire[ISI].UISprite);

            // Affiche le bon nom
            SetNameLangueObj(InventorySlotInstantiate[ISI].transform.GetComponent<InventorySlot>(), PlayerScript.Inventaire[ISI]);
            if (InventorySlotInstantiate[ISI].GetComponent<InventorySlot>().NameObject.TextToTranslate.text.Length < 12) InventorySlotInstantiate[ISI].GetComponent<InventorySlot>().BackgroundNameObject.sizeDelta = new Vector2(InventorySlotInstantiate[ISI].GetComponent<InventorySlot>().BackgroundNameObject.sizeDelta.x, 36f);
            else InventorySlotInstantiate[ISI].GetComponent<InventorySlot>().BackgroundNameObject.sizeDelta = new Vector2(InventorySlotInstantiate[ISI].GetComponent<InventorySlot>().BackgroundNameObject.sizeDelta.x, 62f);
            InventorySlotInstantiate[ISI].GetComponent<InventorySlot>().BackgroundNameObject.gameObject.SetActive(true);

            if (PlayerScript.Inventaire[ISI].multipleEntries && PlayerScript.Inventaire[ISI].unité <= PlayerScript.Inventaire[ISI].valeurMax)
            {
                //Affiche l'image de fond du compteur
                InventorySlotInstantiate[ISI].GetComponent<InventorySlot>().Counter.enabled = true;

                //Modifie valeurs du compteur
                InventorySlotInstantiate[ISI].GetComponent<InventorySlot>().Counter.GetComponentInChildren<TextMeshProUGUI>().text = PlayerScript.Inventaire[ISI].unité /*- 1*/ + " / " + PlayerScript.Inventaire[ISI].valeurMax.ToString();
                InventorySlotInstantiate[ISI].GetComponent<InventorySlot>().Counter.GetComponentInChildren<TextMeshProUGUI>().enabled = true;
            }
            else
            {
                InventorySlotInstantiate[ISI].GetComponent<InventorySlot>().Counter.enabled = false;
                InventorySlotInstantiate[ISI].GetComponent<InventorySlot>().Counter.GetComponentInChildren<TextMeshProUGUI>().enabled = false;
            }
        }
    }

    void SetNameLangueObj(InventorySlot inventorySlotToSet, InteractibleObject NameTarget)
    {
        inventorySlotToSet.NameObject.SetTranslationKey(NameTarget.translationKey, TypeDictionary.InventoryItems);
    }

    void SetImgSlotInventaire(RectTransform ContourImg, Image ImgSlot, Sprite SpriteDisplay)
    {
        ImgSlot.sprite = SpriteDisplay;

        Vector2 SizeSprite = SpriteDisplay.bounds.size;
        SizeSprite *= 100f;
        float DivisionSpriteSize = 1f;

        while (!SizeIsGood(SizeSprite, ContourImg.sizeDelta, DivisionSpriteSize))
        {
            DivisionSpriteSize += 0.1f;
        }

        ImgSlot.GetComponent<RectTransform>().sizeDelta = new Vector2(SizeSprite.x / DivisionSpriteSize, SizeSprite.y / DivisionSpriteSize);

        ImgSlot.enabled = true;
    }

    bool SizeIsGood(Vector2 RefSizeSprite, Vector2 ContourImgSizeDelta, float DivisionSpriteValue)
    {
        ContourImgSizeDelta -= new Vector2(15f, 15f);
        if (RefSizeSprite.x >= RefSizeSprite.y)
        {
            if (((RefSizeSprite.x / DivisionSpriteValue) > ContourImgSizeDelta.x))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        else
        {
            if (((RefSizeSprite.y / DivisionSpriteValue) > ContourImgSizeDelta.y))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }



    public void DisplayNextSlot()
    {
        if (CarrousselSlotCurrentValue < InventorySlotInstantiate.Count - 1) CarrousselSlotCurrentValue++;

        ChangeCurrentSlotDisplay();

        if (CarrousselSlotCurrentValue >= InventorySlotInstantiate.Count - 2) ButtonNextSlot.SetActive(false);
        ButtonPreviousSlot.SetActive(true);
    }

    public void DisplayPreviousSlot()
    {
        if (CarrousselSlotCurrentValue > 1) CarrousselSlotCurrentValue--;

        ChangeCurrentSlotDisplay();

        if (CarrousselSlotCurrentValue == 1) ButtonPreviousSlot.SetActive(false);
        ButtonNextSlot.SetActive(true);
    }

    void ChangeCurrentSlotDisplay()
    {
        for (int Is = 0; Is < InventorySlotInstantiate.Count; Is++)
        {
            if (Is >= CarrousselSlotCurrentValue - 1 && Is <= CarrousselSlotCurrentValue + 1)
            {
                InventorySlotInstantiate[Is].SetActive(true);
            }
            else
            {
                InventorySlotInstantiate[Is].SetActive(false);
            }
        }
    }

    #endregion

}