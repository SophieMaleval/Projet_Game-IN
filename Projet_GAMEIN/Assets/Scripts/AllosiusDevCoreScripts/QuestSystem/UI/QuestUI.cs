using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AllosiusDev.QuestSystem
{
    public class QuestUI : MonoBehaviour
    {
        #region Fields

        private List<QuestItemUI> questsUi = new List<QuestItemUI>();

        #endregion

        #region UnityInspector

        [SerializeField] QuestItemUI questPrefab;
        [SerializeField] QuestStepItemUI questStepPrefab;

        [SerializeField] private Transform contentQuests;

        #endregion

        #region Behaviour

        void Start()
        {

            Redraw();
        }
        public List<QuestItemUI> GetQuestsUI()
        {
            return questsUi;
        }

        public void Redraw()
        {
            Debug.Log("Redraw");

            foreach (Transform item in contentQuests)
            {
                Destroy(item.gameObject);
            }

            questsUi.Clear();

            foreach (QuestStatus status in GameManager.Instance.questManager.GetStatuses())
            {
                QuestItemUI uiInstance = Instantiate<QuestItemUI>(questPrefab, contentQuests);

                foreach (QuestStepStatus step in status.GetQuestStepStatuses())
                {
                    if (step.isUnlocked)
                    {
                        QuestStepItemUI stepUiInstance = Instantiate<QuestStepItemUI>(questStepPrefab, contentQuests);
                        stepUiInstance.Setup(step);
                        uiInstance.AddStepsUi(stepUiInstance);
                    }
                }

                Debug.Log("Seup Ui Instance");
                uiInstance.Setup(status);
                if (GameManager.Instance.gameCanvasManager.questTrackingUi.currentQuestStatusActive == null || GameManager.Instance.gameCanvasManager.questTrackingUi.currentQuestStatusActive == uiInstance.Statuts)
                {
                    GameManager.Instance.gameCanvasManager.questTrackingUi.SetQuestTrackingState(true, uiInstance.Statuts);
                }
                questsUi.Add(uiInstance);

                if (status.GetQuestCompleted())
                {
                    if (GameManager.Instance.gameCanvasManager.questTrackingUi.currentQuestStatusActive != null)
                    {
                        GameManager.Instance.gameCanvasManager.questTrackingUi.SetQuestTrackingState(false, null);
                    }
                }
            }
        }

        public void UpdateQuestsItemsUI()
        {
            if (questsUi.Count > 0)
            {
                foreach (QuestItemUI questItem in questsUi)
                {
                    questItem.CheckActiveList();
                }

            }

        }


        #endregion
    }
}
