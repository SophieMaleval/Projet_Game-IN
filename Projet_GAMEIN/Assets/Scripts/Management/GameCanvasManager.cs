using AllosiusDev.DialogSystem;
using AllosiusDev.QuestSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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

    #endregion

    #region UnityInspector

    [SerializeField] private CutoutAnim cutoutMask;

    [SerializeField] private PopUpManager popUpManager;

    [SerializeField] private Transform messageParent;

    [SerializeField] private Image fadeAnimation;

    [SerializeField] private Image darkScreen;

    [SerializeField] private MessageBox messageBox;

    #endregion

    #region Behaviour

    private void Awake()
    {
        GameManager.Instance.gameCanvasManager = this;

        darkScreen.gameObject.SetActive(false);
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
