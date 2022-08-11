using AllosiusDev.DialogSystem;
using AllosiusDev.QuestSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameCanvasManager : MonoBehaviour
{
    #region Fields

    private GameObject eventSystemCurrentObjectSelected;

    #endregion

    #region Properties

    //public GameObject Fade => fade;
    public CutoutAnim CutoutMask => cutoutMask;

    public InventoryScript inventory { get; set; }
    //public QuestSys questManager { get; set; }
    public QuestTrackingUI questTrackingUi { get; set; }
    public QuestUI questUi { get; set; }
    public DialogueDisplayUI dialogCanvas { get; set; }

    public EventSystem eventSystem { get; set; }

    #endregion

    #region UnityInspector

    [SerializeField] private CutoutAnim cutoutMask;

    #endregion

    #region Behaviour

    private void Awake()
    {
        GameManager.Instance.gameCanvasManager = this;
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

    #endregion
}
