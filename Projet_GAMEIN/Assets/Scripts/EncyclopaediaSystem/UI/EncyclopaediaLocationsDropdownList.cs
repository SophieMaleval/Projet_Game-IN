using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Village.EncyclopaediaMenu
{
    public class EncyclopaediaLocationsDropdownList : MonoBehaviour
    {
        #region Fields

        private ScrollRect scrollRect;

        private List<EncyclopaediaLocationsDropdownItem> items = new List<EncyclopaediaLocationsDropdownItem>();

        #endregion

        #region UnityInspector

        [SerializeField]
        private EncyclopaediaMenu encyclopaediaMenu;

        #endregion

        #region Behaviour

        private void Start()
        {
            scrollRect = GetComponent<ScrollRect>();
            encyclopaediaMenu.dropdownScrollRect = scrollRect;

            for (int i = 0; i < scrollRect.content.childCount; i++)
            {
                EncyclopaediaLocationsDropdownItem item = scrollRect.content.GetChild(i).GetComponent<EncyclopaediaLocationsDropdownItem>();
                if (item != null && item.gameObject.activeSelf)
                {
                    Debug.Log(item.name);
                    items.Add(item);
                }
            }

            for (int i = 0; i < encyclopaediaMenu.LocationsDropdown.options.Count; i++)
            {
                EncyclopaediaOptionData optionData = (EncyclopaediaOptionData)encyclopaediaMenu.LocationsDropdown.options[i];
                Debug.Log(optionData.location.name);

                LocationStatus locationStatus = GameManager.Instance.locationsList.GetLocationStatus(optionData.location);
                if (locationStatus != null && i < items.Count)
                {
                    LocationData questLocation = null;
                    if (CheckQuestActive())
                    {
                        questLocation = GameManager.Instance.gameCanvasManager.inventory.QuestTrackingUi.currentQuestStepStatusActive.GetQuestStep().questLocationData;
                    }

                    if (locationStatus.GetLocationAlreadyVisited() || (questLocation != null && optionData.location == questLocation))
                    {
                        items[i].GetComponent<Toggle>().enabled = true;
                        Debug.Log("true");
                    }
                    else
                    {
                        items[i].GetComponent<Toggle>().enabled = false;
                        Debug.Log("false");
                    }

                    if(questLocation != null && optionData.location == questLocation)
                    {
                        items[i].ItemLabel.color = encyclopaediaMenu.QuestLocationTextColor;
                        for (int j = 0; j < items[i].QuestIcons.Length; j++)
                        {
                            items[i].QuestIcons[j].SetActive(true);
                        }
                    }
                    else
                    {
                        items[i].ItemLabel.color = encyclopaediaMenu.NormalLocationTextColor;
                        for (int j = 0; j < items[i].QuestIcons.Length; j++)
                        {
                            items[i].QuestIcons[j].SetActive(false);
                        }
                    }
                }
            }
        }

        private bool CheckQuestActive()
        {
            if (GameManager.Instance.gameCanvasManager.inventory.QuestTrackingUi.currentQuestStatusActive != null &&
                GameManager.Instance.gameCanvasManager.inventory.QuestTrackingUi.currentQuestStepStatusActive != null)
            {
                return true;
            }

            return false;
        }

        #endregion
    }
}
