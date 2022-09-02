using AllosiusDev.DialogSystem;
using AllosiusDev.QuestSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using AllosiusDev.TranslationSystem;
using DG.Tweening;

public class GameCanvasManager : MonoBehaviour
{
    #region Fields

    private GameObject eventSystemCurrentObjectSelected;

    #endregion

    #region Properties

    //public GameObject Fade => fade;
    public CutoutAnim CutoutMask => cutoutMask;

    public PopUpManager PopUpManager => popUpManager;

    public Transform MessageParent => messageParent;

    public Image DarkScreen => darkScreen;

    public Image FadeAnimation => fadeAnimation;

    public InventoryScript inventory { get; set; }
    //public QuestSys questManager { get; set; }
    public QuestTrackingUI questTrackingUi { get; set; }
    public QuestUI questUi { get; set; }
    public DialogueDisplayUI dialogCanvas { get; set; }

    public EventSystem eventSystem { get; set; }

    public CanvasGroup TitleBanner => titleBanner;
    public ToTranslateObject TitleBannerText => titleBannerText;

    #endregion

    #region UnityInspector

    [SerializeField] private CutoutAnim cutoutMask;

    [SerializeField] private PopUpManager popUpManager;

    [SerializeField] private Transform messageParent;

    [SerializeField] private Image fadeAnimation;

    [SerializeField] private Image darkScreen;

    [SerializeField] private MessageBox messageBox;

    [SerializeField] private CanvasGroup titleBanner;
    [SerializeField] private ToTranslateObject titleBannerText;

    #endregion

    #region Behaviour

    private void Awake()
    {
        GameManager.Instance.gameCanvasManager = this;

        darkScreen.gameObject.SetActive(false);

        titleBanner.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (eventSystem.currentSelectedGameObject != null)
        {
            eventSystemCurrentObjectSelected = eventSystem.currentSelectedGameObject;
        }

        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0 || Input.GetButtonDown("Submit"))
        {
            if (eventSystemCurrentObjectSelected != null && eventSystem.currentSelectedGameObject == null)
            {
                eventSystem.SetSelectedGameObject(eventSystemCurrentObjectSelected);
            }
        }
    }

    public IEnumerator SetTitleBannerActivation(bool value, string keyTrad, float transitionAnimDuration)
    {
        titleBannerText.SetTranslationKey(keyTrad, TypeDictionary.GeneralsUI);
        titleBanner.gameObject.SetActive(value);
        if(value)
        {
            FadeInUI(titleBanner, transitionAnimDuration);

            yield return new WaitForSeconds(transitionAnimDuration);

            FadeOutUI(titleBanner, transitionAnimDuration);

            titleBanner.gameObject.SetActive(false);
        }
    }

    public void FadeOutUI(CanvasGroup canvasGroup, float transitionAnimDuration)
    {
        canvasGroup.DOFade(0, transitionAnimDuration);
    }

    public void FadeInUI(CanvasGroup canvasGroup, float transitionAnimDuration)
    {
        canvasGroup.DOFade(1, transitionAnimDuration);
    }

    public MessageBox CreateMessageBox(string message, float boxSize, bool isChoiceBox = false)
    {
        MessageBox box = Instantiate(messageBox, messageParent.transform);
        box.messageKey = message;
        if(boxSize > 0)
        {
            box.SetBoxSize(boxSize);
        }
        box.UpdateMessage(isChoiceBox);

        return box;
    }

    #endregion
}
