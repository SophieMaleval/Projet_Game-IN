using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using static TMPro.TMP_Dropdown;

namespace Village.EncyclopaediaMenu
{
    public class EncyclopaediaMenu : MonoBehaviour
    {
        #region Fields


        #endregion

        #region Properties


        public TMP_Dropdown LocationsDropdown => locationsDropdown;
        public ScrollRect dropdownScrollRect { get; set; }


        public Color NormalLocationTextColor => normalLocationTextColor;
        public Color QuestLocationTextColor => questLocationTextColor;

        #endregion

        #region UnityInspector

        [SerializeField] private Image titleDropdownBorder;
        [SerializeField] private Image panelBorder;

        [SerializeField] private GameObject[] titleLabelQuestIcons;

        [Space]

        [SerializeField] private ZoneButtonCtrl[] zonesButtons;

        [Space]

        [SerializeField] private LocationData[] locations;
        [SerializeField] private TMP_Dropdown locationsDropdown;
        [SerializeField] private TextMeshProUGUI locationDropdownLabel;
        [SerializeField] private string locationUnknownName = "???????";

        [SerializeField] private Color normalLocationTextColor;
        [SerializeField] private Color questLocationTextColor;

        [Space]

        [SerializeField] public LocationData currentLocation;

        [Space]

        [SerializeField] private Image placeImg;

        [SerializeField] private RectTransform[] npcSpawnPoints;
        [SerializeField] private EncyclopaediaNpcUI prefabEncyclopaediaNpcUi;

        [SerializeField] private Color unlockedPlaceColor;
        [SerializeField] private Color lockedPlaceColor;

        #endregion

        #region Behaviour

        private void Awake()
        {
            
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

        public void InitEncyclopaediaMenu()
        {
            Debug.Log("Redraw");

            if(CheckQuestActive())
                currentLocation = GameManager.Instance.gameCanvasManager.inventory.QuestTrackingUi.currentQuestStepStatusActive.GetQuestStep().questLocationData;

            InitLocationsDropdown();

            UpdateMenu();
        }

        private void InitLocationsDropdown()
        {
            locationsDropdown.ClearOptions();

            List<OptionData> options = new List<OptionData>();
            for (int i = 0; i < locations.Length; i++)
            {
                LocationStatus locationStatus = GameManager.Instance.locationsList.GetLocationStatus(locations[i]);
                if (locationStatus != null)
                {
                    LocationData questLocation = null;
                    if (CheckQuestActive())
                    {
                        questLocation = GameManager.Instance.gameCanvasManager.inventory.QuestTrackingUi.currentQuestStepStatusActive.GetQuestStep().questLocationData;
                    }

                    if (locationStatus.GetLocationAlreadyVisited() || (questLocation != null && locations[i] == questLocation))
                    {
                        EncyclopaediaOptionData optionData = new EncyclopaediaOptionData(locations[i].locationName);
                        optionData.location = locations[i];
                        options.Add(optionData);
                    }
                    else
                    {
                        EncyclopaediaOptionData optionData = new EncyclopaediaOptionData(locationUnknownName);
                        optionData.location = locations[i];
                        options.Add(optionData);
                    }
                }
            }

            locationsDropdown.AddOptions(options);


            for (int i = 0; i < locationsDropdown.options.Count; i++)
            {
                EncyclopaediaOptionData optionData = (EncyclopaediaOptionData)locationsDropdown.options[i];
                if (optionData.location == currentLocation)
                {
                    locationsDropdown.value = i;
                    SetTitleLabelQuestIcons();
                    break;
                }
            }
        }

        public void SetCurrentLocationUI()
        {
            EncyclopaediaOptionData optionData = (EncyclopaediaOptionData)locationsDropdown.options[locationsDropdown.value];
            if(optionData != null)
            {
                currentLocation = optionData.location;

                SetTitleLabelQuestIcons();
            }

            UpdateMenu();
        }

        private void UpdateMenu()
        {
            if (currentLocation == null)
            {
                return;
            }

            UpdatePanelInformations();
        }

        private void UpdatePanelInformations()
        {
            for (int i = 0; i < zonesButtons.Length; i++)
            {
                zonesButtons[i].SetSelectedButton(false);
            }

            for (int i = 0; i < npcSpawnPoints.Length; i++)
            {
                foreach (Transform item in npcSpawnPoints[i])
                {
                    Destroy(item.gameObject);
                }
            }


            Color colorToApply = Color.black;
            if (GameManager.Instance.locationsList.GetLocationStatus(currentLocation).GetLocationAlreadyVisited())
            {
                colorToApply = unlockedPlaceColor;
            }
            else
            {
                colorToApply = lockedPlaceColor;
            }

            placeImg.sprite = currentLocation.spriteLocation;
            placeImg.color = colorToApply;

            for (int i = 0; i < currentLocation.spritesLocationNpc.Length; i++)
            {
                if (i < npcSpawnPoints.Length)
                {
                    EncyclopaediaNpcUI encyclopaediaNpc = Instantiate(prefabEncyclopaediaNpcUi, npcSpawnPoints[i].transform);
                    encyclopaediaNpc.NpcImg.sprite = currentLocation.spritesLocationNpc[i];
                    encyclopaediaNpc.NpcImg.color = colorToApply;
                }
            }

            for (int i = 0; i < zonesButtons.Length; i++)
            {
                if (zonesButtons[i].ZoneAssociated.zoneType == currentLocation.zone.zoneType)
                {
                    zonesButtons[i].SetSelectedButton(true);
                }
            }

            panelBorder.color = currentLocation.zone.zoneColor;
            titleDropdownBorder.color = currentLocation.zone.zoneColor;
        }
        
        private void SetTitleLabelQuestIcons()
        {
            LocationData questLocation = null;
            if (CheckQuestActive())
            {
                questLocation = GameManager.Instance.gameCanvasManager.inventory.QuestTrackingUi.currentQuestStepStatusActive.GetQuestStep().questLocationData;
            }

            if (questLocation != null && currentLocation == questLocation)
            {
                locationDropdownLabel.color = questLocationTextColor;

                for (int i = 0; i < titleLabelQuestIcons.Length; i++)
                {
                    titleLabelQuestIcons[i].SetActive(true);
                }
            }
            else
            {
                locationDropdownLabel.color = normalLocationTextColor;

                for (int i = 0; i < titleLabelQuestIcons.Length; i++)
                {
                    titleLabelQuestIcons[i].SetActive(false);
                }
            }
        }

        #endregion
    }

    [System.Serializable]
    public class EncyclopaediaOptionData : OptionData
    {
        #region Properties

        public LocationData location { get; set; }
        public EncyclopaediaOptionData(string text) : base(text)
        {
        }

        #endregion
    }
}

