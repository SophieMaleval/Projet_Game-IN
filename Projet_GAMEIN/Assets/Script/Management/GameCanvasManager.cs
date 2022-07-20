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

    public GameObject Fade => fade;

    public InventoryScript inventory { get; set; }
    public QuestSys questManager { get; set; }
    public QuestUI questUi { get; set; }
    public DialogueDisplayerController dialogCanvas { get; set; }
    public DialogueDisplayUI newDialogCanvas { get; set; }
    public QCMManager qcmPanel { get; set; }

    public EventSystem eventSystem { get; set; }

    #endregion

    #region UnityInspector

    [SerializeField] private GameObject fade;

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

        /*if(eventSystem.currentSelectedGameObject != null)
            Debug.Log(eventSystem.currentSelectedGameObject.name);*/
    }

    #endregion
}
