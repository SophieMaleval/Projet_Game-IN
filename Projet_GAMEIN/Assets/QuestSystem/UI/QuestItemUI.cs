using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestItemUI : MonoBehaviour
{
    #region Fields

    QuestStatus statuts;

    private List<QuestStepItemUI> stepsUi = new List<QuestStepItemUI>();

    private bool isOpen;

    #endregion

    #region Properties

    public QuestStatus Statuts => statuts;

    #endregion

    #region UnityInspector

    [SerializeField] private Sprite listOpenIcon;
    [SerializeField] private Sprite listCloseIcon;

    [SerializeField] TextMeshProUGUI title;

    [SerializeField] Image listArrow;

    #endregion

    #region Behaviour

    private void Start()
    {
        SetOpenList(false);

        CheckActiveList();
    }

    public void CheckActiveList()
    {
        if (GameManager.Instance.gameCanvasManager.questTrackingUi.currentQuestStatusActive != null && GameManager.Instance.gameCanvasManager.questTrackingUi.currentQuestStatusActive == statuts && isOpen == false)
        {
            Debug.Log("OpenList");
            OpenList();
        }
    }

    public void AddStepsUi(QuestStepItemUI stepUi)
    {
        if(stepsUi.Contains(stepUi) == false)
        {
            stepsUi.Add(stepUi);
        }
    }

    public void Setup(QuestStatus status)
    {
        this.statuts = status;
        title.text = status.GetQuest()._name;
        
    }

    public QuestStatus GetQuestStatus()
    {
        return statuts;
    }

    public void OpenList()
    {
        isOpen = !isOpen;

        for (int i = 0; i < GameManager.Instance.gameCanvasManager.questUi.GetQuestsUI().Count; i++)
        {
            if(GameManager.Instance.gameCanvasManager.questUi.GetQuestsUI()[i] != this)
            {
                GameManager.Instance.gameCanvasManager.questUi.GetQuestsUI()[i].SetOpenList(false);
            }
        }

        UpdateOpenListState();

    }

    public void SetOpenList(bool value)
    {
        isOpen = value;

        UpdateOpenListState();
    }

    private void UpdateOpenListState()
    {
        if (isOpen)
        {
            GameManager.Instance.gameCanvasManager.questTrackingUi.SetQuestTrackingState(true, this.statuts);

            listArrow.GetComponent<RectTransform>().localEulerAngles = new Vector3(0, 0, 0);
            listArrow.sprite = listOpenIcon;
        }
        else
        {
            listArrow.GetComponent<RectTransform>().localEulerAngles = new Vector3(0, 0, 90);
            listArrow.sprite = listCloseIcon;
        }

        for (int i = 0; i < stepsUi.Count; i++)
        {
            stepsUi[i].gameObject.SetActive(isOpen);
        }
    }

    #endregion
}
