using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro ;
using DG.Tweening;

public class InventoryScript : MonoBehaviour
{
    #region Fields

    private int CarrousselSlotCurrentValue = 1 ;

    private CSVReader TextUILocation;

    #endregion

    #region Properties

    public PlayerScript PlayerScript { get; set; }
    public PopUpManager PopUpManager => popUpManager;

    #endregion

    #region UnityInspector

    public GameObject InventoryPanel ;
    public GameObject DialogueCanvas ;
    public GameObject PannelENTCanvas ;

    public GameObject InventorySlotPannel ;
    public GameObject SlotInventoryPrefab ;
    public List<GameObject> InventorySlotInstantiate = new List<GameObject>() ;
    public TextMeshProUGUI TextInventoryEmpty ;
    public GameObject ButtonPreviousSlot ;
    public GameObject ButtonNextSlot ;

    [SerializeField] private TextMeshProUGUI QuestTitle ;
    [SerializeField] private TextMeshProUGUI InventoryTitle ;
    [SerializeField] private TextMeshProUGUI MapTitle ;
    //public List<Image> InventoryDisplayer ;

    [SerializeField] private RectTransform SettingPanel ;
    [SerializeField] private RectTransform ControlsPanel;
    [SerializeField] private RectTransform CreditsPanel;

    [SerializeField] private PopUpManager popUpManager;

    #endregion

    #region Behaviour

    private void Awake()
    {
        if (GameManager.Instance.player != null)   // Récupère le player au lancement de la scène
        {
            PlayerScript = GameManager.Instance.player;
            TextUILocation = PlayerScript.GetComponentInChildren<CSVReader>();
        }
        else
        {
            Debug.LogWarning("Player is null");
        }
    }

    void Update()
    {
        if (PlayerPrefs.GetInt("Langue") == 0 && QuestTitle.text != TextUILocation.UIText.PauseFR[0]) SetUIText();
        if (PlayerPrefs.GetInt("Langue") == 1 && QuestTitle.text != TextUILocation.UIText.PauseEN[0]) SetUIText();
    }

    public void SwitchToggleInventoryDisplay()
    {
        //SetDisplayInventory();        
        SetInventoryCount();
        SetUIText();
        InventoryPanel.SetActive(!InventoryPanel.activeSelf);
        if (!InventoryPanel.activeSelf)
        {
            transform.SetSiblingIndex(transform.parent.childCount - 2);

            if (DialogueCanvas.activeSelf == true) PlayerScript.playerBackpack.ResumeDialogue();

            if (DialogueCanvas.activeSelf == false && PannelENTCanvas.activeSelf == false && GameManager.Instance.gameCanvasManager.newDialogCanvas.gameObject.activeSelf == false) PlayerScript.GetComponent<PlayerMovement>().EndActivity();

            if(GameManager.Instance.player.InDiscussion)
            {
                if(GameManager.Instance.player.GetComponent<PlayerConversant>().IsChoosing() == false)
                {
                    GameManager.Instance.gameCanvasManager.eventSystem.SetSelectedGameObject(GameManager.Instance.gameCanvasManager.newDialogCanvas.NextButton.gameObject);
                }
                else if (GameManager.Instance.player.GetComponent<PlayerConversant>().IsChoosing() && GameManager.Instance.gameCanvasManager.newDialogCanvas.CurrentChoices.Count > 0)
                {
                    GameManager.Instance.gameCanvasManager.eventSystem.SetSelectedGameObject(GameManager.Instance.gameCanvasManager.newDialogCanvas.CurrentChoices[0]);
                }
            }
           

        }
        else
        {
            transform.SetSiblingIndex(transform.parent.childCount - 2);

            PlayerScript.GetComponent<PlayerMovement>().StartActivity();
            PlayerScript.playerBackpack.PausedInDialogue();
            
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
        if (PlayerPrefs.GetInt("Langue") == 0)
        {
            QuestTitle.text = TextUILocation.UIText.PauseFR[0];
            InventoryTitle.text = TextUILocation.UIText.PauseFR[1];
            TextInventoryEmpty.text = TextUILocation.UIText.PauseFR[2];
            MapTitle.text = TextUILocation.UIText.PauseFR[3];
        }
        if (PlayerPrefs.GetInt("Langue") == 1)
        {
            QuestTitle.text = TextUILocation.UIText.PauseEN[0];
            InventoryTitle.text = TextUILocation.UIText.PauseEN[1];
            TextInventoryEmpty.text = TextUILocation.UIText.PauseEN[2];
            MapTitle.text = TextUILocation.UIText.PauseEN[3];
        }

        for (int ISI = 0; ISI < InventorySlotInstantiate.Count; ISI++)
        {
            SetNameLangueObj(InventorySlotInstantiate[ISI].GetComponent<InventorySlot>().NameObjectText, PlayerScript.Inventaire[ISI]);
        }
    }


    public void SetInventoryCount()
    {
        /*InventoryDisplayer.Clear();
        for (int Id = 0; Id < InventorySlotPannel.transform.childCount; Id++)
        {
            InventoryDisplayer.Add(null);            
        }        

        if(PlayerScript != null && PlayerScript.Inventaire.Length != InventoryDisplayer.Count)
        {
            PlayerScript.Inventaire = new InteractibleObject[InventorySlotPannel.transform.childCount] ;
        }*/

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
            TextInventoryEmpty.gameObject.SetActive(true);
        }
        else
        {
            TextInventoryEmpty.gameObject.SetActive(false);
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
            SetNameLangueObj(InventorySlotInstantiate[ISI].transform.GetComponent<InventorySlot>().NameObjectText, PlayerScript.Inventaire[ISI]);
            if (InventorySlotInstantiate[ISI].GetComponent<InventorySlot>().NameObjectText.text.Length < 12) InventorySlotInstantiate[ISI].GetComponent<InventorySlot>().BackgroundNameObject.sizeDelta = new Vector2(InventorySlotInstantiate[ISI].GetComponent<InventorySlot>().BackgroundNameObject.sizeDelta.x, 36f);
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

    void SetNameLangueObj(TextMeshProUGUI TextDisplay, InteractibleObject NameTarget)
    {
        if (PlayerPrefs.GetInt("Langue") == 0) TextDisplay.text = NameTarget.NameFR;
        if (PlayerPrefs.GetInt("Langue") == 1) TextDisplay.text = NameTarget.NameEN;
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
